using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private VariableJoystick _movementJoystick;
    [SerializeField] private VariableJoystick _heightJoystick;

    public Vector3 GetMoveInput()
    {
        //float xInput = Input.GetAxis("Horizontal");
        //float zInput = Input.GetAxis("Vertical");
        //float yInput = Input.GetAxis("Elevation");

        //Vector3 moveVector = new Vector3(xInput, yInput, zInput);

        //return moveVector.normalized;

        Vector2 joystickInput = _movementJoystick.Direction;
        Vector2 heightInput = _heightJoystick.Direction;

        return new Vector3(joystickInput.x, heightInput.y, joystickInput.y);
    }
}
