using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XHFrameWork;
using System.Collections.Generic;
using System.Linq;
using System;

class BagUI : BaseUI
{
    private readonly string TOGGLE_TAB_ONE_PATH = "Canvas(Clone)/bgImage/RightArea/Tab1";
    private readonly string TOGGLE_TAB_TWO_PATH = "Canvas(Clone)/bgImage/RightArea/Tab2";
    private readonly string TOGGLE_TAB_THREE_PATH = "Canvas(Clone)/bgImage/RightArea/Tab3";
    private readonly string COUNT_TEXT_THREE_PATH = "Canvas(Clone)/bgImage/RightArea/CountNum/box/Counts/CountText";

    private readonly string GRID_PREFAB_PATH = "Prefabs/BoardTile";
    private readonly string ITEM_PREFAB_PATH = "Prefabs/Imgae_Item";
    private readonly string ITEM_PANEL_PATH = "Canvas(Clone)/bgImage/RightArea/Panel/BagUI(Clone)";

    private readonly int ITEM_COUNT_PER_SHEET = 5 * 3;

    private BagUIModule _bagUIModule;

    private GameObject _tabOne;
    private GameObject _tabTwo;
    private GameObject _tabThree;
    GameObject _pagCount;

    private UnityEngine.Object _goPrefabGrid;
    private UnityEngine.Object _goPrefabItem;

    private GameObject _goItemPanel;


    private Toggle _toggleOne;
    private Toggle _toggleTwo;
    private Toggle _toggleThree;

    #region implemented abstract members of BaseUI
    public override EnumUIType GetUIType()
    {
        return EnumUIType.BagUI;
    }
    #endregion

    #region override method
    protected override void OnAwake()
    {
        base.OnAwake();
        _pagCount = GameObject.Find(COUNT_TEXT_THREE_PATH);
        _tabOne = GameObject.Find(TOGGLE_TAB_ONE_PATH);
        _tabTwo = GameObject.Find(TOGGLE_TAB_TWO_PATH);
        _tabThree = GameObject.Find(TOGGLE_TAB_THREE_PATH);
        _goPrefabGrid = Resources.Load(GRID_PREFAB_PATH);
        _goPrefabItem = Resources.Load(ITEM_PREFAB_PATH);
        _goItemPanel = GameObject.Find(ITEM_PANEL_PATH);

        _toggleOne = _tabOne.GetComponent<Toggle>();
        _toggleOne.onValueChanged.AddListener((isOn) => { OnToggleValueChanged(_toggleOne, isOn); });

        _toggleTwo = _tabTwo.GetComponent<Toggle>();
        _toggleTwo.onValueChanged.AddListener((isOn) => { OnToggleValueChanged(_toggleTwo, isOn); });

        _toggleThree = _tabThree.GetComponent<Toggle>();
        _toggleThree.onValueChanged.AddListener((isOn) => { OnToggleValueChanged(_toggleThree, isOn); });


    }
    protected override void OnLoadData()
    {
        base.OnLoadData();
        _bagUIModule = ModuleManager.Instance.Get<BagUIModule>();
        SetData();
    }

    protected override void OnRelease()
    {
        base.OnRelease();
        _queueItem.Clear();
        _queueItem = null;
        _queueGrid.Clear();
        _queueGrid = null;
        _listItem.Clear();
        _listItem = null;
        _listGrid.Clear();
        _listGrid = null;
        _listToggleSheet.Clear();
        _listToggleSheet = null;
    }
    #endregion

    private void OnToggleValueChanged(Toggle sender, bool isOn)
    {
        //Text text = sender.transform.Find("Text").GetComponent<Text>();
        //text.color = isOn ? new Color(0x8C / 255.0f, 0xBC / 255.0f, 0xC8 / 255.0f) : Color.black;
        if (isOn)
        {
            SetData();
        }
    }

    #region SetItem data
    private bool _isSettingData = false;

    //save sheet & item
    private List<GameObject> _listGrid = new List<GameObject>();
    private List<Item> _listItem = new List<Item>();

    //pool sheet & item
    /// <summary>
    /// grid集合，先进先出
    /// </summary>
    private Queue<GameObject> _queueGrid = new Queue<GameObject>();
    private Queue<GameObject> _queueItem = new Queue<GameObject>();

    //toggle sheet
    private List<Toggle> _listToggleSheet = new List<Toggle>();

    /// <summary>
    /// 获取_queueGrid中第一个对象
    /// </summary>
    /// <returns></returns>
    private GameObject GetOneGrid()
    {
        GameObject goRtn = null;

        if (_queueGrid.Count > 0)
        {
            goRtn = _queueGrid.Dequeue();//Queue<T>.Dequeue 移除，并返回位于Queue<T>开始位置的对象；
        }
        else
        {
            goRtn = Instantiate(_goPrefabGrid) as GameObject;
        }
        goRtn.transform.DestroyAllChildren();
        //ClearAllChildren(goRtn.transform);
        return goRtn;
    }

    /// <summary>
    /// 获取_queueItem中第一个对象
    /// </summary>
    /// <returns></returns>
    private Item GetOneItem()
    {
        Item rtnItem = null;

        if (_queueItem.Count > 0)
        {
            rtnItem = _queueItem.Dequeue().GetComponent<Item>();
        }
        else
        {
            GameObject obj = Instantiate(_goPrefabItem) as GameObject;
            rtnItem = obj.AddComponent<Item>();
        }
        rtnItem.transform.ClearAllChildren();
        return rtnItem;
    }


    GameObject tempGO = null;
    Item tempItem = null;
    ItemModule.EnumMainType mainType;

    public void SetData()
    {
        //Debug.Log("SetData");
        _isSettingData = true;
        List<ItemModule.ItemData> listItem = null;

        if (_toggleTwo.isOn)
            mainType = ItemModule.EnumMainType.ItemTwo;
        else if (_toggleThree.isOn)
            mainType = ItemModule.EnumMainType.ItemThree;
        else
            mainType = ItemModule.EnumMainType.ItemOne;

        listItem = _bagUIModule.GetListByItemMainType(mainType);
        AddBoardTile();
        AddItem();
        ShowAllImage(listItem);
        _pagCount.GetComponent<Text>().text = _bagUIModule.SetTableCount(mainType).ToString();
    }

    private void AddItem()
    {
        if (_listItem.Count == ITEM_COUNT_PER_SHEET)
        {
            return;
        }
        else
        {
            foreach (var item in _listGrid)
            {
                tempItem = GetOneItem();
                SetInfo(tempItem.gameObject, item.transform, Vector3.one);
                _listItem.Add(tempItem);
                tempItem.transform.SetAsLastSibling();

            }
        }
    }

    private void AddBoardTile()
    {
        if (_listGrid.Count == ITEM_COUNT_PER_SHEET)
            return;
        else
        {
            for (int num = _listGrid.Count; num < ITEM_COUNT_PER_SHEET; num++)
            {
                tempGO = GetOneGrid();
                SetInfo(tempGO, _goItemPanel.transform, Vector3.one);
                _listGrid.Add(tempGO);

                //must set last for ugui
                tempGO.transform.SetAsLastSibling();
            }
        }
    }

    private void SetInfo(GameObject gameObject, Transform parentTran, Vector3 pos )
    {
        gameObject.transform.SetParent(parentTran);
        gameObject.transform.localScale = pos;
        gameObject.SetActive(true);
        
    }

    private void ShowAllImage(List<ItemModule.ItemData> listItem)
    {

        for (int i = 0; i < _listItem.Count; i++)
        {
            //if (listItem[i] != null)
            //{
                _listItem[i].GetComponent<Item>().SetItemData(listItem[i]);
                //Debug.Log(listItem[i]);
                //Debug.Log(listItem[i].GetHashCode());
            //}

            
        }

        
    }
    
    #endregion



}

