using UnityEngine;

public class Speedometer : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float maxAngleSpeed;

    public float MaxLinearSpeed => 15f;
    public float LastMaxSpeed { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        LastMaxSpeed = Mathf.Max(LastMaxSpeed, _rigidbody.velocity.magnitude);
        maxAngleSpeed = Mathf.Max(maxAngleSpeed, _rigidbody.angularVelocity.magnitude);
    }

    private void OnGUI()
    {
        GUILayout.Label($"V:                {_rigidbody.velocity.magnitude:0.0}");
        GUILayout.Label($"AngV:             {_rigidbody.angularVelocity.magnitude:0.0}");
        GUILayout.Label($"Max AngV:         {_rigidbody.maxAngularVelocity / _rigidbody.angularDrag:0.0}");
        GUILayout.Label($"Mathf Max V:      {LastMaxSpeed:0.0}");
        GUILayout.Label($"Mathf Max AngV:   {maxAngleSpeed:0.0}");
    }

    public void ResetLastMaxSpeed()
    {
        LastMaxSpeed = 0f;
    }
}
