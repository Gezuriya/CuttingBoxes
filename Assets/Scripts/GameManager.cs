using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnCubeSpawned = delegate { };
    CubeSpawner[] spawners;
    CubeSpawner currentSpawner;
    private void Awake()
    {
        spawners = FindObjectsOfType<CubeSpawner>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (MovingCube.CurrentCube != null)
            {
                MovingCube.CurrentCube.Stop();
            }
            currentSpawner = spawners[UnityEngine.Random.Range(0,2)];
            currentSpawner.SpawnCube();
            OnCubeSpawned();
        }
    }
}
