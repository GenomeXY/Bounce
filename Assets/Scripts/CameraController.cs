using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _boostFOVFactor = 1.4f;

    private float _defaultFOV;
    private float _boostFOV;

    private Camera _mainCamera;
    private PlayerInput _playerInput;
    private Coroutine _coroutine;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _playerInput = FindObjectOfType<PlayerInput>();

        _defaultFOV = _mainCamera.fieldOfView;
        _boostFOV = _defaultFOV * _boostFOVFactor;
    }

    private void OnEnable()
    {
        _playerInput.BoostEnabled += OnBoostEnabled;
        _playerInput.BoostDisabled += OnBoostDisabled;
    }

    private void OnDisable()
    {
        _playerInput.BoostEnabled -= OnBoostEnabled;
        _playerInput.BoostDisabled -= OnBoostDisabled;
    }

    private void OnBoostEnabled()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeFOVAsync(_boostFOV));
    }

    private void OnBoostDisabled()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeFOVAsync(_defaultFOV));
    }

    private IEnumerator ChangeFOVAsync(float targetFOV)
    {
        while (Mathf.Approximately(_mainCamera.fieldOfView, targetFOV) == false)
        {
            _mainCamera.fieldOfView = Mathf.Lerp(_mainCamera.fieldOfView, targetFOV, Time.deltaTime);
            yield return null;
        }
    }
}
