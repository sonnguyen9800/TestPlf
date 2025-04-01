using System;
using DG.Tweening;
using NTC.Pool;
using TMPro;
using UnityEngine;

public class TapTextEffect : MonoBehaviour , IPoolable
{
    private TextMeshPro _textMeshPro = null;
    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
    }

    public void OnSpawn()
    {
        _textMeshPro.alpha = 1;

        transform.DOLocalMoveY(3, 1.2f);
        _textMeshPro.DOFade(0, 1.2f);


    }

    public void OnDespawn()
    {
    }
}