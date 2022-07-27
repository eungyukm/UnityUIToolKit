using System.Collections;
using System.Net;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEngine.Networking;

public class AssetDataSetEditor : EditorWindow
{
    [MenuItem("Tools/DataSetEditor")]
    public static void ShowWindow()
    {
        GetWindow<AssetDataSetEditor>();
    }
    
    string path = "Assets/Editor/AssetEditor/UIDocument/ExcelSetEditor.uxml";

    private string url = "https://docs.google.com/spreadsheets/d/1TrqGt5divJHVf81eeX8iz5IUXUjt9mLYuZsZGzsfRe8/edit?usp=sharing";
    public int count = 6;
    
    private void OnEnable()
    {
        var asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
        // UXML을 EditorWindow에 배치
        asset.CloneTree(rootVisualElement);

        rootVisualElement.Query<Button>("ExcelSetBtn").ForEach((button) =>
        {
            button.clicked += DownloadItemSO;
        });
    }
    
    private void DownloadItemSO()
    {
        rootVisualElement.Query<TextField>("ExcelURLField").ForEach((TextField) =>
        {
            url = TextField.text;
        });
        
        rootVisualElement.Query<TextField>("DataCountField").ForEach((TextField) =>
        {
            count = int.Parse(TextField.text);
        });

        if (string.IsNullOrEmpty(url))
        {
            return;
        }
        
        WebClient wc = new WebClient();
        wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:22.0) Gecko/20100101 Firefox/22.0");
        wc.Headers.Add("DNT", "1");
        wc.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        wc.Headers.Add("Accept-Encoding", "deflate");
        wc.Headers.Add("Accept-Language", "en-US,en;q=0.5");
        var data = wc.DownloadString(url);
        //Debug.Log(data);

        PlaceSO placeSo = new PlaceSO();
        placeSo.Locations = new Location[count];
        
        string[] row = data.Split('\n');
        for (int i = 3; i < count + 3; i++) {
            string[] column = row[i].Split(',');
            string placeName = column[0];
            string placeTag = column[1];
            Debug.Log(placeName);
            Debug.Log(placeTag);
            placeSo.Locations[i-3].placeName = placeName;
            placeSo.Locations[i-3].placeTag = placeTag;
        }

        foreach (var value in placeSo.Locations)
        {
            Debug.Log(value.placeName);
        }
        
        AssetDatabase.CreateAsset(placeSo, "Assets/Editor/AssetEditor/ScriptableObjects/PlaceSO.asset");
        AssetDatabase.Refresh();
    }
}
