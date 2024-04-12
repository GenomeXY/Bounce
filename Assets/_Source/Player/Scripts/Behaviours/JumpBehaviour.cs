using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    private PlayerInput _player;
    private Rigidbody _rigidbody;
    private GroundSensor _groundSensor;

    private void Awake()
    {
        _groundSensor = GetComponent<GroundSensor>();
        _player = FindObjectOfType<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
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
            var force = new Vector3(0f, _jumpForce, 0f);
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}
