using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextBubble : MonoBehaviour
{
    [SerializeField] private float _padding = 1f;

    public void Setup(string text)
    {
        var textMeshPro = GetComponentInChildren<TextMeshPro>();
        var bubbleSpriteRenderer = GetComponent<SpriteRenderer>();

        Debug.Log(nameof(Setup) + " - called with - " + text);
        textMeshPro.text = text;
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new (_padding, _padding);
        bubbleSpriteRenderer.size = textSize + padding;
    }
}
