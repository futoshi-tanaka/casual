using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    public void SetText(string text)
    {
        _text.SetText(text);
    }
}
