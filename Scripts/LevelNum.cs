using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelNum : MonoBehaviour 
{
    Text text;
    public GameObject strengthNumInput;
    public GameObject dexNumInput;
    public GameObject focusNumInput;
    int strengthNum;
    int dexNum;
    int focusNum;
    void Awake()
    {
        text = GetComponent<Text>();

    }
    void Update()
    {
        strengthNum = strengthNumInput.GetComponent<InputFieldNum>().num;
        dexNum = dexNumInput.GetComponent<InputFieldNum>().num;
        focusNum = focusNumInput.GetComponent<InputFieldNum>().num;
        text.text = ((strengthNum + dexNum + focusNum )/ 50).ToString();
    }

}
