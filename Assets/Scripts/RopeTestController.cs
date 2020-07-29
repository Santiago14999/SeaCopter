using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RopeTestController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _yOffset;

    private Animator _animator;
    private readonly int _dropAnimation = Animator.StringToHash("ReleaseRope");
    private readonly int _pullAnimation = Animator.StringToHash("PullRope");
    private bool _isDropped;

    private void Awake() => _animator = GetComponent<Animator>();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_isDropped)
            {
                _isDropped = true;
                _animator.Play(_dropAnimation);
            }
            else
            {
                _isDropped = false;
                _animator.Play(_pullAnimation);
            }
        }
    }

    private void LateUpdate()
    {
        transform.position = _player.position + Vector3.up * _yOffset;
    }
}
