using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider _slider;

    protected void Awake()
    {
        _slider = GetComponent<Slider>();
    }

}
