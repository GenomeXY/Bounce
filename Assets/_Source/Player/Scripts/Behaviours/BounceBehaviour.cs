using System.Collections.Generic;
using UnityEngine;

public class BounceBehaviour : MonoBehaviour
{
    int touch = 0;
    [SerializeField] private float _PressedInTimeJumpScaler = 1.2f;
    [SerializeField] private Collider _collider;
    [SerializeField] private GroundSensor _groundSensor;
    [SerializeField] private JumpBehaviour _jumpBehaviour;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Speedometer _speedometer;

    Queue<float> lastFrameSpeedCache = new();
    float lastFrameSpeed;

    public bool IsJumpOrdered { get; set; }

    private void Awake()
    {
        lastFrameSpeedCache.Enqueue(_rigidbody.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        lastFrameSpeed = lastFrameSpeedCache.Dequeue();
        lastFrameSpeedCache.Enqueue(_rigidbody.velocity.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isVertical = IsVertical(other);
        ++touch;
        print($"Touch {touch}.IsJumpOrdered({IsJumpOrdered}) - Speed({_rigidbody.velocity.magnitude:0.0})");
        if (IsJumpOrdered && isVertical)
        {
            ResetBounciness();
        }
        else
        {
            SetBounciness(isVertical);
        }
    }
    // TODO: Баг. Если в прыжке нажать ещё раз прыжок и больше ничего не трогать - мяч при приземлении один раз отпрыгнет от земли, но второй раз столкновение будет просто мгновенной остановкой, без упругости. 
    private void ResetBounciness()
    {
        _collider.material.bounciness = default;
        _jumpBehaviour.SetForce(_rigidbody.velocity.magnitude * _PressedInTimeJumpScaler);
        IsJumpOrdered = false;
    }

    private void SetBounciness(bool isVertical)
    {
        float speed = NormalizedSpeed();
        print($"Touch {touch}.BEFORE({speed:0.0})");
        if (isVertical)
        {
            speed = Mathf.Min(speed, 0.2f);
        print($"Touch {touch}.AFTER({speed:0.0})");
        }
        else
        {
            speed = Mathf.Min(speed, 0.9f);
        }

        
        _collider.material.bounciness = speed;
    }

    private bool IsVertical(Collider other)
    {
        Vector3 distance = (other.ClosestPointOnBounds(transform.position) - transform.position);

        Physics.Raycast(transform.position, distance, out RaycastHit hit);
        bool isVertical = Vector3.Dot(hit.normal, Vector3.up) >= 0.5f;
        return isVertical;
    }

    private float NormalizedSpeed()
    {
        float currentMaxSpeed = Mathf.Max(_speedometer.LastMaxSpeed, _speedometer.MaxLinearSpeed);
        _speedometer.ResetLastMaxSpeed();

        float speed = lastFrameSpeed / currentMaxSpeed;
        print($"Touch {touch}.FORMULA({lastFrameSpeed:0.0} / {currentMaxSpeed:0.0} = {speed:0.0})");
        return speed;
    }
}
