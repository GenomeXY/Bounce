using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _observer;
    [SerializeField] private float _torqueSpeed;
    
    private Rigidbody _rigidbody;
    private float _xInput;
    private float _zInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _xInput = -Input.GetAxisRaw("Horizontal");
        _zInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        _rigidbody.AddTorque(_xInput * _torqueSpeed * _observer.forward);
        _rigidbody.AddTorque(_zInput * _torqueSpeed * _observer.right);
    }
}
