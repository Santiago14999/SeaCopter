using UnityEngine;

public class MenuCameraRotator : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 1f;
    [SerializeField] private Transform _player;

    private void Awake() => GameManager.OnGameEnded += delegate { transform.rotation = Quaternion.identity; };

    private void Update()
    {
        transform.position = _player.position;
        transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
    }
}
