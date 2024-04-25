using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public class InAnimation : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    private void In()
    {
        canvasGroup.DOFade(1.0f, 1.0f);
    }

    private void Out()
    {
        canvasGroup.DOFade(0.0f, 1.0f);
    }
}
