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

    public Transform TrackingObject;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _defaultWidth = _rectTransform.rect.width;
        _canvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
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
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _defaultWidth * (health / _maxHealth));
    }

    void SetMaxHealth(float max)
    {
        _maxHealth = max;
    }
}
