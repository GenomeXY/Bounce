using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointsManager : MonoBehaviour
{
    public Vector3 LatestPlayerPosition;
    private Player _player;


    private void Awake()
    {
        _player = FindAnyObjectByType<Player>();
    }

    //Если игрок умрет до того, как прошел чек-поинт - спавним на начальную позицию
    private void Start()
    {
        LatestPlayerPosition = _player.transform.position;
    }

    // private void OnEnable()
    // {
    //     Player.Died += OnCharacterDied;
    // }
    //
    // private void OnDisable()
    // {
    //     Player.Died -= OnCharacterDied;
    // }
    private void OnCharacterDied()
    {

        //Пока примитивно, слишком резко переключается, надо чем то сгладить
        _player.transform.position = LatestPlayerPosition;


    }



}
