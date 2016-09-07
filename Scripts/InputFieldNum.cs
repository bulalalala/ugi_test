using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputFieldNum : MonoBehaviour 
{
    string textnum = "";
    public int  num ;
    InputField inputField;
    public GameObject placeholder;
    void Awake()
    {
        inputField = GetComponent<InputField>();
        num = int.Parse(placeholder.GetComponent<Text>().text.ToString());
    }
    void Update()
    {
        if (num < 1000 && num >=0)
        {
            if (inputField.text.ToString() != "")
            {
                num = int.Parse(inputField.text.ToString());
            }
        }
        else
        {
            num = 999;
            inputField.text = num.ToString();
        }
    }
    public void change()
    {
        num += 1;
        inputField.text = num.ToString();
    }
    public void Add()
    {
        num += 20;
        inputField.text = num.ToString();
    }
}
