using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HelicopterMovementController), typeof(Animator))]
public class HelicopterAnimationController : MonoBehaviour
{
    [SerializeField] private float _bladesMaxSpeed;
    [SerializeField] private float _bladesChangeStateTime;
    
    private Animator _animator;
    private HelicopterMovementController _movementController;
    private IEnumerator _bladesStateChangeAnimation;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movementController = GetComponent<HelicopterMovementController>();
        _movementController.OnGroundedStateChanged += UpdateBladesSpeed;
    }

    private void UpdateBladesSpeed(bool isGrounded)
    {
        if (_bladesStateChangeAnimation != null)
            StopCoroutine(_bladesStateChangeAnimation);

        _bladesStateChangeAnimation = SetBladesStateSmooth(isGrounded);
        StartCoroutine(_bladesStateChangeAnimation);
    }

    IEnumerator SetBladesStateSmooth(bool isGrounded)
    {
        float elapsed = 0f;
        float fromSpeed = _animator.speed;
        float toSpeed = isGrounded ? 0 : _bladesMaxSpeed;

        while (elapsed < _bladesChangeStateTime)
        {
            elapsed += Time.deltaTime;
            _animator.speed = Mathf.Lerp(fromSpeed, toSpeed, elapsed / _bladesChangeStateTime);
            yield return null;
        }

        _animator.speed = toSpeed;

        // _bladesStateChangeAnimation = null; Not sure if this is ok
    }

}
