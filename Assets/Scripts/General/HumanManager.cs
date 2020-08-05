using UnityEngine;
using UnityEngine.UI;

public class HumanManager : MonoBehaviour
{
    public event System.Action OnLastHumanDrown = delegate { };

    [SerializeField] private Image[] _humanLifesIcons;
    private int _humansDrown;

    private void Awake()
    {
        HumanController.OnDrown += OnHumanDrown;
        GameManager.OnGameStarted += HandleGameStart;
    }

    private void HandleGameStart()
    {
        _humansDrown = 0;
        for (int i = 0; i < _humanLifesIcons.Length; i++)
            _humanLifesIcons[i].color = Color.white;
    }

    private void OnHumanDrown()
    {
        if (_humansDrown > 2)
            return;

        _humanLifesIcons[_humansDrown].color = Color.black;
        _humansDrown++;

        if (_humansDrown == 3)
            OnLastHumanDrown();
    }
}
