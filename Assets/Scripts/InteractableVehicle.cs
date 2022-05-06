using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableVehicle : MonoBehaviour
{
    [SerializeField] private Animator _pipeAnimator;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _exitTarget;
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _textBubblePrefab;
    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private State _state = State.MovingToTarget;

    private Collider2D _targetCollider;
    private Collider2D _exitTargetCollider;
    private FuelModel _requestedFuel;

    private enum State
    {
        MovingToTarget,
        Interacting,
        Exiting,
        OutOfBounds
    }

    public UnityEvent<FuelModel> StartedInteraction;
    public UnityEvent<bool> EndedInteraction;

    public void OnFuelSubmitted(FuelModel fuel)
    {
        StartCoroutine(WaitForAnimationCoroutine(fuel));
    }
    public void Setup(Animator animator, GameObject target, GameObject exitTarget)
    {
        _pipeAnimator = animator;
        _target = target;
        _exitTarget = exitTarget;
        _targetCollider = _target.GetComponent<Collider2D>();
        _exitTargetCollider = _exitTarget.GetComponent<Collider2D>();
    }

    private void Start()
    {
        _targetCollider = _target.GetComponent<Collider2D>();
        _exitTargetCollider = _exitTarget.GetComponent<Collider2D>();
        _requestedFuel = FuelModel.CreateRandom();

        Character character = null;

        StartedInteraction.AddListener((FuelModel fuel) =>
        {
            var prefab = Instantiate(_characterPrefab);
            character = prefab.GetComponent<Character>();

            if (prefab.TryGetComponent(out StoryCharacter storyCharacter))
            {
                _requestedFuel = storyCharacter.GetFuelModel();
                storyCharacter.SayNext();
            }
            else
            {
                character.Say($"Привет\nБензин: {fuel.Gasoline}%\nВодород: {fuel.Hydrogen}%\nКеросин: {fuel.Kerosene}%\nСпирт: {fuel.Alcohol}%");
            }
        });

        EndedInteraction.AddListener((bool success) =>
        {
            if (character.isActiveAndEnabled && character.TryGetComponent(out StoryCharacter storyCharacter) == true)
                storyCharacter.SayBye(success);
            else if (success)
                character.Say("Спасибо.");
            else
                character.Say("Ну и бурду ты намешал.");

            Destroy(character.gameObject);
        });
    }

    private void FixedUpdate()
    {
        if (_state == State.MovingToTarget)
            MoveTo(_target);
        else if (_state == State.Exiting)
            MoveTo(_exitTarget);
        else if (_state == State.OutOfBounds)
            transform.Translate(_speed * Time.fixedDeltaTime * 100f, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == _targetCollider)
        {
            StartedInteraction?.Invoke(_requestedFuel);
            _state = State.Interacting;
        }
        else if (collision.collider == _exitTargetCollider)
        {
            _state = State.OutOfBounds;
        }
    }

    private void MoveTo(GameObject target)
    {
        Vector3 position = Vector3.MoveTowards(transform.position, target.transform.position, _speed);
        transform.position = position;
    }
    private IEnumerator WaitForAnimationCoroutine(FuelModel fuel)
    {
        var time = _pipeAnimator.GetCurrentAnimatorStateInfo(0).length * 6;
        yield return new WaitForSeconds(time);
        _state = State.Exiting;
        Debug.Log($"Recieved: g-{fuel.Gasoline} h-{fuel.Hydrogen}");
        EndedInteraction?.Invoke(FuelModel.AreSimilar(_requestedFuel, fuel));
    }
}
