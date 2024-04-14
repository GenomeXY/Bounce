using Unity.VisualScripting;
using UnityEngine;

public class BounceBehaviour : MonoBehaviour
{
    [SerializeField] private float _PressedInTimeJumpScaler = 1.15f;
    [SerializeField] private Collider _collider;
    
    private JumpBehaviour _jumpBehaviour;
    private Rigidbody _rigidbody;
    private Speedometer _speedometer;

    public bool IsJumpOrdered { get; set; }

    private void Awake()
    {
        _jumpBehaviour = GetComponentInParent<JumpBehaviour>();
        _rigidbody = GetComponentInParent<Rigidbody>();
        _speedometer = GetComponentInParent<Speedometer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isVertical = IsVertical(other);
        if (IsJumpOrdered && isVertical)
        {
            ResetBounciness();
        }
        else
        {
            SetBounciness(isVertical);
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
        
        speed = isVertical 
            ? minBounciness 
            : Mathf.Clamp(speed, minBounciness, maxBounciness);

        _collider.material.bounciness = speed;
    }

    private bool IsVertical(Collider other)
    {
        RaycastHit hit;

        if (other is MeshCollider meshCollider && meshCollider.convex == false)
        {
            Physics.Raycast(transform.position, Vector3.down, out hit);
        }
        else
        {
            Vector3 distance = (other.ClosestPointOnBounds(transform.position) - transform.position);
            Physics.Raycast(transform.position, distance, out hit);
        }
        
        bool isVertical = Vector3.Dot(hit.normal, Vector3.up) >= 0.5f;
        print(Vector3.Dot(hit.normal, Vector3.up).ToString("0.0"));
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
