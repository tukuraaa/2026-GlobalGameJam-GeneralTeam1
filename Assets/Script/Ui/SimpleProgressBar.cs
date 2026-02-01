using System;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Mask), typeof(Image))]
public class SimpleProgressBar : MonoBehaviour
{
    [SerializeField]
    bool isVertical;    //if is not vertical, then is horizontal.


    [SerializeField]
    float startingDimension;

    [SerializeField]
    RectTransform rectTransform;

    [Space]
    [SerializeField]
    TextMeshProUGUI textDisplay;

    [SerializeField]
    float maxValue;
    
    public ReactiveProperty<float> currentValue;

    void OnValidate()
    {
        if(rectTransform == null && transform is RectTransform)
        {
            rectTransform = transform as RectTransform;
        }
        if(rectTransform == null)
            return; //やば

        startingDimension = isVertical ? rectTransform.sizeDelta.y : rectTransform.sizeDelta.x;
        maxValue = 1;   // レイで割るダメ
        currentValue = new ReactiveProperty<float>(maxValue);
        if(textDisplay != null)
        {
            textDisplay.text = $"{currentValue:f0}";
        }
    }
    void Start()
    {
        currentValue.Subscribe(ValueChangeHandler);
    }

    private void ValueChangeHandler(float value)
    {
        Vector2 newDelta = rectTransform.sizeDelta;
        if(textDisplay != null)
        {
            textDisplay.text = $"{currentValue:f0}";
        }
        if (isVertical)
        {

            newDelta.y = startingDimension * value / maxValue;
            Vector2.Lerp(rectTransform.sizeDelta, newDelta, 0.5f);
        }
    }

}
