using UnityEngine;
using System.Collections.Generic;
using XHFrameWork;
using System;

public class BagUIModule : BaseModule
{
    private List<ItemModule.ItemData> _itemData;
    Dictionary<ItemModule.EnumMainType, ItemModule.ItemData[]> tableDic = new Dictionary<ItemModule.EnumMainType, ItemModule.ItemData[]>();
    //int?[] tableOneID = new int?[25];//0-5的值，是数组_listItem的编号；
    //int?[] tableTwoID = new int?[25];
    //int?[] tableThreeID = new int?[25];
    private ItemModule.ItemData[] tableOneListItem = new ItemModule.ItemData[25];
    private ItemModule.ItemData[] tableTwoListItem = new ItemModule.ItemData[25];
    private ItemModule.ItemData[] tableThreeListItem = new ItemModule.ItemData[25];


    public void RemoveImageInfo(ItemModule.EnumMainType mainType, int num, uint count = 1)
    {
        for (int i = 0; i < 25; i++)
        {
            if (tableDic[mainType][i] == _itemData[num] && tableDic[mainType][i].Num > 0)
            {
                if (tableDic[mainType][i].Num > count)
                {
                    tableDic[mainType][i].Num -= count;
                }
                else
                {
                    tableDic[mainType][i] = null;
                    count -= tableDic[mainType][i].Num;
                    RemoveImageInfo(mainType, num, count);
                }
            }
        }
    }

    void AddImageInfo(ItemModule.EnumMainType mainType, int num, uint count = 1)
    {
        for (int i = 0; i < 25; i++)
        {
            if (count <= 0)
                return;
            if ((tableDic[mainType][i] == _itemData[num] && tableDic[mainType][i].Num < tableDic[mainType][i].OverlayNum) || tableDic[mainType][i] == null)
            {
                tableDic[mainType][i] = _itemData[num];
                if (tableDic[mainType][i].Num + count <= tableDic[mainType][i].OverlayNum)
                {
                    tableDic[mainType][i].Num += count;
                    return;
                }
                else
                {
                    count = (count + tableDic[mainType][i].Num) - tableDic[mainType][i].OverlayNum;
                    tableDic[mainType][i].Num = tableDic[mainType][i].OverlayNum;
                }
            }
        }
    }

    public int SetTableCount(ItemModule.EnumMainType mainType)
    {
        int count = 0;
        foreach (var item in tableDic[mainType])
        {
            if (item != null && item.Num != 0)
            {
                count++;
                //Debug.Log(item);
            }
        }
        return count;

    }

    public BagUIModule()
    {

        AutoRegister = true;
        _itemData = new List<ItemModule.ItemData>();
        tableDic.Add(ItemModule.EnumMainType.None, null);
        tableDic.Add(ItemModule.EnumMainType.ItemOne, tableOneListItem);
        tableDic.Add(ItemModule.EnumMainType.ItemTwo, tableTwoListItem);
        tableDic.Add(ItemModule.EnumMainType.ItemThree, tableThreeListItem);
    }


    #region Override method
    protected override void OnLoad()
    {
        base.OnLoad();
        //load data from setting data
        BaseData_ItemTable.Instance.LoadDataFromSettingTable();

        //create some item data(from network server) 
        CreateSomeItemData();


        foreach (int item in Enum.GetValues(typeof(ItemModule.EnumMainType)))
        {
            if (item == 0)
                continue;
            string typeName = Enum.GetName(typeof(ItemModule.EnumMainType), item);
            ItemModule.EnumMainType temp = (ItemModule.EnumMainType)Enum.Parse(typeof(ItemModule.EnumMainType), typeName);
            int num = UnityEngine.Random.Range(3, 5);
            //Debug.Log(num);
            for (int i = 0; i < num; i++)
            {
                ItemModule.ItemData itemModile = _itemData[UnityEngine.Random.Range(0, _itemData.Count)];
                //tableDic[temp][i] = itemModile;
                tableDic[temp][i] = MethodExtension.DeepClone<ItemModule.ItemData>(itemModile);

                //Debug.Log(_listItem.Count);

            }
        }
    }

    protected override void OnRelease()
    {
        base.OnRelease();

        _itemData.Clear();
        _itemData = null;

        //_dicSpritePrefab.Clear();
        //_dicSpritePrefab = null;
    }
    #endregion


    private void CreateSomeItemData()
    {
        //var item = new { id = 1, itemId = 10001, Num = (uint)Random.Range(300, 500), Lv = 0 };
        //1 HpBottle
        ItemModule.ItemData item = new ItemModule.ItemData() { Id = 1, ItemId = 10001, Num = (uint)UnityEngine.Random.Range(3, 5), Lv = 0 };
        _itemData.Add(item);

        //2 MpBottle
        item = new ItemModule.ItemData() { Id = 2, ItemId = 10002, Num = (uint)UnityEngine.Random.Range(3, 5), Lv = 0 };
        _itemData.Add(item);

        //3 sword
        item = new ItemModule.ItemData() { Id = 3, ItemId = 10003, Num = (uint)UnityEngine.Random.Range(3, 5), Lv = 0 };
        _itemData.Add(item);

        //4 cloth
        item = new ItemModule.ItemData() { Id = 4, ItemId = 10004, Num = (uint)UnityEngine.Random.Range(1, 5), Lv = (uint)UnityEngine.Random.Range(1, 10) };
        _itemData.Add(item);

        //5 gold
        item = new ItemModule.ItemData() { Id = 5, ItemId = 10005, Num = (uint)UnityEngine.Random.Range(1, 3), Lv = 0 };
        _itemData.Add(item);

        //5 book
        item = new ItemModule.ItemData() { Id = 6, ItemId = 10006, Num = (uint)UnityEngine.Random.Range(1, 5), Lv = 0 };
        _itemData.Add(item);
    }

    public List<ItemModule.ItemData> GetListAllItem()
    {
        return _itemData;
    }

    public List<ItemModule.ItemData> GetListByItemMainType(ItemModule.EnumMainType mainType)
    {//根据type相应的内容设置item的列表
        //Debug.Log("GetListByItemMainType");
        List<ItemModule.ItemData> rtnList = new List<ItemModule.ItemData>();
        foreach (var temp in tableDic[mainType])
        {
            if (temp == null)
                rtnList.Add(null);
            else
                rtnList.Add(temp);

        }
        return rtnList;
    }





    //#region save sprite resource temporary for current resmanger does not support Resource.Load(path,type) method
    //private static Dictionary<string, Sprite> _dicSpritePrefab = new Dictionary<string, Sprite>();
    //public static Sprite GetSpriteByPath(string path)
    //{
    //    if (_dicSpritePrefab == null)
    //    {
    //        _dicSpritePrefab = new Dictionary<string, Sprite>();
    //    }

    //    Sprite rtnSprite = null;
    //    if (_dicSpritePrefab.ContainsKey(path))
    //    {
    //        rtnSprite = _dicSpritePrefab[path];
    //    }
    //    else
    //    {
    //        UnityEngine.Object obj = Resources.Load(path, typeof(Sprite));
    //        if (obj == null)
    //        {
    //            Debug.Log("sprite path is not correct!! path:" + path);
    //        }
    //        else
    //        {
    //            rtnSprite = obj as Sprite;
    //            if (rtnSprite != null)
    //            {
    //                _dicSpritePrefab.Add(path, rtnSprite);
    //            }
    //            else
    //            {
    //                Debug.Log("sprite type not correct!! path:" + path);
    //            }
    //        }
    //    }

    //    return rtnSprite;
    //}
    //#endregion
}

//raw setting data
public class BaseData_ItemTable
{
    class RecordeData
    {
        public uint ItemId { get; set; }
        public string ItemName { get; set; }
        public uint MainType { get; set; }
        public uint SubType { get; set; }
        public uint Price { get; set; }
        public uint OverlayNum { get; set; }
        public string Description { get; set; }
        public float Effect { get; set; }
        public string Icon { get; set; }
    }


    #region instance & constructor
    private static BaseData_ItemTable _instance;
    public static BaseData_ItemTable Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BaseData_ItemTable();
            }

            return _instance;
        }
    }
    private BaseData_ItemTable()
    {
        _dicRecords = new Dictionary<uint, RecordeData>();
    }
    #endregion

    /// <summary>
    /// The res path.
    /// </summary>
    private readonly string ResPath = "txt/Table_BaseItem";

    /// <summary>
    /// dictionay for save basic record data
    /// </summary>
    private Dictionary<uint, RecordeData> _dicRecords;

    /// <summary>
    /// The _is load.
    /// </summary>
    private bool _isLoad = false;

    /// <summary>
    /// Loads the data from setting table.
    /// </summary>
    public void LoadDataFromSettingTable()
    {
        if (_isLoad)
        {
            return;
        }

        _isLoad = true;

        TextAsset txt = Resources.Load<TextAsset>(ResPath);
        if (txt == null)
        {
            Debug.Log("can't load setting table:Table_BaseItem,Path = " + ResPath);
        }
        else
        {
            foreach (var line in txt.text.Split("\r\n".ToCharArray()))
            {
                string[] values = line.Split(',');

                RecordeData recData = new RecordeData();
                recData.ItemId = GetValideData<uint>(values[0]);
                recData.ItemName = values[1];
                recData.MainType = GetValideData<uint>(values[2]);
                recData.SubType = GetValideData<uint>(values[3]);
                recData.Price = GetValideData<uint>(values[4]);
                recData.OverlayNum = GetValideData<uint>(values[5]);
                recData.Description = values[6];
                recData.Effect = GetValideData<float>(values[7]);
                recData.Icon = values[8];

                if (_dicRecords.ContainsKey(recData.ItemId))
                {
                    _dicRecords[recData.ItemId] = recData;
                }
                else
                {
                    _dicRecords.Add(recData.ItemId, recData);
                }
                //Debug.Log(_dicRecords.Count);
            }
        }
    }

    private T GetValideData<T>(string value) where T : struct
    {
        if (typeof(T) == typeof(uint))
        {
            uint ret = 0;
            if (!uint.TryParse(value, out ret))
            {
                //Debug.Log("not set correct val")
            }

            object retObj = ret;
            return (T)retObj;
        }
        else if (typeof(T) == typeof(float))
        {
            float ret = 0;
            if (!float.TryParse(value, out ret))
            {
                //Debug.Log("not set correct val")
            }

            object retObj = ret;
            return (T)retObj;
        }
        else
        {
            Debug.Log("not realize method for type:" + typeof(T));
            return default(T);
        }
    }

    #region get value by itemId
    public string ItemName { get; private set; }
    public uint MainType { get; private set; }
    public uint SubType { get; private set; }
    public uint Price { get; private set; }
    public uint OverlayNum { get; private set; }
    public string Description { get; private set; }
    public float Effect { get; private set; }
    public string Icon { get; private set; }

    public string GetItemName(uint itemId)
    {
        return _dicRecords.ContainsKey(itemId) ? _dicRecords[itemId].ItemName : string.Empty;
    }

    public uint GetMainType(uint itemId)
    {
        return _dicRecords.ContainsKey(itemId) ? _dicRecords[itemId].MainType : 0;
    }

    public uint GetSubType(uint itemId)
    {
        return _dicRecords.ContainsKey(itemId) ? _dicRecords[itemId].SubType : 0;
    }
    public uint GetPrice(uint itemId)
    {
        return _dicRecords.ContainsKey(itemId) ? _dicRecords[itemId].Price : 0;
    }
    public uint GetOverlayNum(uint itemId)
    {
        return _dicRecords.ContainsKey(itemId) ? _dicRecords[itemId].OverlayNum : 0;
    }
    public string GetDescription(uint itemId)
    {
        return _dicRecords.ContainsKey(itemId) ? _dicRecords[itemId].Description : string.Empty;
    }
    public float GetEffect(uint itemId)
    {
        return _dicRecords.ContainsKey(itemId) ? _dicRecords[itemId].Effect : 0f;
    }
    public string GetIcon(uint itemId)
    {
        return _dicRecords.ContainsKey(itemId) ? _dicRecords[itemId].Icon : string.Empty;
    }
    #endregion

}