using System;
using System.Collections;
using UnityEngine;

public class JumpHelper : MonoBehaviour
{
    [SerializeField] private float _jumpHelperDelay;
    [SerializeField] private BounceBehaviour _bouncinessBehaviour;

    private float _jumpHelperTimer;
    private GroundSensor _groundSensor;
    private Coroutine _coroutine;

    private void Awake()
    {
        _groundSensor = GetComponent<GroundSensor>();
    }

    public void RunHelper(Action action)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(DelayJump(action));
    }

    private IEnumerator DelayJump(Action action)
    {
        _bouncinessBehaviour.IsJumpOrdered = true;
        _jumpHelperTimer = _jumpHelperDelay;

        while (_groundSensor.IsGrounded == false && _jumpHelperTimer > 0)
        {
            _jumpHelperTimer -= Time.deltaTime;
            yield return null;
        }

        if (_groundSensor.IsGrounded)
        {
            action();
        }

        _bouncinessBehaviour.IsJumpOrdered = false;
    }
}
