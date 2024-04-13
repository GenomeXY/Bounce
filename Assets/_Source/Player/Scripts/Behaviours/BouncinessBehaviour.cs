using UnityEngine;

public class BouncinessBehaviour : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private GroundSensor _groundSensor;

    private Rigidbody _rigidbody;
    public bool IsJumpHelperOrdered { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsJumpHelperOrdered)
        {
            IsJumpHelperOrdered = false;
            return;
        }

        float speed = NormalizedSpeed();
        float was = _collider.material.bounciness;
        _collider.material.bounciness = speed;
        print($"ENTER Before({was:0.0}) After({_collider.material.bounciness:0.0}) SPEED({speed})");
    }

    private float NormalizedSpeed()
    {
        return Mathf.Clamp01((_rigidbody.velocity.magnitude - 0) / (_rigidbody.maxAngularVelocity / _rigidbody.angularDrag - 0));
    }
}
