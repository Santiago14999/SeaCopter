using UnityEngine;

public class QuaternionTesting : MonoBehaviour
{
    [SerializeField] private Vector3 _xLeftPoint;
    [SerializeField] private Vector3 _xRightPoint;
    [SerializeField] private Vector3 _zLeftPoint;
    [SerializeField] private Vector3 _zRightPoint;

    private void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_xLeftPoint, _xRightPoint);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_zLeftPoint, _zRightPoint);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.Cross(_xRightPoint - _xLeftPoint, _zRightPoint - _zLeftPoint));
    }
}
