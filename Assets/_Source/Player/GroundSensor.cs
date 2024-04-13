using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool IsGrounded { get; private set; }
    public float LastTouchDot { get; private set; }

    private void OnCollisionStay(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;

        LastTouchDot = Vector3.Dot(normal, Vector3.up);
        
        bool IsSameOrPerpendicular = LastTouchDot >= 0.5f;
        if (IsSameOrPerpendicular)
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        IsGrounded = false;
    }
}
