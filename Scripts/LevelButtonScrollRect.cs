using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButtonScrollRect : MonoBehaviour ,IBeginDragHandler, IEndDragHandler
{
    ScrollRect scrollRect;
    float[] pageArray = new float[] { 0f, 0.4996137f,  0.9985816f };
    public float speed = 2f;
    float scrollRectPosition = 0f;
    public Toggle[] movePage;
    void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }
    void Update()
    {
         scrollRect.horizontalNormalizedPosition =
         Mathf.Lerp(scrollRect.horizontalNormalizedPosition, scrollRectPosition, speed * Time.deltaTime);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float temp = scrollRect.horizontalNormalizedPosition;
        float offset = 0.25f;
        for (int i = 0; i < pageArray.Length; i++)
        {
            if (Mathf.Abs(temp - pageArray[i]) < offset)
            {
                scrollRectPosition = pageArray[i];
                movePage[i].isOn = true;
            }
        }
    }
    public void MovePage1(bool isOn)
    {
        if (isOn)
        {
            scrollRectPosition = pageArray[0];
        }
    }
    public void MovePage2(bool isOn)
    {
        if (isOn)
        {
            scrollRectPosition = pageArray[1];
        }
    }
    public void MovePage3(bool isOn)
    {
        if (isOn)
        {
            scrollRectPosition = pageArray[2];
        }
    }
}
