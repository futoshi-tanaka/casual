using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModalUI : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text descriptionText;
    [SerializeField]
    private Button okButton;
    [SerializeField]
    private Button outOfModal;
    [SerializeField]
    private RectTransform outOfModalRectTransform;
    [SerializeField]
    private float fadeDuration = 0.5f;

    private void Awake()
    {
        Disable();
    }

    private void Enable()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    private void Disable()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    private void In()
    {
        canvasGroup.DOFade(1.0f, fadeDuration).OnComplete(() =>
        {
            Enable();
        });
    }

    private void Out()
    {
        canvasGroup.DOFade(0.0f, fadeDuration).OnComplete(() =>
        {
            Disable();
        });
    }

    public void Open(string title, string description, UnityAction onOk)
    {
        titleText.text = title;
        descriptionText.text = description;
        okButton.onClick.AddListener(onOk);
        outOfModal.onClick.AddListener(Close);
        In();
    }

    public void Close()
    {
        Out();
    }
}
