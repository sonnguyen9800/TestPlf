using System;
using Cinemachine;
using UnityCommunity.UnitySingleton;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GUIManager : MonoSingleton<GUIManager>
{
    [SerializeField] private Image[] _hearts;
    [SerializeField] private TMPro.TextMeshProUGUI _coinValueTMP;

    [SerializeField] private CinemachineImpulseSource _impulseSource;
    private int _coinCount;
    private void Awake()
    {
        _coinValueTMP.text = string.Empty;
    }
    
    public void SetHearth(int heartCount)
    {
        for (int i = 0; i < _hearts.Length; i++)
        {
            _hearts[i].enabled = i < heartCount;
        }
    }

    public void IncreaseCoinValue(int amount)
    {
        _coinCount += amount;
        _coinValueTMP.text = _coinCount.ToString();
    }

    public void PlayEffectHurt()
    {
        Debug.Log("PlayEffectHurt");
        _impulseSource.GenerateImpulse();
    }
}
