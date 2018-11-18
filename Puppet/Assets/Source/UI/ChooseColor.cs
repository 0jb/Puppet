using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Puppet.Source.Network;

public class ChooseColor : MonoBehaviour
{
    [SerializeField]
    private Slider _hueSlider;
    [SerializeField]
    private Slider _saturationSlider;
    [SerializeField]
    private Slider _valueSlider;

    [SerializeField]
    private Image _colorDemo;

    public void UpdateDemoColor()
    {
        _colorDemo.color = Color.HSVToRGB(_hueSlider.value, 
                                    _saturationSlider.value, 
                                    _valueSlider.value);
    }

    public void ApplyColor()
    {
        GameManager.Instance.UpdateLocalPlayerVisual(_colorDemo.color);
    }

}
