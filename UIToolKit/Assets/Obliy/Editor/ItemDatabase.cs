using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemDatabase : EditorWindow
{
    private Sprite _defaultItemIcon;
    private static List<Item> _itemDatabase = new List<Item>();
    
    private VisualElement _itemsTab;
    private static VisualTreeAsset _itemRowTemplate;
    private ListView _itemListView;
    private float _itemHeight = 60f;

    private ScrollView _detailSection;
    private VisualElement _largeDisplayIcon;
    private Item _activeItem;

    [MenuItem("Item/Item Database")]
    public static void Init()
    {
        ItemDatabase wnd = GetWindow<ItemDatabase>();
        wnd.titleContent = new GUIContent("Item Database");

        Vector2 size = new Vector2(800, 475);
        wnd.minSize = size;
        wnd.maxSize = size;
    }

    public void CreateGUI()
    {
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Obliy/Editor/ItemDatabase.uxml");
        VisualElement rootFromUXML = visualTree.Instantiate();
        rootVisualElement.Add(rootFromUXML);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Obliy/Editor/ItemDatabase.uss");
        rootVisualElement.styleSheets.Add(styleSheet);

        _itemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Obliy/Editor/ItemRowTemplate.uxml");
        _defaultItemIcon =
            (Sprite) AssetDatabase.LoadAssetAtPath("Assets/Obliy/Editor/Sprites/UnknownIcon.png", typeof(Sprite));

        LoadAllItems();
        
        _itemsTab = rootVisualElement.Q<VisualElement>("ItemsTab");
        GenerateListView();
        
        _detailSection = rootVisualElement.Q<ScrollView>("ScrollView_Details");
        _detailSection.style.visibility = Visibility.Hidden;
        _largeDisplayIcon = _detailSection.Q<VisualElement>("Icon");

        rootVisualElement.Q<Button>("Btn_AddItem").clicked += AddItem_OnClick;

        _detailSection.Q<TextField>("ItemName").RegisterValueChangedCallback(evt =>
        {
            _activeItem.FriendlyName = evt.newValue;
            _itemListView.Rebuild();
        });

        _detailSection.Q<ObjectField>("IconPicker").RegisterValueChangedCallback(evt =>
        {
            Sprite newSprite = evt.newValue as Sprite;
            
            _activeItem.Icon = newSprite == null ? _defaultItemIcon : newSprite;
            _largeDisplayIcon.style.backgroundImage = newSprite == null ? _defaultItemIcon.texture : newSprite.texture;
            _itemListView.Rebuild();
        });

        rootVisualElement.Q<Button>("Btn_DeleteItem").clicked += DeleteItem_OnClick;
    }

    private void LoadAllItems()
    {
        _itemDatabase.Clear();
    
        string[] allPaths = Directory.GetFiles("Assets/Obliy/Data", "*.asset", SearchOption.AllDirectories);
    
        foreach (var path in allPaths)
        {
            string cleanedPath = path.Replace("\\", "/");
            _itemDatabase.Add((Item)AssetDatabase.LoadAssetAtPath(cleanedPath,typeof(Item)));
        }
    }
    
    private void GenerateListView()
    {
        Func<VisualElement> makeItem = () => _itemRowTemplate.CloneTree();
    
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            e.Q<VisualElement>("Icon").style.backgroundImage =
                _itemDatabase[i] == null ? _defaultItemIcon.texture : _itemDatabase[i].Icon.texture;
    
            e.Q<Label>("Name").text = _itemDatabase[i].FriendlyName;
        };
    
        _itemListView = new ListView(_itemDatabase, _itemHeight, makeItem, bindItem);
        _itemListView.selectionType = SelectionType.Single;
        _itemListView.style.height = _itemDatabase.Count * _itemHeight;
        _itemsTab.Add(_itemListView);

        _itemListView.onSelectionChange += ListView_OnSelectionChange;
    }

    private void ListView_OnSelectionChange(IEnumerable<object> selectedItems)
    {
        _activeItem = (Item) selectedItems.First();

        SerializedObject so = new SerializedObject(_activeItem);
        _detailSection.Bind(so);

        if (_activeItem.Icon != null)
        {
            _largeDisplayIcon.style.backgroundImage = _activeItem.Icon.texture;
        }

        _detailSection.style.visibility = Visibility.Visible;
    }

    private void AddItem_OnClick()
    {
        Item newItem = CreateInstance<Item>();
        newItem.FriendlyName = $"New Item";
        newItem.Icon = _defaultItemIcon;
        
        AssetDatabase.CreateAsset(newItem, $"Assets/Obliy/Data/{newItem.ID}.asset");
        
        _itemDatabase.Add(newItem);
        
        _itemListView.Rebuild();
        _itemListView.style.height = _itemDatabase.Count * _itemHeight;
    }

    private void DeleteItem_OnClick()
    {
        string path = AssetDatabase.GetAssetPath(_activeItem);
        AssetDatabase.DeleteAsset(path);

        _itemDatabase.Remove(_activeItem);
        _itemListView.Rebuild();

        _detailSection.style.visibility = Visibility.Hidden;
    }
}
