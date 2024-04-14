using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private readonly string Horizontal = "Horizontal";
    private readonly string Vertical = "Vertical";

    [SerializeField] private KeyCode _boostKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    private Vector3 _direction = new Vector3();

    public event Action BoostEnabled;
    public event Action BoostDisabled;
    public event Action<Vector3> MoveOrdered;
    public event Action JumpOrdered;

    private void Update()
    {
        ProcessInput();
    }

    private void FixedUpdate()
    {
        if (_direction == Vector3.zero)
            return;

        MoveOrdered?.Invoke(_direction);
    }

    private void ProcessInput()
    { 
        _direction.Set(Input.GetAxisRaw(Horizontal), 0f, Input.GetAxisRaw(Vertical));
        _direction.Normalize();

        if(Input.GetKeyDown(_jumpKey))
        {
            JumpOrdered?.Invoke();
        }

        if (Input.GetKeyUp(_boostKey))
        {
            BoostDisabled?.Invoke();
        }

        if (_direction == Vector3.zero)
            return;

        if (Input.GetKeyDown(_boostKey))
        {
            BoostEnabled?.Invoke();
        }
    }
}
