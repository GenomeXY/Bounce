using UnityEngine;

public class BounceBehaviour : MonoBehaviour
{
    [SerializeField] private float _PressedInTimeJumpScaler = 1.15f;
    [SerializeField] private Collider _collider;
    [SerializeField] private GroundSensor _groundSensor;
    [SerializeField] private JumpBehaviour _jumpBehaviour;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Speedometer _speedometer;

    public bool IsJumpOrdered { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        bool isVertical = IsVertical(other);
        if (IsJumpOrdered && isVertical)
        {
            ResetBounciness();
            print("ResetBounciness");
        }
        else
        {
            SetBounciness(isVertical);
            print("SetBounciness");
        }
    }
    // TODO: Решено(иногда появлялся вроде) - Баг. Если в прыжке нажать ещё раз прыжок и больше ничего не трогать - мяч при приземлении один раз отпрыгнет от земли, но второй раз столкновение будет просто мгновенной остановкой, без упругости. 
    private void ResetBounciness()
    {
        IsJumpOrdered = false;
        _collider.material.bounciness = default;

        float verticalMagnitude = new Vector3(0f, _rigidbody.velocity.y, 0f).magnitude;
        float jumpForce = Mathf.Min(verticalMagnitude * _PressedInTimeJumpScaler, _jumpBehaviour.MaxJumpForce);

        _jumpBehaviour.SetForce(jumpForce);
    }

    private void SetBounciness(bool isVertical)
    {
        const float minBounciness = 0.4f;
        const float maxBounciness = 0.9f;

        float speed = NormalizedSpeed();
        if (isVertical)
        {
            
            speed = minBounciness;
        }
        else
        {
            speed = Mathf.Clamp(speed, minBounciness, maxBounciness);
        }

        _collider.material.bounciness = speed;
        print(speed);
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

        float speed = _rigidbody.velocity.magnitude / currentMaxSpeed;
        return speed;
    }
}
