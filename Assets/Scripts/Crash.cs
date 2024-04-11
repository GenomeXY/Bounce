using UnityEngine;
using System;

public class Crash : MonoBehaviour
{
    public static Action Damage; //��������� ������������ ����������� � ������

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            Damage?.Invoke();
        }
    }
}
