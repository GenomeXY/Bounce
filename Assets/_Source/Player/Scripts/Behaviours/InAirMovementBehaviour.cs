using UnityEngine;

public class InAirMovementBehaviour : MonoBehaviour
{
    [SerializeField] private float _forceReduceFactor = 0.7f;
    [SerializeField] private float _limitSpeedToForce = 10f;
    [SerializeField] private float _gravityFactor = 0.5f;

    [SerializeField] private Transform _pivot;
    [SerializeField] private float _inAirForce = 7f;

    private float _actualForce;

    private GroundSensor _groundSensor;
    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _groundSensor = GetComponent<GroundSensor>();
        _playerInput = FindObjectOfType<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _playerInput.MoveOrdered += OnMoved;
    }

    private void OnDisable()
    {
        _playerInput.MoveOrdered -= OnMoved;
    }

    private void FixedUpdate()
    {
        if (_groundSensor.IsGrounded == false)
        {
            _rigidbody.AddForce(_gravityFactor * Physics.gravity.y * Vector3.up);
        }
    }

    private void OnMoved(Vector3 direction)
    {
        if (direction == Vector3.zero || _groundSensor.IsGrounded)
        {
            return;
        }

        _actualForce = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z).magnitude < _limitSpeedToForce
            ? _inAirForce
            : _inAirForce * _forceReduceFactor;

        _rigidbody.AddForce(direction.z * _actualForce * _pivot.forward, ForceMode.Force);
        _rigidbody.AddForce(direction.x * _actualForce * _pivot.right, ForceMode.Force);
    }
}
