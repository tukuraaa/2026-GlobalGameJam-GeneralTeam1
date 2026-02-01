using System;
using DG.Tweening;
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
    private bool _isInitialized = false; 


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
    public void Init(float max)
    {
        maxValue = max;
        _isInitialized = true;
    }

    private void ValueChangeHandler(float value)
    {
        Vector2 newDelta = rectTransform.sizeDelta;
        if(textDisplay != null)
        {
            textDisplay.text = $"{currentValue:f0}";
        }
        if (!isVertical)
        {
            //bruh horizontal
            Debug.Log($"value:{value} vs: {maxValue}");
            // Debug.Log($"newX: {startingDimension * value / maxValue} prevX: {startingDimension}");
            newDelta.x = startingDimension * value / maxValue;
            rectTransform.DOSizeDelta(newDelta, 0.5f);
        }
        else
        {
            
        }
    }


}
