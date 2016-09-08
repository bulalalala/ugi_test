using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XHFrameWork;
using System;
using UnityEngine.UI;
using System.Linq;

public class StartGame : MonoBehaviour {
	// Use this for initialization
	void Start () {
        UIManager.Instance.OpenUI(EnumUIType.CanvasRoot);
        UIManager.Instance.OpenUI(EnumUIType.BagUI, "Canvas(Clone)/bgImage/RightArea/Panel");
        //UIManager.Instance.OpenUI(EnumUIType.Character, "Canvas(Clone)/bgImage");

        GameObject character = GameObject.Find("Canvas(Clone)/bgImage/LeftArea");
        character.AddComponent<Character>();
        character.GetComponent<Character>().enabled = true;

        RegisterAllModules();
        GameObject bagUI = GameObject.Find("Canvas(Clone)/bgImage/RightArea/Panel/BagUI(Clone)");
        bagUI.transform.DestroyAllChildren();
        //ClearAllChildren(bagUI.transform);
        //Debug.Log(bagUI);

    }

    private void RegisterAllModules()
	{
        BagUIModule bagUIModule = new BagUIModule();
        bagUIModule.Load();

        ItemModule itemModule = new ItemModule();
        itemModule.Load();

        CharacterModule charaModule = new CharacterModule();
        charaModule.Load();

        CharacterModule characterModule = new CharacterModule();
		//.....add
	}

}
