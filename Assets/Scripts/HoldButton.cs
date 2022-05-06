using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class HoldButton : MonoBehaviour
{
    [SerializeField] private KeyCode _keyboardKey;
    [SerializeField] private Sprite _pressed;

    private Sprite _active;
    private SpriteRenderer _spriteRenderer;

    public UnityEvent ButtonPressed;
    public UnityEvent ButtonReleased;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _active = _spriteRenderer.sprite;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_keyboardKey))
            OnButtonDown();
        else if (Input.GetKeyUp(_keyboardKey))
            OnButtonUp();
    }

    private void OnMouseDown()
    {
        OnButtonDown();
    }

    private void OnMouseUp()
    {
        OnButtonUp();
    }

    private void OnButtonDown()
    {
        Debug.Log("Button Pressed");
        ButtonPressed?.Invoke();
        _spriteRenderer.sprite = _pressed;
    }
    
    private void OnButtonUp()
    {
        Debug.Log("Button Released");
        ButtonReleased?.Invoke();
        _spriteRenderer.sprite = _active;
    }
}
