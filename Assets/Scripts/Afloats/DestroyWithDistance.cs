using UnityEngine;

public class DestroyWithDistance : MonoBehaviour
{
    [SerializeField] private float _distanceToDestroy = 150f;

    private static Transform _player;

    private void Awake()
    {
        if (!_player)
            _player = FindObjectOfType<HelicopterMovementController>().transform;
    }

    private void LateUpdate()
    {
        if ((transform.position - _player.position).magnitude >= _distanceToDestroy)
            gameObject.SetActive(false);
    }
}
