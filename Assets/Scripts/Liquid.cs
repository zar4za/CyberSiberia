using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    [SerializeField] private Jar _jar;
    [SerializeField] private LiquidType _type;
    [SerializeField] private float _volume = 10f;
    private Animator _animator;
    private LiquidModel _liquidModel;

    public Color Color { get; private set; }

    public enum LiquidType
    {
        Gasoline,
        Hydrogen,
        Kerosene,
        Alcohol
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        Color = GetComponent<SpriteRenderer>().color;
        _liquidModel = new LiquidModel(Color, _type, _volume);
    }

    public void StartFlow()
    {
        _animator.Play("StartFlow");
        _jar.StartPouring(_liquidModel); ;
    }

    public void EndFlow()
    {
        _animator.Play("Idle");
        _jar.StopPouring();
    }
}
