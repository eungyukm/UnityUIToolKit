using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class AssetEditorMangaer : EditorWindow
{
    [MenuItem("Tools/AssetEditor")]
    public static void ShowWindow()
    {
        GetWindow<AssetEditorMangaer>();
    }

    private string selectedObjectName = "";

    private RadioButtonGroup placeRadioButtonGroup;

    private Location[] _locations; 

    private void OnEnable()
    {
        string path = "Assets/Editor/AssetEditor/UIDocument/AssetEditor.uxml";

        var asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        // UXML을 EditorWindow에 배치
        asset.CloneTree(rootVisualElement);

        SetUIUP();
        InitRadioButtonGroup();
        
        rootVisualElement.Query<Button>("ChangeNameBtn").ForEach((button) =>
        {
            button.clicked += OnClickNameChange;
        });
        
        rootVisualElement.Query<Button>("GetObjectPathBtn").ForEach((button) =>
        {
            Debug.Log("Clicked");
            button.clicked += OnClickGetPath;
        });

        rootVisualElement.Query<Button>("BtnTest").ForEach((btn) =>
        {
            
        });
    }

    private void InitRadioButtonGroup()
    {
        placeRadioButtonGroup = rootVisualElement.Query<RadioButtonGroup>("PlaceRadioGroup");
    }

    private void SetUIUP()
    {
        _locations = GetLocations();
        // foreach (var item in locations)
        // {
        //     Debug.Log("item : " + item.placeName);
        // }

        rootVisualElement.Query<RadioButtonGroup>("PlaceRadioGroup").ForEach((group =>
        {
            foreach (var location in _locations)
            {
                RadioButton radioButton = new RadioButton(location.placeName);
                group.Add(radioButton);   
            }
        }));
    }

    private void OnClickNameChange()
    {
        // string assetPath = "";
        // rootVisualElement.Query<TextField>("SearchField").ForEach((textfield) =>
        // {
        //     assetPath = textfield.text;
        //     Debug.Log("asset path : " + assetPath);
        // });

        string changeName = "";
        rootVisualElement.Query<TextField>("NameField").ForEach((textField) =>
        {
            changeName = textField.text;
            Debug.Log("name : " + changeName);
        });
        string assetFolderPath = selectedObjectName;
        Debug.Log("folder path : "+ assetFolderPath);
        string[] assetGuids = AssetDatabase.FindAssets("t:object", new[] {assetFolderPath});
        Debug.Log("assetguid length : " + assetGuids.Length);
        string[] assetPathList = Array.ConvertAll<string, string>(assetGuids, AssetDatabase.GUIDToAssetPath);
        
        // 라디오 버튼에 출력되는 이름을 얻어 옵니다.
        int placeIndex = GetPlaceRadioButtonIndex();
        string locationTag = _locations[placeIndex].placeTag; 
        
        Debug.Log("asset Length : " + assetPathList.Length);
        for (int i = 0; i < assetPathList.Length; i++)
        {
            string assetName = string.Format(changeName + "_" + locationTag + "_" + i);
            SetName(assetPathList[i], assetName);
        }
    }

    private void OnClickGetPath()
    {
        string path = GetSelectedAssetPath();
        rootVisualElement.Query<Label>("ObjectPath").ForEach((label) =>
        {
            Debug.Log("path : " + path);
            label.text = path as string;
        });
    }

    private void SetName(string assetPath, string name)
    {
        Debug.Log("name : " + name);
        AssetDatabase.RenameAsset(assetPath, name);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private string GetSelectedAssetPath()
    {
        UnityEngine.Object selectObj = Selection.activeObject;
        string assetPath = AssetDatabase.GetAssetPath(selectObj.GetInstanceID());
        selectedObjectName = assetPath;
        Debug.Log("selected object asset path : " + selectedObjectName);
        return selectedObjectName;
    }
    
    // Asset path만 넣으면 name 반환
    private string GetAssetName(string assetPath)
    {
        return assetPath.Substring(assetPath.LastIndexOf("/") + 1);
    }

    private string GetAssetPath(string assetPath)
    {
        return assetPath.Substring(0, assetPath.LastIndexOf("/"));
    }
    
    // Place List 가져와서 생성
    private Location[] GetLocations()
    {
        string locationSOPath = "Assets/Editor/AssetEditor/ScriptableObjects";
        string[] result = AssetDatabase.FindAssets("t:object", new []{locationSOPath});
        foreach (var guid in result)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            string assetName = GetAssetName(path);
            Debug.Log("Asset Name : " + assetName);
            if (assetName.Equals("PlaceSO.asset"))
            {
                Debug.Log("Selected PalceSO");
                PlaceSO placeSo = (PlaceSO)AssetDatabase.LoadAssetAtPath(path, typeof(PlaceSO));
                return placeSo.Locations;
            }
        }
        return null;
    }
    
    // Change Name을 클릭하였을 경우, 어떤 라디오 버튼이 클릭되었는지 찾아야 함
    private int GetPlaceRadioButtonIndex()
    {
        int childCount = placeRadioButtonGroup.childCount;

        int selectedNumber = 0;
        for (int i = 2; i < childCount; i++)
        {
            RadioButton radioButton = (RadioButton)placeRadioButtonGroup[i];
            bool radioButtonValue = radioButton.value;
            Debug.Log(radioButtonValue);

            if (radioButtonValue)
            {
                selectedNumber = i - 2;
                Debug.Log("selected Number : " + selectedNumber);
                return selectedNumber;
            }
        }

        return -1;
    }
}
