using UnityEngine;

public class Die : MonoBehaviour
{
    private void OnEnable()
    {
        Health.GameOver += DieObject;
    }

    private void OnDisable()
    {
        Health.GameOver -= DieObject;
    }

    //��� ����� ������ ���������
    private void DieObject()
    {
        gameObject.SetActive(false);
    }
}
