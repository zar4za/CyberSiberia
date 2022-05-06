using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleSpawner : MonoBehaviour
{
    private const int MinPercent = 1;
    private const int MaxPercent = 100;

    [SerializeField] private int _minSeconds = 5;
    [SerializeField] private int _maxSeconds = 20;
    [SerializeField] private int _chancePercent = 25;
    [SerializeField] private List<GameObject> _storyVehicles = new();
    [SerializeField] private List<GameObject> _interactableVehicles = new();
    [SerializeField] private List<GameObject> _mockVehicles = new();
    [SerializeField] private Collider2D _destroyer;
    [SerializeField] private Animator _fuelAnimator;
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _exitTarget;
    [SerializeField] private Button _submitButton;
    [SerializeField] private Jar _jar;

    private float _timer = 0;
    private Queue<GameObject> _storyQueue;
    [SerializeField] private bool _canInteract = true;

    private void Start()
    {
        _storyQueue = new Queue<GameObject>(_storyVehicles);

        foreach (var vehicle in _mockVehicles)
        {
            vehicle.GetComponent<Vehicle>().SetDestroyer(_destroyer);
        }

        foreach (var vehicle in _interactableVehicles)
        {
            vehicle.GetComponent<Vehicle>().SetDestroyer(_destroyer);
        }
    }

    private void FixedUpdate()
    {
        _timer -= Time.fixedDeltaTime;

        if (_timer <= 0)
        {
            GameObject prefab;
            _timer = Random.Range(_minSeconds, _maxSeconds);

            if (_canInteract && Random.Range(MinPercent, MaxPercent) <= _chancePercent)
            {
                _canInteract = false;

                if (_storyQueue.TryDequeue(out prefab))
                {
                    
                }
                else
                {
                    var index = Mathf.RoundToInt(Random.Range(0, _interactableVehicles.Count - 1));
                    prefab = _interactableVehicles[index];
                }


                var interactable = Instantiate(prefab).GetComponent<InteractableVehicle>();
                interactable.StartedInteraction.AddListener(_jar.RequestFuel);
                _jar.FuelSubmitted.AddListener(interactable.OnFuelSubmitted);
                interactable.EndedInteraction.AddListener(OnEndedInteraction);
                interactable.Setup(_fuelAnimator, _target, _exitTarget);
                Debug.Log("Interactable");
            }
            else
            {
                var index = Mathf.RoundToInt(Random.Range(0, _mockVehicles.Count - 1));
                prefab = _mockVehicles[index];
                Instantiate(prefab);
            }
        }
    }
    private void OnEndedInteraction(bool success) => _canInteract = true;
}
