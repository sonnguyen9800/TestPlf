using System;
using System.Collections;
using _Custom;
using _Custom.Effect;
using Cinemachine;
using DG.Tweening;
using NTC.Pool;
using UnityCommunity.UnitySingleton;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GUIManager : MonoSingleton<GUIManager>
{
    public enum EffectType
    {
        Fly,
        Hit,
        TokenClaimed,
    }
    [SerializeField] private HorizontalLayoutGroup _heartLayout = null;
    [SerializeField] private Image _hearthPrefab = null;
    private Image[] _hearts;
    [SerializeField] private TextMeshProUGUI _coinValueTMP;
    [SerializeField] private CinemachineImpulseSource _impulseSource;
    
    [SerializeField] private Volume _postProcessVolume; // Reference to Post-Processing Volume
    private ColorAdjustments _colorAdjustments;

    [SerializeField] private float _flashDuration = 0.2f; // Flash duration
    [SerializeField] private ObjectPool _hitTextPool = null;
    [SerializeField] private Canvas _canvasUI;
    [SerializeField] private Camera _mainCamera;
    private int _coinCount;

    private void Awake()
    {
        _coinValueTMP.text = _coinCount.ToString();

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
            if (i < heartCount)
            {
                _hearts[i].gameObject.SetActive(true);
                _hearts[i].transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
                _hearts[i].DOFade(1f, 0.3f);
            }
            else
            {
                _hearts[i].transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack)
                    .OnComplete(() => _hearts[i].gameObject.SetActive(false));
                _hearts[i].DOFade(0f, 0.3f);
            }
        }
    }

    public void IncreaseCoinValue(int amount)
    {
        var final =_coinCount + amount;
        _coinValueTMP.text = _coinCount.ToString();
        DOTween.To(() => _coinCount, x => 
        {
            _coinCount = x;
            _coinValueTMP.text = x.ToString();
        }, final, 1f).SetEase(Ease.OutQuad);
    }

    public void PlayEffectHurt()
    {
        _impulseSource.GenerateImpulse();

        if (_colorAdjustments != null)
        {
           FlashEffect();
        }
    }

    private void FlashEffect()
    {
        Color initialColor = _colorAdjustments.colorFilter.value;
        DOTween.To(() => _colorAdjustments.colorFilter.value, x => _colorAdjustments.colorFilter.value = x, Color.red, _flashDuration)
            .OnComplete(() => DOTween.To(() => _colorAdjustments.colorFilter.value, x => _colorAdjustments.colorFilter.value = x, initialColor, _flashDuration));
    }
    private Vector2 ConvertWorldToCanvasPosition(Vector3 worldPosition)
    {
        Vector3 screenPosition = _mainCamera.WorldToScreenPoint(worldPosition);
        // Step 2: Convert screen position to UI (anchoredPosition)
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvasUI.transform as RectTransform, screenPosition, GetComponent<Camera>(), out Vector2 uiPosition);
        return uiPosition;
    }
    public GameObject SpawnText(EffectType type, Vector3 worldPos, bool autoDespawn = true)
    {
        Debug.LogError(type.ToString());
        var text = _hitTextPool.GetPrefabByTag(type.ToString());
        if (text == null)
        {
            Debug.LogError("Text is null" + type.ToString());
            return null;
        }

        return null;
        var obj = NightPool.Spawn(text);

        obj.transform.position = worldPos;
            
        obj.GetComponent<TextMeshProUGUI>();
            
        if (autoDespawn)
            NightPool.Despawn(obj, 1.3f);
        return obj;

    }

}