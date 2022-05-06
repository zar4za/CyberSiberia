using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarFuel : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Vector3 _minPosition = new Vector3(3.5f, -4.11f, 0f);
    private Vector3 _minScale = new Vector3(1.35f, -0.02f, 1f);
    private Vector3 _maxPosition = new Vector3(3.5f, -3.4f, 0f);
    private Vector3 _maxScale = new Vector3(1.35f, -1.45f, 1f);
    private float _positionDistance;
    private float _scaleDistance;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _positionDistance = Vector3.Distance(_minPosition, _maxPosition);
        _scaleDistance = Vector3.Distance(_minScale, _maxScale);
    }

    public void Draw(int fillInPercent, Color color)
    {
        fillInPercent = Clamp(fillInPercent);
        float deltaDistance = _positionDistance * (fillInPercent / 100f);
        float deltaScale = _scaleDistance * (fillInPercent / 100f);
        Vector3 newPosition = Vector3.MoveTowards(_minPosition, _maxPosition, deltaDistance);
        Vector3 newScale = Vector3.MoveTowards(_minScale, _maxScale, deltaScale);
        _spriteRenderer.color = color;
        _spriteRenderer.transform.position = newPosition;
        _spriteRenderer.transform.localScale = newScale;
    }

    private int Clamp(int percent)
    {
        if (percent > 100)
            return 100;
        return percent;
    }
}
