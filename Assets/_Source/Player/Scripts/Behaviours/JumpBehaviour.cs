using System;
using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    [SerializeField] private float DefaultJumpModifier = 1.5f;
    [SerializeField] private float _defaultJumpForce = 6f;
    private float _jumpForce;

    private JumpHelper _jumpHelper;
    private PlayerInput _playerInput;
    private GroundSensor _groundSensor;
    private Rigidbody _rigidbody;

    public bool CanJump { get; private set; }
    public float MaxJumpForce { get; private set; }

    public event Action Jumped;
    
    private void Awake()
    {
        _groundSensor = GetComponent<GroundSensor>();
        _playerInput = FindObjectOfType<PlayerInput>();
        _jumpHelper = GetComponent<JumpHelper>();
        _rigidbody = GetComponent<Rigidbody>();

        ResetForce();
    }

    private void OnEnable()
    {
        _playerInput.JumpOrdered += OnJumpOrdered;
        _groundSensor.Grounded += OnGrounded;
    }

    private void OnDisable()
    {
        _playerInput.JumpOrdered -= OnJumpOrdered;
        _groundSensor.Grounded -= OnGrounded;
    }

    private void OnJumpOrdered()
    {
        if (_groundSensor.IsGrounded && CanJump)
        {
            Jump();
        }
        else
        {
            _jumpHelper.RunHelper(Jump);
        }
    }

    private void Jump()
    {
        Jumped?.Invoke();

        _rigidbody.velocity.Set(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.AddForce(new Vector3(0f, _jumpForce, 0f), ForceMode.Impulse);
        
        ResetForce();
        CanJump = false;
    }

    public void SetForce(float value)
    {
        if (value > MaxJumpForce)
            throw new ArgumentOutOfRangeException();

        _jumpForce = value;
    }

    private void ResetForce()
    {
        _jumpForce = _defaultJumpForce;
        MaxJumpForce = _defaultJumpForce * DefaultJumpModifier;
    }

    private void OnGrounded()
    {
        CanJump = true;
    }
}
