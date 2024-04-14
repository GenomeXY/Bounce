using UnityEngine;

public class FollowBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Vector3 _offset = new Vector3(0f, -0.5f, 0f);

    private void LateUpdate()
    {
        transform.position = _target.position + _offset;
    }
}
