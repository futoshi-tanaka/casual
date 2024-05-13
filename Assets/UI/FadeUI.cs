using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class FadeUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image image;

    public void Initialize()
    {
        image.DOFade(1f, 0);
    }

    public void FadeIn(float duration = 0.5f, UnityAction onComplete = null)
    {
        image.DOFade(1f, duration).OnComplete(() => onComplete?.Invoke());
    }

    public void FadeOut(float duration = 0.5f, UnityAction onComplete = null)
    {
        image.DOFade(0f, duration).OnComplete(() => onComplete?.Invoke());
    }
}
