using UnityEngine;

public class BounceColliderBehaviour : MonoBehaviour
{
    [SerializeField] private float _density = 10f;
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private float _diveFactor = 0.5f;

    private float _divePercent;
    private Vector3 _applyingForce;
    private Rigidbody _rigidbody;
    private string _touchingObjectName;
    private int _lastId;
    private Vector3 _direction;

    private void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        int id = other.GetHashCode();

        if (id != _lastId)
        {
            _lastId = id;
            _density = 50f * _rigidbody.velocity.magnitude;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        print("a");

        Vector3 direction = other.ClosestPoint(transform.position) - transform.position;
        
        if(Physics.Raycast(transform.position, direction, out RaycastHit hitInfo))
        {
            _direction = hitInfo.normal;
            _touchingObjectName = hitInfo.collider.name;
        }
        else
        {
            _direction = Vector3.up;
            _touchingObjectName = "None";
        }
        
        _divePercent = -(_direction.z) + _diveFactor;
        
        _divePercent = Mathf.Clamp01(_divePercent);
        _applyingForce = _density * _divePercent * _direction;
        
        _density = 10f;
        _rigidbody.AddForce(_applyingForce);
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 200, 300, 200));
        GUILayout.Label($"Touching ({_touchingObjectName})");
        GUILayout.EndArea();
    }
}
