using Assets.Scripts;
using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Jar : MonoBehaviour
{
    [SerializeField] private Button _submit; 
    [SerializeField] private float _volumePerTick = 0.1f;
    [SerializeField] private Liquid _gasoline;
    [SerializeField] private Liquid _hydrogen;
    [SerializeField] private Liquid _kerosene;
    [SerializeField] private Liquid _alcohol;

    private const int MaxVolume = 100;
    private bool _isPouring = false;
    private Dictionary<Liquid.LiquidType, Color> _liquidColors = new();
    private Dictionary<Liquid.LiquidType, float> _liquidVolumes = new();

    public UnityEvent<FuelModel> FuelSubmitted;
    public UnityEvent<int, Color> VolumeChanged;
    private int FillPercentage => Mathf.RoundToInt(TotalVolume / MaxVolume * 100);
    private float TotalVolume => _liquidVolumes.Sum(x => x.Value);

    private void Start()
    {
        _liquidVolumes.Add(Liquid.LiquidType.Gasoline, 0f);
        _liquidVolumes.Add(Liquid.LiquidType.Hydrogen, 0f);
        _liquidVolumes.Add(Liquid.LiquidType.Kerosene, 0f);
        _liquidVolumes.Add(Liquid.LiquidType.Alcohol, 0f);
        _liquidColors.Add(Liquid.LiquidType.Gasoline, _gasoline.GetComponent<SpriteRenderer>().color);
        _liquidColors.Add(Liquid.LiquidType.Hydrogen, _hydrogen.GetComponent<SpriteRenderer>().color);
        _liquidColors.Add(Liquid.LiquidType.Kerosene, _kerosene.GetComponent<SpriteRenderer>().color);
        _liquidColors.Add(Liquid.LiquidType.Alcohol, _alcohol.GetComponent<SpriteRenderer>().color);
    }

    public void RequestFuel(FuelModel fuel) => _submit.interactable = true;

    public void StartPouring(LiquidModel liquid) => StartCoroutine(PouringCoroutine(liquid));

    public void StopPouring() => _isPouring = false;

    public void Clear()
    {
        for (int i = 0; i < _liquidVolumes.Count; i++)
        {
            _liquidVolumes[(Liquid.LiquidType)i] = 0f;
        }

        VolumeChanged?.Invoke(0, Color.clear);
    }

    public void SubmitFuel()
    {
        _submit.interactable = false;
        var color = GetFuelColor();
        FuelSubmitted?.Invoke(new FuelModel(color, _liquidVolumes));
        Clear();
    }

    private IEnumerator PouringCoroutine(LiquidModel liquid)
    {
        int mouseButton = 0;
        _isPouring = true;

        while (_isPouring)
        {
            float liquidVolume = _liquidVolumes[liquid.Liquid] + _volumePerTick * Time.fixedDeltaTime;
            Debug.Log(liquidVolume);
            _liquidVolumes[liquid.Liquid] = liquidVolume;
            VolumeChanged?.Invoke(FillPercentage, GetFuelColor());

            if (Input.GetMouseButtonUp(mouseButton))
            {
                _isPouring = false;
            }
            yield return null;
        }
    }

    private Color GetFuelColor()
    {
        float red = 0;
        float green = 0;
        float blue = 0;

        foreach (var liquid in _liquidColors)
        {
            var multiplier = _liquidVolumes[liquid.Key] / TotalVolume;
            red += liquid.Value.r * multiplier;
            green += liquid.Value.g * multiplier;
            blue += liquid.Value.b * multiplier;
        }

        red /= _liquidVolumes.Count(x => x.Value > 0f);
        green /= _liquidVolumes.Count(x => x.Value > 0f);
        blue /= _liquidVolumes.Count(x => x.Value > 0f);
        return new Color(red, green, blue);
    }
}
