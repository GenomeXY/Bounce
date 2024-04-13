using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 7f;
    
    private PlayerInput _player;
    private Rigidbody _rigidbody;
    private GroundSensor _groundSensor;
    private JumpHelper _jumpHelper;
    
    private void Awake()
    {
        _groundSensor = GetComponent<GroundSensor>();
        _player = FindObjectOfType<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _jumpHelper = GetComponent<JumpHelper>();
    }

    private void OnEnable()
    {
        _player.Jumped += OnJumped;
    }

    private void OnDisable()
    {
        _player.Jumped -= OnJumped;
    }

    private void OnJumped()
    {
        if (_groundSensor.IsGrounded)
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
        var force = new Vector3(0f, _jumpForce, 0f);
        
        _rigidbody.velocity.Set(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
        _rigidbody.AddForce(force, ForceMode.Impulse);
    }
}
