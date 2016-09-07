
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XHFrameWork;


public class CanvasRoot : BaseUI
{
    #region implemented abstract members of BaseUI
    public override EnumUIType GetUIType()
    {
        return EnumUIType.CanvasRoot;
    }
    #endregion
}

