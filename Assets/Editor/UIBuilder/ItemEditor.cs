using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class ItemEditor : EditorWindow
{
    //[SerializeField]
    //private VisualTreeAsset m_VisualTreeAsset = default;

    private ItemDataList_SO dataBase;

    private VisualTreeAsset itemRowTemplate;

    private ListView itemListView;

    private List<ItemDetails> itemList = new List<ItemDetails>();

    [MenuItem("AstroWYH/ItemEditor")]
    public static void ShowExample()
    {
        ItemEditor wnd = GetWindow<ItemEditor>();
        wnd.titleContent = new GUIContent("ItemEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        //VisualElement label = new Label("Hello World! From C#");
        //root.Add(label);

        // Instantiate UXML
        //VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        //root.Add(labelFromUXML);

        // ItemEditor的面板
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UIBuilder/ItemEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        // Item模板的面板赋值
        itemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UIBuilder/ItemRowTemplate.uxml");

        // ItemListView赋值
        itemListView = root.Q<VisualElement>("ItemList").Q<ListView>("ListView");

        LoadDataBase();

        GenerateListView();
    }

    private void LoadDataBase()
    {
        var dataArray = AssetDatabase.FindAssets("ItemDataList_SO");
        if (dataArray.Length > 1)
        {
            var path = AssetDatabase.GUIDToAssetPath(dataArray[0]);
            dataBase = AssetDatabase.LoadAssetAtPath(path, typeof(ItemDataList_SO)) as ItemDataList_SO;
        }

        itemList = dataBase.itemDetailsList;
        // 如果不标记，则无法保存数据
        EditorUtility.SetDirty(dataBase);
        Debug.Log(itemList[0].itemID);
    }

    private void GenerateListView()
    {
        // 定义一个 Func 委托，它返回一个 VisualElement 对象。这个委托表示如何创建列表中的每一项。
        Func<VisualElement> makeItem = () => itemRowTemplate.CloneTree();

        // 定义一个 Action 委托，它接受一个 VisualElement 对象和一个整数作为参数。这个委托表示如何将数据绑定到列表中的每一项。
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            if (i < itemList.Count)
            {
                // 如果数据列表中的项存在，则将项的图标和名称绑定到列表项中。
                if (itemList[i].itemIcon != null)
                    e.Q<VisualElement>("Icon").style.backgroundImage = itemList[i].itemIcon.texture;
                e.Q<Label>("Name").text = itemList[i] == null ? "NO ITEM" : itemList[i].itemName;
            }
        };

        itemListView.fixedItemHeight = 60;

        // 将数据源设置为 itemList
        itemListView.itemsSource = itemList;

        // 将创建项的委托设置为 makeItem
        itemListView.makeItem = makeItem;

        // 将绑定项的委托设置为 bindItem
        itemListView.bindItem = bindItem;
    }

}
