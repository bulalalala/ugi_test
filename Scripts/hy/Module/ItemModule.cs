using UnityEngine;
using System.Collections;
using XHFrameWork;
using System;


public class ItemModule : BaseModule
{
    ItemModule _itemModule;
    public ItemData _ItemData;
    bool isChanged = true;
    public enum EnumMainType
    {
        None = 0,
        ItemOne = 1,
        ItemTwo = 2,
        ItemThree = 3,
    }

    public ItemModule()
    {
        AutoRegister = true;
    }
    #region override method
    protected override void OnLoad()
    {
        base.OnLoad();
        Debug.Log("OnLoad");
        //StartCoroutine----MonoBehaviour
        //_interface.StartCoroutine(AutoUpdateItem());//NullReferenceException,继承自Mono的类，都无法使用new创建，只能使用创建的GameObject通过GetComponent<>()方法获得脚本
        CoroutineController.Instance.StartCoroutine(AutoUpdateItem());
    }

    protected override void OnRelease()
    {
        base.OnRelease();
        _ItemData = null;
        _itemModule = null;
    }

    #endregion

    private IEnumerator AutoUpdateItem()
    {
        //int gold = 0;
        while (true)
        {
            //    gold++;
            yield return _itemModule;
            if (isChanged)
            {
                Message message = new Message(MessageType.Net_MessageItem.ToString(), this);
                message["Item"] = _itemModule;
                MessageCenter.Instance.SendMessage(message);
                isChanged = false;
            }
            
            //Debug.Log("AutoUpdateItem");
        }
    }


    public void RemoveImageNum(uint count = 1)
    {
        if (_ItemData.Num > 0)
        {
            if (_ItemData.Num >= count)
            {
                _ItemData.Num -= count;
            }
            else if (_ItemData.Num - count == 0)
            {
                _itemModule._ItemData = null;
                _ItemData.Num = 0;
            }
            else
            {
                Debug.Log("num is not enough!");
            }
        }
        isChanged = true;
        //Debug.Log(_ItemData.Num);
    }

    public void AddImageNum(uint count = 1)
    {
        if (_ItemData.Num < _ItemData.OverlayNum)
        {
            _ItemData.Num += count;
        }
        else
        {
            Debug.Log("num is overfllow");
        }
        isChanged = true;
    }
    [Serializable]
    public class ItemData 
    {

        #region Property

        public uint ItemId { get; set; }//get item info from setting table by itemid
        public uint Lv { get; set; }
        public uint Num { get; set; }

        //basic data
        private string _itemName;
        public string ItemName
        {
            get
            {
                if (string.IsNullOrEmpty(_itemName))
                {
                    _itemName = BaseData_ItemTable.Instance.GetItemName(ItemId);
                }

                return _itemName;
            }
        }

        private EnumMainType _mainType = EnumMainType.None;
        public EnumMainType MainType
        {
            get
            {
                if (_mainType == EnumMainType.None)
                {
                    _mainType = (EnumMainType)BaseData_ItemTable.Instance.GetMainType(ItemId);
                }

                return _mainType;
            }
        }

        private uint _subType = 0;
        public uint SubType
        {
            get
            {
                if (_subType == 0)
                {
                    _subType = BaseData_ItemTable.Instance.GetSubType(ItemId);
                }

                return _subType;
            }
        }




        private uint? _overlayNum = null;
        public uint OverlayNum
        {
            get
            {
                if (!_overlayNum.HasValue)
                {
                    _overlayNum = BaseData_ItemTable.Instance.GetOverlayNum(ItemId);
                }

                return _overlayNum.Value;
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                if (string.IsNullOrEmpty(_description))
                {
                    _description = BaseData_ItemTable.Instance.GetDescription(ItemId);
                }

                return _description;
            }
        }

        private float? _effect = null;
        public float Effect
        {
            get
            {
                if (!_effect.HasValue)
                {
                    _effect = BaseData_ItemTable.Instance.GetEffect(ItemId);
                }

                return _effect.Value;
            }
        }

        #endregion

        # region imageInfo

        public uint Id { get; set; }   //identify num

        private string _icon;
        public string Icon
        {
            get
            {
                if (string.IsNullOrEmpty(_icon))
                {
                    _icon = BaseData_ItemTable.Instance.GetIcon(ItemId);
                }

                return _icon;
            }
        }

        private uint? _price = null;
        public uint Price
        {
            get
            {
                if (!_price.HasValue)
                {
                    _price = BaseData_ItemTable.Instance.GetPrice(ItemId);
                }

                return _price.Value;
            }
        }

        #endregion

    }
}
