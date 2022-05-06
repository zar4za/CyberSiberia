using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Fuel : MonoBehaviour
{
    private const string FlowAnimationName = "Flow";
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Flow(FuelModel fuel)
    {
        _spriteRenderer.color = fuel.Color;
        _animator.Play(FlowAnimationName);
    }
}
