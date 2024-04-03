using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeUI : MonoBehaviour
{
    [SerializeField] private Image _gauge;

    public void UpdateGauge(float current, float max)
    {
        _gauge.fillAmount = current / max;
    }
}