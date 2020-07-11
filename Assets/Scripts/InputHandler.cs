using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;

    public Vector3 GetMoveInput()
    {
        if (!_joystick)
        {
            float xInput = Input.GetAxis("Horizontal");
            float zInput = Input.GetAxis("Vertical");
            float yInput = Input.GetAxis("Elevation");

            Vector3 moveVector = new Vector3(xInput, yInput, zInput);

            return moveVector.normalized;
        }

        Vector2 joystickInput = _joystick.Direction;

        return new Vector3(joystickInput.x, 0, joystickInput.y);
    }
}
