using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RescueRopeController : MonoBehaviour
{
    private Animator _animator;
    private readonly int _releaseRopeAnimation = Animator.StringToHash("ReleaseRope");
    private readonly int _pullRopeAnimation = Animator.StringToHash("PullRope");

    private void Awake() => _animator = GetComponent<Animator>();

    public void ReleaseRope() => _animator.Play(_releaseRopeAnimation);
    public void PullRope() => _animator.Play(_pullRopeAnimation);
}
