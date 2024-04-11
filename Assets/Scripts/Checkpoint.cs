using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckPointsManager _checkPointsManager;
    

    private void Awake()
    {
        _checkPointsManager = FindAnyObjectByType<CheckPointsManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _checkPointsManager.LatestPlayerPosition = other.transform.position;
        }
    }
}
