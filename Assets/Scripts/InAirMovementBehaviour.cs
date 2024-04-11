using UnityEngine;

public class InAirMovementBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _pivot;
    [SerializeField] private float _inAirForce;

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
        _playerInput.Moved += OnMoved;
    }

    private void OnDisable()
    {
        _playerInput.Moved -= OnMoved;
    }

    private void OnMoved(Vector3 direction)
    {
        if (_groundSensor.IsGrounded == false)
        {
            _rigidbody.AddForce(direction.z * _inAirForce * _pivot.forward, ForceMode.Force);
            _rigidbody.AddForce(-direction.x * _inAirForce * _pivot.right, ForceMode.Force);
        }
    }
}
