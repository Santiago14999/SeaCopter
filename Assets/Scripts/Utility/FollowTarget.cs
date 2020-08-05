using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private bool _followX;
    [SerializeField] private bool _followY;
    [SerializeField] private bool _followZ;
    [SerializeField] private bool _followXRotation;
    [SerializeField] private bool _followYRotation;
    [SerializeField] private bool _followZRotation;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private Vector3 _rotationOffset;

    private void Update()
    {
        Vector3 targetPosition = _target.position + _positionOffset;
        Vector3 targetRotation = _target.eulerAngles + _rotationOffset;

        if (!_followX)
            targetPosition.x = transform.position.x;
        if (!_followY)
            targetPosition.y = transform.position.y;
        if (!_followZ)
            targetPosition.z = transform.position.z;

        if (!_followXRotation)
            targetRotation.x = transform.eulerAngles.x;
        if (!_followYRotation)
            targetRotation.y = transform.eulerAngles.y;
        if (!_followZRotation)
            targetRotation.z = transform.eulerAngles.z;

        transform.position = targetPosition;
        transform.rotation = Quaternion.Euler(targetRotation);
    }
}
