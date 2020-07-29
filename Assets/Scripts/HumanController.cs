using UnityEngine;

public class HumanController : MonoBehaviour
{
    public static event System.Action OnDrown = delegate { };

    [SerializeField] private float _timeToDrown = 60f;

    private Animator _animator;
    private readonly int _grapRopeAnimation = Animator.StringToHash("GrabRope");
    private float _currentFloatingTime;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _currentFloatingTime = _timeToDrown;
    }

    private void Update()
    {
        _currentFloatingTime -= Time.deltaTime;
        if (_currentFloatingTime < 0)
        {
            OnDrown();
        }
    }

    public float GetPercentage() => _currentFloatingTime / _timeToDrown;
    
    public void GrapRope()
    {
        _animator.Play(_grapRopeAnimation);
    }
}
