using UnityEngine;

public class BallAnimation : MonoBehaviour
{
    [SerializeField] private JumpBehaviour _jumpBehaviour;
    [SerializeField] private BounceBehaviour _bounceBehaviour;
    [SerializeField] private Vector3 _defaultPosition;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _jumpBehaviour.Jumped += OnJumped;
        _bounceBehaviour.Bounced += OnBounced;
    }

    private void OnDisable()
    {
        _jumpBehaviour.Jumped -= OnJumped;
        _bounceBehaviour.Bounced -= OnBounced;
    }

    private void OnJumped()
    {
        transform.position = _defaultPosition;
        _animator.Play("Jump");
    }

    private void OnBounced(Vector3 hitPoint, Vector3 normal)
    {
        transform.position = hitPoint;
        transform.up = normal;
        _animator.Play("Bounce");
    }
}
