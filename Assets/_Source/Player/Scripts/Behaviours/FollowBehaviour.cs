using UnityEngine;

public class FollowBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private bool _repeatRotation;
    [SerializeField] private bool _repeatTranslation;

    private void LateUpdate()
    {
        if (_repeatTranslation)
            transform.position = _target.position + _offset;

        if (_repeatRotation)
            transform.rotation = _target.rotation;
    }
}
