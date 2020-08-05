using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private VariableJoystick _movementJoystick;
    [SerializeField] private VariableJoystick _heightJoystick;

    public Vector3 GetMoveInput()
    {
        Vector2 joystickInput = _movementJoystick.Direction;
        Vector2 heightInput = _heightJoystick.Direction;

        return new Vector3(joystickInput.x, heightInput.y, joystickInput.y);
    }
}
