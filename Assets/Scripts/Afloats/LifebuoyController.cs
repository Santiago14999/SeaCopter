using UnityEngine;

public class LifebuoyController : MonoBehaviour
{
    [Tooltip("Off-screen offset needed to destroy the object.")]
    [SerializeField] private float _offsetToDestroy = .1f;

    private static Camera _camera;

    private void Awake()
    {
        if (!_camera)
            _camera = FindObjectOfType<Camera>();
    }

    private void LateUpdate()
    {
        Vector3 viewport = _camera.WorldToViewportPoint(transform.position);
        if (viewport.x < -_offsetToDestroy || viewport.x > 1 + _offsetToDestroy ||
            viewport.y < -_offsetToDestroy || viewport.y > 1 + _offsetToDestroy)
        {
            gameObject.SetActive(false);
        }
    }
}
