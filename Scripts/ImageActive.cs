using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageActive : MonoBehaviour 
{
    public Text text;
    Image image;
    void Awake()
    {
        image = GetComponent<Image>();
    }
    void Update()
    {
        //Debug.Log(image.sprite);
        if (int.Parse(text.text.ToString()) <= 0)
        {
            image.sprite = null;
            image.enabled = false;
        }
    }
}
