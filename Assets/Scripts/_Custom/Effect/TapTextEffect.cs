using System;
using DG.Tweening;
using NTC.Pool;
using TMPro;
using UnityEngine;

public class TapTextEffect : MonoBehaviour , IPoolable
{
    private TextMeshProUGUI _textMeshPro = null;
    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void OnSpawn()
    {
        _textMeshPro.alpha = 1;

        transform.DOMoveY(transform.position.y + 30, 1.2f);
        _textMeshPro.DOFade(0, 1.2f);


    }

    public void OnDespawn()
    {
    }
}