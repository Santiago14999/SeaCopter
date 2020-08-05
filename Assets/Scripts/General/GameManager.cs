using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event System.Action OnGameStarted;
    public static event System.Action OnGameEnded;

    [SerializeField] private float _delayAfterEmptyFuel = 2f;
    [SerializeField] private HelicopterFuelController _fuelController;
    [SerializeField] private HumanManager _humanManager;

    [SerializeField] private GameObject[] _disableOnPlay;
    [SerializeField] private GameObject[] _enableOnPlay;

    private Coroutine _emptyFuelDelay;

    public bool Playing { get; private set; }
    public static GameManager Instance { get; private set; }

    private void Awake() => Instance = this;

    private void Start()
    {
        _fuelController.OnFuelStateChanged += HandleFuelState;
        _humanManager.OnLastHumanDrown += HandleLastHumanDrown;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Lose();
    }

    public void Play()
    {
        foreach (GameObject gameObj in _disableOnPlay)
            gameObj.SetActive(false);
        foreach (GameObject gameObj in _enableOnPlay)
            gameObj.SetActive(true);

        Playing = true;
        OnGameStarted();
    }

    private void Lose()
    {
        Playing = false;
        OnGameEnded();

        foreach (GameObject gameObj in _disableOnPlay)
            gameObj.SetActive(true);
        foreach (GameObject gameObj in _enableOnPlay)
            gameObj.SetActive(false);
    }

    private void HandleFuelState(bool hasFuel)
    {
        if (!Playing)
            return;

        if (hasFuel)
        {
            if (_emptyFuelDelay != null)
                StopCoroutine(_emptyFuelDelay);
        }
        else
        {
            _emptyFuelDelay = StartCoroutine(EmptyFuelCoroutine());
        }
    }

    private void HandleLastHumanDrown() => Lose();

    IEnumerator EmptyFuelCoroutine()
    {
        yield return new WaitForSeconds(_delayAfterEmptyFuel);
        _emptyFuelDelay = null;
        Lose();
    }
}
