using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoodsCount : MonoBehaviour 
{
    int num;
    Text text;
    void Awake()
    { 
        text = GetComponent<Text>();
        num = int.Parse(text.text.ToString());
    }
    void Update()
    {
        if (num <= 0)
        {
            num = 0;
        }
    }
    public void Using()
    {
        if (num > 0)
        {
            num -= 1;
            text.text = num.ToString();
        }
    }
}
