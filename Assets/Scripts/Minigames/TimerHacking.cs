using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerHacking : MonoBehaviour
{
    RectTransform rectangle;
    public bool isCounting = false;
    public float timer = 29;
    Vector2 originalSize;
    public int securityLvl;
    void Start()
    {
        rectangle = GetComponent<RectTransform>();
        originalSize = rectangle.sizeDelta;
    }

    void Update()
    {
        if(isCounting)
        {
            timer -= Time.deltaTime;
            switch (securityLvl)
            {
                case 1:
                    rectangle.sizeDelta = new Vector2(rectangle.sizeDelta.x - Time.deltaTime * 39f, rectangle.sizeDelta.y);
                    break;
                case 2:
                    rectangle.sizeDelta = new Vector2(rectangle.sizeDelta.x - Time.deltaTime * 50f, rectangle.sizeDelta.y);
                    break;
                case 3:
                    rectangle.sizeDelta = new Vector2(rectangle.sizeDelta.x - Time.deltaTime * 71f, rectangle.sizeDelta.y);
                    break;
            }
            if (timer < 0)
            {
                rectangle.sizeDelta = originalSize;
            }
        }
    }
}
