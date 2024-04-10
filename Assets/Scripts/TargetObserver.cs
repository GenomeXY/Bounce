using UnityEngine;

public class TargetObserver : MonoBehaviour
{
    [SerializeField] private Rigidbody _target;

    private void LateUpdate()
    {
        if (_target.velocity == Vector3.zero)
            return;

        transform.position = _target.position;
        
        float targetSpeed = _target.velocity.magnitude * Time.deltaTime;

        transform.rotation = Quaternion.Lerp
            (transform.rotation, 
            Quaternion.LookRotation(_target.velocity),
            targetSpeed);
    }
}
