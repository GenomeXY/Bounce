using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject _healthIconPrefab; //������ ������
    [SerializeField] private List<GameObject> HealthIcons = new List<GameObject>(); //���� ��� ������ ������

    [SerializeField] private int _health = 5; //���-�� ������ ������

    public static Action GameOver; //��� ����� ���������

    private void OnEnable()
    {
        Crash.Damage += MinusHealth; //���������� ������������ � ������������
    }

    private void OnDisable()
    {
        Crash.Damage -= MinusHealth;
    }


    //�������� ������ �� ������ ��� ��������
    private void Start()
    {
        for (int i = 0; i < _health; i++)
        {
            GameObject newIcon = Instantiate(_healthIconPrefab, transform); //������� ������ ������
            HealthIcons.Add(newIcon); //������� �� � ������
        }
        DisplayHealth(); //���������� �� �� ������
    }
        
    //����������� ������ �� ������
    public void DisplayHealth()
    {
        for (int i = 0; i < HealthIcons.Count; i++)
        {
            if (i < _health)
            {
                HealthIcons[i].SetActive(true);
            }
            else
            {
                HealthIcons[i].SetActive(false);
            }
        }
    }

    //��������� ����� �������� 1 �����
    public void MinusHealth()
    {
        _health -= 1;
        DisplayHealth();

        if (_health == 0)
        {
            GameOver?.Invoke(); //���� ��������
        }
    }


}
