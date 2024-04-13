using UnityEngine;

public class BounceBehaviour : MonoBehaviour
{
    [SerializeField] private float _PressedInTimeJumpScaler = 1.2f;
    [SerializeField] private Collider _collider;
    [SerializeField] private GroundSensor _groundSensor;
    [SerializeField] private JumpBehaviour _jumpBehaviour;

    private Rigidbody _rigidbody;
    public bool IsJumpOrdered { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsJumpOrdered)
        {
            _collider.material.bounciness = default;
            _jumpBehaviour.SetForce(_rigidbody.velocity.magnitude * _PressedInTimeJumpScaler);
            IsJumpOrdered = false;
            return;
        }

        float speed = NormalizedSpeed();

        Vector3 distance = (other.ClosestPointOnBounds(transform.position) - transform.position);

        Physics.Raycast(transform.position, distance, out RaycastHit hit);
        bool isVertical = Vector3.Dot(hit.normal, Vector3.up) >= 0.5f;

        if (isVertical)
        {
            speed *= 0.6f; 
        }
        else
        {
            speed = Mathf.Min(speed, 0.9f);
        }

        _collider.material.bounciness = speed;
    }

    private float NormalizedSpeed()
    {
        const float maxLinearSpeed = 15f;
        float maxSpeedModifier = 5f;

        float speed = Mathf.Clamp01((_rigidbody.velocity.magnitude - 0) / (maxLinearSpeed - maxSpeedModifier - 0));

        return speed;
    }
}
