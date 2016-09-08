using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XHFrameWork;


 
public class CharacterModule : BaseModule
{
    #region player data
    string playerName = "bulalala";
    Dictionary<string, uint> ButtonDic = new Dictionary<string, uint>();
    //CharacterModule _charaModule = new CharacterModule();

    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }

    uint level = 8;

    public uint Level
    {
        get { return level; }
        set 
        {
            if (value > 99)
            {
                value = 99;
            }
            level = value; 
        }
    }

    uint strengthNum = 12;

    public uint StrengthNum
    {
        get { return strengthNum; }
        set
        {
            if (value > 999)
            {
                value = 999;
            }
            strengthNum = value;
        }
    }

    uint dexNum = 24;

    public uint DexNum
    {
        get { return dexNum; }
        set
        {
            if (value > 999)
            {
                value = 999;
            }
            dexNum = value;
        }
    }

    uint focusNum;

    public uint FocusNum
    {
        get { return focusNum; }
        set
        {
            if (value > 999)
            {
                value = 999;
            }
            focusNum = value;
        }
    }
    #endregion

    public void SetNum(GameObject obj, uint count = 1)
    {
        //Debug.Log(obj);
        //Debug.Log(ButtonDic.ContainsKey(obj.name));
        //Debug.Log(ButtonDic.ContainsKey("StrengthNumAddNum"));//StrengthNumAddNum
        //ButtonDic[obj.name] += count;
        //Debug.Log(ButtonDic[obj.name]);
        //Debug.Log(strengthNum);
        switch (obj.name)
        {
            case"StrengthNumAddNum":
                StrengthNum += count;
                break;
            case"DexNumAddNum":
                DexNum += count;
                break;
            case"FocusNumAddNum":
                FocusNum += count;
                break;
            default:
                break;
        }
        SetLevel();
    }
    void SetLevel()
    {

        Level = (StrengthNum + DexNum + FocusNum) / 20;
        
    }

    public CharacterModule()
    {
        SetLevel();
        ButtonDic.Add("StrengthNumAddNum", StrengthNum);
        ButtonDic.Add("DexNumAddNum", DexNum);
        ButtonDic.Add("FocusNumAddNum", FocusNum);
        //Debug.Log(ButtonDic["StrengthNumAddNum"].GetHashCode());
        //Debug.Log(StrengthNum.GetHashCode());
    }



    #region override method
    protected override void OnLoad()
    {
        base.OnLoad();
        
    }

    protected override void OnRelease()
    {
        base.OnRelease();
    }

    #endregion

}

