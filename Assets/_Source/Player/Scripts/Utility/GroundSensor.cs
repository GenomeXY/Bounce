using System;
using System.Collections;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public event Action Grounded;

    [SerializeField] private float _delay;
    
    private JumpBehaviour _jumpBehaviour;
    private Coroutine _coroutine;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        _jumpBehaviour = GetComponent<JumpBehaviour>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsVertical(collision))
        {
            IsGrounded = true;
            Grounded?.Invoke();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (IsVertical(collision))
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(_jumpBehaviour.CanJump == false)
        {
            IsGrounded = false;
        }
        else if (IsGrounded)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ResetIsGroundedDelayed());
        }
    }

    private bool IsVertical(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;

        bool isVertical = Vector3.Dot(normal, Vector3.up) >= 0.5f;
        return isVertical;
    }

    private IEnumerator ResetIsGroundedDelayed()
    {
        float timer = _delay;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        IsGrounded = false;
    }
}
