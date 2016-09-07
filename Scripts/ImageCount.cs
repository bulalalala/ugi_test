using UnityEngine;
using UnityEngine.UI;
using System.Collections;
[System.Serializable]
public class ImageCount : MonoBehaviour 
{
    public Image[] tabOneImages ;
    public Image[] tabTwoImages;
    public Image[] tabThreeImages;
    public GameObject[] tabs;
    int count = 15;
    Text text;
    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
            if (tabs[1].activeSelf)
            {
                count = 15;
                foreach (Image image in tabTwoImages)
                {
                    if (image.sprite == null)
                    {
                        count--;
                    }
                }
            }
            if (tabs[2].activeSelf)
            {
                count = 15;
                foreach (Image image in tabThreeImages)
                {
                    if (image.sprite == null)
                    {
                        count--;
                    }
                }
            }
            if(tabs[0].activeSelf)
            {
                count = 15;
                    foreach (Image image in tabOneImages)
                    {
                        if (image.sprite == null)
                        {
                            count--;
                        }
                    }
            }
        if (count <= 0)
        {
            count = 0;
        }
        text.text = count.ToString();
    }
}
