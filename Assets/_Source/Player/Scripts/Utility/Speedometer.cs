using UnityEngine;

public class Speedometer : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float maxSpeed;
    private float maxAngleSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        maxSpeed = Mathf.Max(maxSpeed, _rigidbody.velocity.magnitude);
        maxAngleSpeed = Mathf.Max(maxAngleSpeed, _rigidbody.angularVelocity.magnitude);
    }

    private void OnGUI()
    {
        GUILayout.Label($"V:                {_rigidbody.velocity.magnitude:0.0}");
        GUILayout.Label($"AngV:             {_rigidbody.angularVelocity.magnitude:0.0}");
        GUILayout.Label($"Max LV:           {_rigidbody.maxLinearVelocity:0.0}");
        GUILayout.Label($"Max DV:           {_rigidbody.maxDepenetrationVelocity:0.0}");
        GUILayout.Label($"Max AngV:         {_rigidbody.maxAngularVelocity / _rigidbody.angularDrag:0.0}");
        GUILayout.Label($"Mathf Max V:      {maxSpeed:0.0}");
        GUILayout.Label($"Mathf Max AngV:   {maxAngleSpeed:0.0}");
    }
}
