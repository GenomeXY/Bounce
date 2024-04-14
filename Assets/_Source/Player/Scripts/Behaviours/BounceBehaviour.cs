using System;
using System.Collections;
using UnityEngine;

public class BounceBehaviour : MonoBehaviour
{
    public event Action<Vector3, Vector3> Bounced;

    [SerializeField] private float _jumpScaler = 1.15f;
    [SerializeField] private Collider _collider;

    public bool IsJumpOrdered { get; set; }

    private JumpBehaviour _jumpBehaviour;
    private Rigidbody _rigidbody;
    private Speedometer _speedometer;

    private RaycastHit _raycastHit;
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

        Vector3 direction = ComputeClosestPoint(other);
        _raycastHit = GetHit(direction);

        if (IsJumpOrdered && IsVertical)
        {
            ResetBounciness();
        }
        else
        {
            SetBounciness();
            Bounced?.Invoke(_raycastHit.point, _raycastHit.normal);
        }

        _canEnter = false;
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 direction = ComputeClosestPoint(other);
        _raycastHit = GetHit(direction);

        IsVertical = Vector3.Dot(_raycastHit.normal, Vector3.up) >= 0.5f;
    }

    private Vector3 ComputeClosestPoint(Collider other)
    {
        if (other is MeshCollider meshCollider && meshCollider.convex == false)
        {
            return Vector3.down;
        }
        else
        {
            return other.ClosestPoint(transform.position) - transform.position;
        }
    }

    private RaycastHit GetHit(Vector3 direction)
    {
        Physics.Raycast(transform.position, direction, out RaycastHit hit);
        return hit;
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

        while (_collisionEnterDelayTimer > 0)
        {
            _collisionEnterDelayTimer -= Time.deltaTime;
            yield return null;
        }

        _canEnter = true;
    }
}
