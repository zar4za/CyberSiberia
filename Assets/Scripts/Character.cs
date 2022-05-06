using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] private float SymbolsPerSecond = 10f;
    [SerializeField] private GameObject _textBubblePrefab;

    public TextBubble Say(string text)
    {
        ClearPrevious();
        float duration = text.Length / SymbolsPerSecond;
        Debug.Log("Text bubble duration: " + duration);
        var bubble = Instantiate(_textBubblePrefab);
        var textBubble = bubble.GetComponent<TextBubble>();
        textBubble.Setup(text);
        return textBubble;
    }

    private void ClearPrevious()
    {
        var bubble = FindObjectOfType<TextBubble>();
        if (bubble)
            Destroy(bubble.gameObject);
    }
}
