using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _pivot;
    [SerializeField] private float _defaultTorqueSpeed;
    [SerializeField] private float _boostTorqueSpeed;

    private float _torqueSpeed;

    private GroundSensor _groundSensor;
    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _torqueSpeed = _defaultTorqueSpeed;

        _groundSensor = GetComponent<GroundSensor>();
        _playerInput = FindObjectOfType<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _playerInput.BoostEnabled += OnBoostEnabled;
        _playerInput.BoostDisabled += OnBoostDisabled;
        _playerInput.MoveOrdered += OnMoveOrdered;
    }

    private void OnDisable()
    {
        _playerInput.BoostEnabled -= OnBoostEnabled;
        _playerInput.BoostDisabled -= OnBoostDisabled;
        _playerInput.MoveOrdered -= OnMoveOrdered;
    }

    private void OnBoostEnabled()
    {
        _torqueSpeed = _boostTorqueSpeed;
    }

    private void OnBoostDisabled()
    {
        _torqueSpeed = _defaultTorqueSpeed;
    }

    private void OnMoveOrdered(Vector3 direction)
    {
        if (_groundSensor.IsGrounded)
        {
            _rigidbody.AddTorque(-direction.x * _torqueSpeed * _pivot.forward);
            _rigidbody.AddTorque(direction.z * _torqueSpeed * _pivot.right);
        }
    }
}
