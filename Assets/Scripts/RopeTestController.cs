using UnityEngine;

public class RopeTestController : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Animator _animator;
    private readonly int _dropAnimation = Animator.StringToHash("DropRope");

    private void Awake() => _animator = GetComponent<Animator>();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _animator.Play(_dropAnimation);
    }

    private void LateUpdate()
    {
        transform.position = _player.position;
    }
}
