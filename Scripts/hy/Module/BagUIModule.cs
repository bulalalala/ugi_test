using UnityEngine;
using System.Collections.Generic;
using XHFrameWork;
using System;

public class BagUIModule : BaseModule
{
    private List<ItemModule> _itemData;
    Dictionary<ItemModule.EnumMainType, ItemModule[]> tableDic = new Dictionary<ItemModule.EnumMainType, ItemModule[]>();
    //int?[] tableOneID = new int?[25];//0-5的值，是数组_listItem的编号；
    //int?[] tableTwoID = new int?[25];
    //int?[] tableThreeID = new int?[25];
    private ItemModule[] tableOneListItem = new ItemModule[25];
    private ItemModule[] tableTwoListItem = new ItemModule[25];
    private ItemModule[] tableThreeListItem = new ItemModule[25];


    //public void RemoveImageInfo(ItemModule.EnumMainType mainType, int num, uint count = 1)
    //{
    //    for (int i = 0; i < 25; i++)
    //    {
    //        if (tableDic[mainType][i] == _itemData[num] && tableDic[mainType][i].Num > 0)
    //        {
    //            if (tableDic[mainType][i].Num > count)
    //            {
    //                tableDic[mainType][i].Num -= count;
    //            }
    //            else
    //            {
    //                tableDic[mainType][i] = null;
    //                count -= tableDic[mainType][i].Num;
    //                RemoveImageInfo(mainType, num, count);
    //            }
    //        }
    //    }
    //}
    public void DecreaseImageNum(ItemModule itemModule, ItemModule.EnumMainType mainType, uint count = 1)
    {
        if (itemModule._ItemData.Num > 0)
        {
            if (itemModule._ItemData.Num >= count)
            {
                itemModule._ItemData.Num -= count;
            }
            else if (itemModule._ItemData.Num - count == 0)
            {
                itemModule._ItemData.Num = 0;
                itemModule = null;
            }
        }
    }


    public void AddImageNum(ItemModule.ItemData itemData, ItemModule.EnumMainType mainType, uint count = 1)
    {
        Debug.Log(itemData);

        if (itemData.Num < 20)
        {
            itemData.Num += count;
        }
        else
        {
            for (int i = 0; i < tableDic[mainType].Length; i++)
            {

                if (tableDic[mainType][i]._ItemData.Id == itemData.Id && tableDic[mainType][i]._ItemData.Num < 20)
                {
                    //tableDic[mainType][i].AddImageNum();
                    AddImageNum(tableDic[mainType][i]._ItemData, mainType);
                    return;
                }

            }
            for (int i = 0; i < tableDic[mainType].Length; i++)
            {

                if (tableDic[mainType][i] == null)
                {
                    tableDic[mainType][i] = new ItemModule() { _ItemData = MethodExtension.DeepClone<ItemModule.ItemData>(itemData) };
                    tableDic[mainType][i]._ItemData.Num = count;
                    return;
                }

            }
            
        }
        
    }

    public int SetTableCount(ItemModule.EnumMainType mainType)
    {
        int count = 0;
        foreach (var item in tableDic[mainType])
        {
            if (item != null && item._ItemData.Num != 0)
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
        _itemData = new List<ItemModule>();
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
                ItemModule.ItemData itemModile = _itemData[UnityEngine.Random.Range(0, _itemData.Count)]._ItemData;
                //tableDic[temp][i] = itemModile;
                //tableDic[temp][i]._ItemData = MethodExtension.DeepClone(itemModile);
                tableDic[temp][i] = new ItemModule() { _ItemData = MethodExtension.DeepClone(itemModile) };

                //Debug.Log(_listItem.Count);

            }
            //for (int i = num; i < tableDic[temp].Length - num; i++)
            //{
            //    tableDic[temp][i] = new ItemModule() { _ItemData = new ItemModule.ItemData() };
            //    Debug.Log(tableDic[temp][i]._ItemData == null);
            //}
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
        ItemModule item = new ItemModule() { _ItemData = new ItemModule.ItemData() { Id = 1, ItemId = 10001, Num = (uint)UnityEngine.Random.Range(3, 5), Lv = 0 } };
        _itemData.Add(item);

        //2 MpBottle
        item = new ItemModule() { _ItemData = new ItemModule.ItemData() { Id = 2, ItemId = 10002, Num = (uint)UnityEngine.Random.Range(3, 5), Lv = 0 } };
        _itemData.Add(item);

        //3 sword
        item = new ItemModule() { _ItemData = new ItemModule.ItemData() { Id = 3, ItemId = 10003, Num = (uint)UnityEngine.Random.Range(3, 5), Lv = 0 } };
        _itemData.Add(item);

        //4 cloth
        item = new ItemModule() { _ItemData = new ItemModule.ItemData() { Id = 4, ItemId = 10004, Num = (uint)UnityEngine.Random.Range(1, 5), Lv = (uint)UnityEngine.Random.Range(1, 10) } };
        _itemData.Add(item);

        //5 gold
        item = new ItemModule() { _ItemData = new ItemModule.ItemData() { Id = 5, ItemId = 10005, Num = (uint)UnityEngine.Random.Range(1, 3), Lv = 0 } };
        _itemData.Add(item);

        //5 book
        item = new ItemModule() { _ItemData = new ItemModule.ItemData() { Id = 6, ItemId = 10006, Num = (uint)UnityEngine.Random.Range(1, 5), Lv = 0 } };
        _itemData.Add(item);
    }

    public List<ItemModule> GetListAllItem()
    {
        return _itemData;
    }

    public List<ItemModule> GetListByItemMainType(ItemModule.EnumMainType mainType)
    {//根据type相应的内容设置item的列表
        //Debug.Log("GetListByItemMainType");
        List<ItemModule> rtnList = new List<ItemModule>();
        foreach (var temp in tableDic[mainType])
        {
            if (temp == null)
                rtnList.Add(null);
            else
                rtnList.Add(temp);

        }
        return rtnList;
    }


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