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

    //все жизни игрока потрачены
    private void DieObject()
    {
        gameObject.SetActive(false);
    }
}
