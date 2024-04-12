using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject _healthIconPrefab; //иконки жизней
    [SerializeField] private List<GameObject> HealthIcons = new List<GameObject>(); //лист для иконок жизней

    [SerializeField] private int _health = 5; //кол-во жизней сейчас

    public static Action GameOver; //все жизни потрачены

    private void OnEnable()
    {
        Crash.Damage += MinusHealth; //произошлло столкновение с препятствием
    }

    private void OnDisable()
    {
        Crash.Damage -= MinusHealth;
    }


    //создание жизней на экране при загрузке
    private void Start()
    {
        for (int i = 0; i < _health; i++)
        {
            GameObject newIcon = Instantiate(_healthIconPrefab, transform); //создаем иконки жизней
            HealthIcons.Add(newIcon); //заносим их в список
        }
        DisplayHealth(); //активируем их на экране
    }
        
    //отображение жизней на экране
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

    //получение урона отнимает 1 жизнь
    public void MinusHealth()
    {
        _health -= 1;
        DisplayHealth();

        if (_health == 0)
        {
            GameOver?.Invoke(); //игра окончена
        }
    }


}
