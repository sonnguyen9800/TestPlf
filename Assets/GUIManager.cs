using System;
using System.Collections;
using _Custom;
using Cinemachine;
using UnityCommunity.UnitySingleton;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.UI;

public class GUIManager : MonoSingleton<GUIManager>
{
    [SerializeField] private HorizontalLayoutGroup _heartLayout = null;
    [SerializeField] private Image _hearthPrefab = null;
    private Image[] _hearts;
    [SerializeField] private TextMeshProUGUI _coinValueTMP;
    [SerializeField] private CinemachineImpulseSource _impulseSource;
    
    [SerializeField] private Volume _postProcessVolume; // Reference to Post-Processing Volume
    private ColorAdjustments _colorAdjustments;

    [SerializeField] private float _flashDuration = 0.2f; // Flash duration

    private int _coinCount;

    private void Awake()
    {
        _coinValueTMP.text = string.Empty;

        // Get the Color Adjustments effect from the Post-Processing Volume
        if (_postProcessVolume.profile.TryGet(out _colorAdjustments))
        {
            _colorAdjustments.colorFilter.value = Color.white; // Default color
        }
    }

    private void Start()
    {
        // Instantiate Heart Prefabs
        int totalHearts = ConfigManager.Instance.GetInitHealth();
        _hearts = new Image[totalHearts];
        for (int i = 0; i < totalHearts; i++)
        {
            _hearts[i] = Instantiate(_hearthPrefab, _heartLayout.transform);
        }
        _hearthPrefab.gameObject.SetActive(false);
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

        if (_colorAdjustments != null)
        {
            StartCoroutine(FlashEffect());
        }
    }

    private IEnumerator FlashEffect()
    {
        _colorAdjustments.colorFilter.value = Color.red; // Set red screen tint
        yield return new WaitForSeconds(_flashDuration);
        _colorAdjustments.colorFilter.value = Color.white; // Reset back to normal
    }
}