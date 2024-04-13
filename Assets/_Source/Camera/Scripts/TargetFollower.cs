using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private float _inAirRotationFactor = 0.3f;
    [SerializeField] private Rigidbody _target;
    [SerializeField] private Transform _pivot;
    [SerializeField] private GroundSensor _groundSensor;

    private void LateUpdate()
    {
        _pivot.transform.position = _target.position;

        Vector3 horizontal = new Vector3(_target.velocity.x, 0f, _target.velocity.z);
        if (horizontal == Vector3.zero)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(horizontal);
        float speed = _target.velocity.magnitude * Time.deltaTime;

        if (_groundSensor.IsGrounded == false)
        {
            speed *= _inAirRotationFactor;
        }

        _pivot.transform.rotation = Quaternion.Lerp(_pivot.transform.rotation, targetRotation, speed);
    }
}
