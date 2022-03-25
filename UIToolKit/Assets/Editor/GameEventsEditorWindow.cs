using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

public class GameEventsEditorWindow : EditorWindow
{
    [MenuItem("Tools/GameEvents/GameEventsWindow")]
    public static void ShowGameEventsWindow()
    {
        GetWindow<GameEventsEditorWindow>();
    }

    private void OnEnable()
    {
        // UXML 경로
        string path = "Assets/Editor/GameEventMenu.uxml";

        var asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);

        // UXML을 EditorWindow에 배치
        asset.CloneTree(rootVisualElement);

        // EditorWindow 아래에 있는 AddBtn 처리
        rootVisualElement.Query<Button>("BtnScenePath").
            ForEach((button) =>
           {
               button.clicked += OnClickPathInScenesBtn;
           });

        rootVisualElement.Query<Button>("BtnAssetPath").
            ForEach((button) =>
            {
                button.clicked += OnClickPahtInAssetsBtn;
            });

        rootVisualElement.Query<Button>("BtnAdd").
            ForEach((button) =>
            {
                button.clicked += OnClickAddBtn;
            });

        rootVisualElement.Query<Button>("BtnDelete").
            ForEach((button) =>
            {
                button.clicked += OnClickDeleteBtn;
            });
    }

    private void OnClickPathInScenesBtn()
    {
        Debug.Log("Path in Scenes Button Click!!");
    }

    private void OnClickPahtInAssetsBtn()
    {
        Debug.Log("Path in Assets Button Click!!");
    }

    private void OnClickAddBtn()
    {
        Debug.Log("Click Add Btn!!");

        // TextField에서 문자열을 가져옵니다.
        TextField textField = rootVisualElement.Query<TextField>().AtIndex(0);

        string textValue = textField.value;

        // Label을 동적으로 생성
        var newLineLabel = new Label(textValue);

        // 삭제 시 Query에서 찾을 수 있도록 myitem class 추가
        newLineLabel.AddToClassList("myitem");

        // ScrollList에 생성된 label 추가
        rootVisualElement.Query<ScrollView>("SceneAreaScroll").
            AtIndex(0).Add(newLineLabel);
    }

    private void OnClickDeleteBtn()
    {
        Debug.Log("Delete Btn Click!!");

        // ScrollView 가져오기
        ScrollView scrollView =
            rootVisualElement.Query<ScrollView>("SceneAreaScroll").AtIndex(0);

        // ScrollView에 있는 myitem 중 마지막 element를 선택합니다.
        var element =
            scrollView.Query<VisualElement>(null, "myitem").Last();

        if(element != null)
        {
            // 해당 element를 parent에서 삭제
            element.parent.Remove(element);
        }
    }
}
