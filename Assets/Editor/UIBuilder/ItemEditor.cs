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

        // ItemEditor�����
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UIBuilder/ItemEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        // Itemģ�����帳ֵ
        itemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UIBuilder/ItemRowTemplate.uxml");

        // ItemListView��ֵ
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
        // �������ǣ����޷���������
        EditorUtility.SetDirty(dataBase);
        Debug.Log(itemList[0].itemID);
    }

    private void GenerateListView()
    {
        // ����һ�� Func ί�У�������һ�� VisualElement �������ί�б�ʾ��δ����б��е�ÿһ�
        Func<VisualElement> makeItem = () => itemRowTemplate.CloneTree();

        // ����һ�� Action ί�У�������һ�� VisualElement �����һ��������Ϊ���������ί�б�ʾ��ν����ݰ󶨵��б��е�ÿһ�
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            if (i < itemList.Count)
            {
                // ��������б��е�����ڣ������ͼ������ư󶨵��б����С�
                if (itemList[i].itemIcon != null)
                    e.Q<VisualElement>("Icon").style.backgroundImage = itemList[i].itemIcon.texture;
                e.Q<Label>("Name").text = itemList[i] == null ? "NO ITEM" : itemList[i].itemName;
            }
        };

        itemListView.fixedItemHeight = 60;

        // ������Դ����Ϊ itemList
        itemListView.itemsSource = itemList;

        // ���������ί������Ϊ makeItem
        itemListView.makeItem = makeItem;

        // �������ί������Ϊ bindItem
        itemListView.bindItem = bindItem;
    }

}
