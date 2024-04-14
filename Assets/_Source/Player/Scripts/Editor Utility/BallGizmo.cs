using UnityEngine;

public class BallGizmo : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] Transform _gameObject;
    [SerializeField] private float RayLengthTransform = 2f;
    [SerializeField] private bool LocalAxis;

    [Header("Rigidbody")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float RayLengthRigidbody = 1f;
    [SerializeField] private bool Velocity;
    [SerializeField] private bool AngularVelocity;

    private void LateUpdate()
    {
        if(LocalAxis)
        {
            Debug.DrawRay(_gameObject.position, _gameObject.forward * RayLengthTransform, Color.blue);
            Debug.DrawRay(_gameObject.position, _gameObject.up * RayLengthTransform, Color.green);
            Debug.DrawRay(_gameObject.position, _gameObject.right * RayLengthTransform, Color.red);
        }
        if(Velocity)
        {
            Debug.DrawRay(_rigidbody.position, _rigidbody.velocity * RayLengthRigidbody, Color.magenta);
        }
        if (AngularVelocity)
        {
            Debug.DrawRay(_rigidbody.position, _rigidbody.angularVelocity * RayLengthRigidbody, Color.cyan);
        }
    }

    private void OnGUI()
    {
        GUILayout.Label($"Velocity({_rigidbody.velocity.magnitude:0.0})");
        GUILayout.Label($"AngVelocity({_rigidbody.angularVelocity.magnitude:0.0})");
    }
}
