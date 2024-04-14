using UnityEngine;

public class BallGizmo : MonoBehaviour
{
    [Space(10)]
    [SerializeField] Transform _cameraPivot;
    [SerializeField] private float RayLengthTransform = 2f;
    [SerializeField] private bool LocalAxis;

    [Space(10)]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float RayLengthRigidbody = 1f;
    [SerializeField] private bool Velocity;
    [SerializeField] private bool AngularVelocity;

    [Space(10)]
    [SerializeField] BounceBehaviour _bounceBehaviour;
    [SerializeField] BallAnimation _ballAnimation;

    private void OnEnable()
    {
        _bounceBehaviour.Bounced += OnBounced;
    }

    private void OnDisable()
    {
        _bounceBehaviour.Bounced -= OnBounced;
    }

    private void LateUpdate()
    {
        if(LocalAxis)
        {
            Debug.DrawRay(_cameraPivot.position, _cameraPivot.forward * RayLengthTransform, Color.blue);
            Debug.DrawRay(_cameraPivot.position, _cameraPivot.up * RayLengthTransform, Color.green);
            Debug.DrawRay(_cameraPivot.position, _cameraPivot.right * RayLengthTransform, Color.red);
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

    private void OnBounced(Vector3 a, Vector3 b)
    {
        Debug.DrawRay(_ballAnimation.transform.position, _ballAnimation.transform.up, Color.red, 5f);
    }
}
