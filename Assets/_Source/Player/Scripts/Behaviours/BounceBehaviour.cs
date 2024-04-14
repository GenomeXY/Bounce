using System.Collections;
using UnityEngine;

public class BounceBehaviour : MonoBehaviour
{
    [SerializeField] private float _jumpScaler = 1.15f;
    [SerializeField] private Collider _collider;

    public bool IsJumpOrdered { get; set; }

    private JumpBehaviour _jumpBehaviour;
    private Rigidbody _rigidbody;
    private Speedometer _speedometer;

    private Vector3 _rayCastDirection;
    private bool IsVertical;
    private float _collisionEnterDelayTimer;
    private bool _canEnter;

    private void Awake()
    {
        _jumpBehaviour = GetComponentInParent<JumpBehaviour>();
        _rigidbody = GetComponentInParent<Rigidbody>();
        _speedometer = GetComponentInParent<Speedometer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StopCoroutine(CollisionEnterDelay());
        StartCoroutine(CollisionEnterDelay());

        if (_canEnter == false)
            return;

        if (IsJumpOrdered && IsVertical)
        {
            ResetBounciness();
        }
        else
        {
            SetBounciness();
        }

        _canEnter = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other is MeshCollider meshCollider && meshCollider.convex == false)
        {
            _rayCastDirection = Vector3.down;
        }
        else
        {
            _rayCastDirection = (other.ClosestPoint(transform.position) - transform.position);
        }

        Physics.Raycast(transform.position, _rayCastDirection, out RaycastHit hit);
        
        IsVertical = Vector3.Dot(hit.normal, Vector3.up) >= 0.5f;
    }

    private void ResetBounciness()
    {
        IsJumpOrdered = false;
        _collider.material.bounciness = default;

        float verticalMagnitude = new Vector3(0f, _rigidbody.velocity.y, 0f).magnitude;
        float jumpForce = Mathf.Min(verticalMagnitude * _jumpScaler, _jumpBehaviour.MaxJumpForce);

        _jumpBehaviour.SetForce(jumpForce);
    }

    private void SetBounciness()
    {
        const float minBounciness = 0.4f;
        const float maxBounciness = 0.9f;

        float speed = NormalizedSpeed();

        speed = IsVertical
            ? minBounciness
            : Mathf.Clamp(speed, minBounciness, maxBounciness);

        _collider.material.bounciness = speed;
    }

    private float NormalizedSpeed()
    {
        float currentMaxSpeed = Mathf.Max(_speedometer.LastMaxSpeed, _speedometer.MaxLinearSpeed);
        _speedometer.ResetLastMaxSpeed();

        float speed = _rigidbody.velocity.magnitude / currentMaxSpeed;
        return speed;
    }

    private IEnumerator CollisionEnterDelay()
    {
        _collisionEnterDelayTimer = 0.1f;

        while(_collisionEnterDelayTimer > 0)
        {
            _collisionEnterDelayTimer -= Time.deltaTime;
            yield return null;
        }

        _canEnter = true;
    }
}
