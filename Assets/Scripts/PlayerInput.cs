using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region stringHash
    private string Horizontal = "Horizontal";
    private string Vertical = "Vertical";

    private const string HorizontalAxis = nameof(Horizontal);
    private const string VerticalAxis = nameof(Vertical);
    #endregion

    [SerializeField] private KeyCode _boostKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    private Vector3 _direction = new Vector3();

    public event Action BoostEnabled;
    public event Action BoostDisabled;
    public event Action<Vector3> Moved;
    public event Action Jumped;

    private void Update()
    {
        ProcessInput();
    }

    private void FixedUpdate()
    {
        if (_direction == Vector3.zero)
            return;

        Moved?.Invoke(_direction);
    }

    private void ProcessInput()
    { 
        _direction.Set(-Input.GetAxisRaw(HorizontalAxis), 0f, Input.GetAxisRaw(VerticalAxis));

        if(Input.GetKeyDown(_jumpKey))
        {
            Jumped?.Invoke();
        }

        if (_direction == Vector3.zero)
            return;

        if (Input.GetKeyDown(_boostKey))
        {
            BoostEnabled?.Invoke();
        }
        else if(Input.GetKeyUp(_boostKey))
        {
            BoostDisabled?.Invoke();
        }
    }
}
