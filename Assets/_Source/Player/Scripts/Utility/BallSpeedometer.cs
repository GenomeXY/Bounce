using UnityEngine;

public class BallSpeedometer : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float maxSpeed;
    private float maxAngleSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        maxSpeed = Mathf.Max(maxSpeed, _rigidbody.velocity.magnitude);
        maxAngleSpeed = Mathf.Max(maxAngleSpeed, _rigidbody.angularVelocity.magnitude);
    }

    private void OnGUI()
    {
        GUILayout.Label($"AngVelocity: {_rigidbody.angularVelocity.magnitude:0.0}");
        GUILayout.Label($"Max AngVelocity: {_rigidbody.maxAngularVelocity / _rigidbody.angularDrag:0.0}");
        GUILayout.Label($"Mathf Max Velocity: {maxSpeed:0.0}");
        GUILayout.Label($"Mathf Max AngVelocity: {maxAngleSpeed:0.0}");
    }
}
