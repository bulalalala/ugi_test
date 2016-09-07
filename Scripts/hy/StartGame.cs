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

        RegisterAllModules();
        GameObject bagUI = GameObject.Find("Canvas(Clone)/bgImage/RightArea/Panel/BagUI(Clone)");
        bagUI.transform.DestroyAllChildren();
        //ClearAllChildren(bagUI.transform);
        //Debug.Log(bagUI);

    }
    //private void DestroyAllChildren(Transform transform)
    //{
    //    //Debug.Log("Destory");
    //    foreach (var child in transform.OfType<Transform>().ToList())
    //    {
    //        Destroy(child.gameObject);
    //    }
    //}
    //private void ClearAllChildren(Transform transform)
    //{
    //    foreach (var child in transform.OfType<Transform>().ToList())
    //    {
    //        Image image = child.FindChild("Imgae_Item").FindChild("Image").GetComponent<Image>();
    //        image.sprite = null;
    //        image.enabled = false;
    //        Text text = child.FindChild("Imgae_Item").FindChild("Text").GetComponent<Text>();
    //        text.text = null;
    //        text.enabled = false;
    //    }
    //}

    private void RegisterAllModules()
	{
        BagUIModule bagUIModule = new BagUIModule();
        bagUIModule.Load();

        ItemModule itemModule = new ItemModule();
        itemModule.Load();

        CharacterModule characterModule = new CharacterModule();
		//.....add
	}

}
