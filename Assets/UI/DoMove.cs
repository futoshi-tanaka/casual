using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoMove : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private Vector3 endPosition;

    [SerializeField]
    private float duration = 1f;

    [SerializeField]
    private bool startAwake = true;

    void Awake()
    {
        if(startAwake) SetMove();
    }

    public void SetMove()
    {
        rectTransform.DOAnchorPos(endPosition, duration).SetEase(Ease.OutBounce).SetRelative(true);
    }
}
