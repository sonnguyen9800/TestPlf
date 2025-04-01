using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private Image[] _hearts;
    [SerializeField] private TMPro.TextMeshProUGUI _coinValueTMP;

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
}
