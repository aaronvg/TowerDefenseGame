using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class HealthbarUIHandler : MonoBehaviour
{
    private float _defaultWidth;
    private RectTransform _rectTransform;
    private Canvas _canvas;

    private float _maxHealth;

    private float _currentHealth;
    private float _targetHealth;

    public Transform TrackingObject;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _defaultWidth = _rectTransform.rect.width;
        _canvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        _currentHealth = Mathf.Lerp(_currentHealth, _targetHealth, 10f * Time.deltaTime);
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _defaultWidth * (_currentHealth / _maxHealth));
        var image = GetComponent<Image>();
        if (image != null)
        {
            image.color = Color.Lerp(Color.red, Color.green, (_currentHealth / _maxHealth)) * 2;
        }

        if (TrackingObject)
        {
            var scrpos = Camera.main.WorldToScreenPoint(TrackingObject.position + new Vector3(0, 1.5f, 0));
            var twodscrpos = new Vector2(scrpos.x, scrpos.y);
            var scaler = GetComponentInParent<CanvasScaler>();
            Vector2 newscrpos = new Vector2(scrpos.x,
                scrpos.y);
            _rectTransform.position = newscrpos;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void SetHealth(float health)
    {
        if (health > _maxHealth)
        {
            health = _maxHealth;
        }
        _targetHealth = health;
    }

    void SetMaxHealth(float max)
    {
        _maxHealth = max;
        _currentHealth = max;
        _targetHealth = max;
    }
}
