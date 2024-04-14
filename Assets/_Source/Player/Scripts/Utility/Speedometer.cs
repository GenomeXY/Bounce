using UnityEngine;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    private float maxAngleSpeed;

    public float MaxLinearSpeed => 15f;
    public float LastMaxSpeed { get; private set; }

    private void FixedUpdate()
    {
        LastMaxSpeed = Mathf.Max(LastMaxSpeed, _rigidbody.velocity.magnitude);
        maxAngleSpeed = Mathf.Max(maxAngleSpeed, _rigidbody.angularVelocity.magnitude);
    }

    public void ResetLastMaxSpeed()
    {
        LastMaxSpeed = 0f;
    }
}
