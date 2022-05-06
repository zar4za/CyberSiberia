using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Character))]
public class StoryCharacter : MonoBehaviour
{
    [SerializeField] private List<string> _phrases = new();
    [SerializeField] private string _declineDialoguePhrase;
    [SerializeField] private string _thanksPhrase;
    [SerializeField] private string _unhappyPhrase;
    private int _index = 0;
    [SerializeField] private Character _character;

    [Header("Fuel Settings")]
    [Range(0, 100)]
    [SerializeField] 
    private int _gasolinePercent = 25;
    [Range(0, 100)]
    [SerializeField]
    private int _hydrogenPercent = 25;
    [Range(0, 100)]
    [SerializeField]
    private int _kerosenePercent = 25;
    [Range(0, 100)]
    [SerializeField]
    private int _alcoholPercent = 25;

    private TextBubble _currentTextBubble = null;

    public bool DialogueFinished => _index >= _phrases.Count;
    private UnityEvent<TextBubble> CreatedTextBubble = new UnityEvent<TextBubble>();

    private void Start()
    {
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SayNext();
    }

    public void SayNext()
    {
        var bubble = FindObjectOfType<TextBubble>();
        if (bubble)
            Destroy(bubble.gameObject);

        if (_index < _phrases.Count)
        {
            var textBubble = _character.Say(_phrases[_index]);
            _index++;
        }
    }

    public void SayBye(bool success)
    {
        if (DialogueFinished == false)
            _character.Say(_declineDialoguePhrase + ' ' + (success ? _thanksPhrase : _unhappyPhrase));
        else
            _character.Say(success ? _thanksPhrase : _unhappyPhrase);
    }

    public FuelModel GetFuelModel()
    {
        return new FuelModel(Color.clear, _gasolinePercent, _hydrogenPercent, _kerosenePercent, _alcoholPercent);
    }
}
