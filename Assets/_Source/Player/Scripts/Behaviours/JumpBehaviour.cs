using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    [SerializeField] private float _defaultJumpForce = 7f;
    [SerializeField] private float _maxJumpForce = 15f;
    private float _jumpForce;

    private PlayerInput _player;
    private Rigidbody _rigidbody;
    private GroundSensor _groundSensor;
    private JumpHelper _jumpHelper;

    private bool _canJump;

    private void Awake()
    {
        _groundSensor = GetComponent<GroundSensor>();
        _player = FindObjectOfType<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _jumpHelper = GetComponent<JumpHelper>();

        ResetForce();
    }

    private void OnEnable()
    {
        _player.JumpOrdered += OnJumpOrdered;
        _groundSensor.Grounded += OnGrounded;
    }

    private void OnDisable()
    {
        _player.JumpOrdered -= OnJumpOrdered;
        _groundSensor.Grounded -= OnGrounded;
    }

    private void OnJumpOrdered()
    {
        if (_groundSensor.IsGrounded && _canJump)
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
        print($"JUMP. canJump({_canJump})");
        _rigidbody.velocity.Set(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        _rigidbody.AddForce(new Vector3(0f, _jumpForce, 0f), ForceMode.Impulse);
        
        ResetForce();
        _canJump = false;
    }

    public void SetForce(float value)
    {
        _jumpForce = Mathf.Min(value, _maxJumpForce);
    }

    private void ResetForce()
    {
        _jumpForce = _defaultJumpForce;
    }

    private void OnGrounded()
    {
        _canJump = true;
    }

}
