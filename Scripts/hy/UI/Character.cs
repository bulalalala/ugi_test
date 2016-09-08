/*************************************************************************
* @Method  :  $SymbolName$
* @brief   :  test
* @inparam :  $MethodArg$
* @outparam:  $SymbolType$
* @author  :  Hooyan
* @date    :  $YEAR$/$MONTH$/$DAY$ $HOUR$:$MINUTE$
* @version :  ver 1.0
*************************************************************************/
using UnityEngine;
using System.Collections;
using XHFrameWork;
using UnityEngine.UI;

public class Character : BaseUI
{
    #region implemented abstract members of BaseUI
    public override EnumUIType GetUIType()
    {
        return EnumUIType.Character;
    }
    #endregion

    private CharacterModule _charModule = new CharacterModule();

    private readonly string TEXT_LEVEL_PATH = "HeadBG/LevelNum";
    private readonly string TEXT_NAME_PATH = "NameArea/NameText";

    private readonly string TEXT_STRENGTH_PATH = "StrengthArea/StrengthNumInput";
    //private readonly string TEXT_STRENGTH_PATH = "Canvas(Clone)/bgImage/LeftArea/StrengthArea/StrengthNumInput";
    private readonly string BUTTON_STRENGTH_PATH = "StrengthArea/StrengthNumAddNum";


    private readonly string TEXT_DEX_PATH = "DexArea/DexNumInput";
    //private readonly string TEXT_STRENGTH_PATH = "Canvas(Clone)/bgImage/LeftArea/StrengthArea/StrengthNumInput";
    private readonly string BUTTON_DEX_PATH = "DexArea/DexNumAddNum";

    private readonly string TEXT_FOCUS_PATH = "FocusArea/FocusNumInput";
    //private readonly string TEXT_STRENGTH_PATH = "Canvas(Clone)/bgImage/LeftArea/StrengthArea/StrengthNumInput";
    private readonly string BUTTON_FOCUS_PATH = "FocusArea/FocusNumAddNum";

    Text levelNum;
    InputField nameText;
    InputField strengthNumInput;
    InputField dexNumInput;
    InputField focusNumInput;

    GameObject strengthNumAddButton;
    GameObject dexNumAddButton;
    GameObject focusNumAddButton;


    #region override method
    protected override void OnAwake()
    {
        //Debug.Log("Chara OnAwake");
        base.OnAwake();
        levelNum = GameObject.Find(TEXT_LEVEL_PATH).GetComponent<Text>();
        nameText = GameObject.Find(TEXT_NAME_PATH).GetComponent<InputField>();

        strengthNumInput = GameObject.Find(TEXT_STRENGTH_PATH).GetComponent<InputField>();
        dexNumInput = GameObject.Find(TEXT_DEX_PATH).GetComponent<InputField>();
        focusNumInput = GameObject.Find(TEXT_FOCUS_PATH).GetComponent<InputField>();

        strengthNumAddButton = GameObject.Find(BUTTON_STRENGTH_PATH);
        EventTriggerListener.Get(strengthNumAddButton).SetEventHandle(EnumTouchEventType.OnClick, AddNum);
        //StrengthNumAddButton.onValueChanged.AddListener((isOn) => { OnToggleValueChanged(_toggleOne, isOn); });
        dexNumAddButton = GameObject.Find(BUTTON_DEX_PATH);
        EventTriggerListener.Get(dexNumAddButton).SetEventHandle(EnumTouchEventType.OnClick, AddNum);
        focusNumAddButton = GameObject.Find(BUTTON_FOCUS_PATH);
        EventTriggerListener.Get(focusNumAddButton).SetEventHandle(EnumTouchEventType.OnClick, AddNum);
        //Debug.Log(focusNumInput);

        ShowPlayerData();
    }

    private void AddNum(GameObject _listener, object _args, params object[] _params)
    {
        //Debug.Log(_listener.ToString());
        _charModule.SetNum(_listener);
        ShowPlayerData();
    }


    protected override void OnLoadData()
    {
        base.OnLoadData();

    }
    protected override void OnRelease()
    {
        base.OnRelease();
    }

    #endregion


    void ShowPlayerData()
    {
        //Debug.Log(_charModule.Level);
        levelNum.text = _charModule.Level.ToString();
        nameText.text = _charModule.PlayerName;
        strengthNumInput.text = _charModule.StrengthNum.ToString();
        dexNumInput.text = _charModule.DexNum.ToString();
        focusNumInput.text = _charModule.FocusNum.ToString();

    }
    void SavePlayerData()
    {
        //_charModule.PlayerName = nameText.

    }
}
