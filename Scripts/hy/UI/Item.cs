using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XHFrameWork;
using System;

public class Item : BaseUI
{
    private ItemModule _itemModule = new ItemModule();
    Image _image ;
    BagUI _bagui;
    KeyCode currentKey;
    #region implemented abstract members of BaseUI
    public override EnumUIType GetUIType()
    {
        return EnumUIType.Item;
    }
    #endregion
	private Image _imgItem;
	public Image ImgItem
	{
		get
		{
			if(_imgItem == null)
			{
                _imgItem = transform.GetComponent<Image>();

            }
            return _imgItem;
		}
	}



    private Text _textItemNum;
	public Text TextItemNum
	{
		get
		{
			if(_textItemNum == null)
			{
                _textItemNum = ImgItem.GetComponentInChildren<Text>();
            }

			return _textItemNum;
		}
	}


    public void SetItemData(ItemModule.ItemData item, uint setNum = 1)
    {
        //Debug.Log("SetItemData");
        _itemModule._ItemData = item;

        if (_itemModule._ItemData == null || _itemModule._ItemData.Num == 0)
        {
            if (!_image.enabled && TextItemNum.enabled)
                return;
            else
            {
                _itemModule._ItemData = null;
                _image.enabled = false;
                TextItemNum.enabled = false;
                EventTriggerListener.Get(ImgItem.gameObject).RemoveAllHandle();
            }
            //EventTriggerListener.Get(_imgItem.gameObject).RemoveAllHandle();
        }
        else
        {
            _image.overrideSprite = Resources.Load("Sprite/" + _itemModule._ItemData.Icon, typeof(Sprite)) as Sprite;
            _image.enabled = true;
            TextItemNum.text = _itemModule._ItemData.Num.ToString();
            TextItemNum.enabled = true;
            EventTriggerListener.Get(_imgItem.gameObject).SetEventHandle(EnumTouchEventType.OnClick, UseItem);
            EventTriggerListener.Get(_imgItem.gameObject).SetEventHandle(EnumTouchEventType.OnDoubleClick, AddItem);
        }
    }

    #region override method
    protected override void OnAwake()
    {
        base.OnAwake();
        currentKey = KeyCode.Space;
        MessageCenter.Instance.AddListener("AutoUpdateItem", ItemMessage);
        //_image = transform.FindChild("Image").GetComponent<Image>();
        _image = ImgItem.transform.FindChild("Image").GetComponent<Image>();
        EventTriggerListener.Get(_imgItem.gameObject).SetEventHandle(EnumTouchEventType.OnClick, UseItem);
        EventTriggerListener.Get(_imgItem.gameObject).SetEventHandle(EnumTouchEventType.OnDoubleClick, AddItem);
        //_image = ImgItem.transform.GetComponentInChildren<Image>();
        //Debug.Log("OnAwake");
        _bagui = GameObject.Find("Canvas(Clone)/bgImage/RightArea/Panel/BagUI(Clone)").GetComponent<BagUI>();
    }

    private void ItemMessage(Message message)
    {
        SetItemData((ItemModule.ItemData)message["Item"]);
    }

    protected override void OnLoadData()
    {
        base.OnLoadData();
        _itemModule = ModuleManager.Instance.Get<ItemModule>();
        //SetItemData();
        Debug.Log("OnLoadData");
    }
    protected override void OnRelease()
    {
        base.OnRelease();
        _imgItem = null;
        EventTriggerListener.Get(ImgItem.gameObject).RemoveAllHandle();
        //Debug.Log("OnRelease");
    }

    #endregion





    #region Event Call back
    private void UseItem(GameObject _listener, object _args, params object[] _params)
    //private void UseItem()
    {
        Debug.Log(Event.current);
        Event e = Event.current;
        if (e.isKey)
        {
            currentKey = e.keyCode;
            Debug.Log("Current Key is : " + currentKey.ToString());
        }
        //if (Input.GetButton("Fire1"))
        //{
        //    Debug.Log("left");
        //}
        //Debug.Log(_args);
        _itemModule.RemoveImageNum();
        _bagui.SetData();
        //}
    }
    private void AddItem(GameObject _listener, object _args, object[] _params)
    {
        _itemModule.AddImageNum();
        _bagui.SetData();
    }

    #endregion

}
