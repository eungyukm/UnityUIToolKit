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
        // UXML URL
        string path = "Assets/Editor/GameEventMenu.uxml";

        var asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);

        // UXML Clone
        asset.CloneTree(rootVisualElement);

        // EditorWindow �Ʒ��� �ִ� AddBtn ó��
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

        // TextField���� ���ڿ��� �����ɴϴ�.
        TextField textField = rootVisualElement.Query<TextField>().AtIndex(0);

        string textValue = textField.value;

        // Label�� �������� ����
        var newLineLabel = new Label(textValue);

        // ���� �� Query���� ã�� �� �ֵ��� myitem class �߰�
        newLineLabel.AddToClassList("myitem");

        // ScrollList�� ������ label �߰�
        rootVisualElement.Query<ScrollView>("SceneAreaScroll").
            AtIndex(0).Add(newLineLabel);
    }

    private void OnClickDeleteBtn()
    {
        Debug.Log("Delete Btn Click!!");

        // ScrollView ��������
        ScrollView scrollView =
            rootVisualElement.Query<ScrollView>("SceneAreaScroll").AtIndex(0);

        // ScrollView�� �ִ� myitem �� ������ element�� �����մϴ�.
        var element =
            scrollView.Query<VisualElement>(null, "myitem").Last();

        if(element != null)
        {
            // �ش� element�� parent���� ����
            element.parent.Remove(element);
        }
    }
}
