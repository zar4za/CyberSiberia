using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class KeyboardButton : MonoBehaviour
{
    [SerializeField] private KeyCode _keyboardKey;
    [SerializeField] private Sprite _pressed;

    private Sprite _active;
    private Image _image;
    private Button _button;

    private void Start()
    {
        _image = GetComponent<Image>();
        _active = _image.sprite;
        _button = GetComponent<Button>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_keyboardKey))
            OnButtonDown();
        else if (Input.GetKeyUp(_keyboardKey))
            OnButtonUp();
    }

    private void OnButtonDown()
    {
        _image.sprite = _pressed;
        _button.onClick?.Invoke();
    }

    private void OnButtonUp()
    {
        _image.sprite = _active;
    }
}
