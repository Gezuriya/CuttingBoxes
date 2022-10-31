using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }

    public float moveSpeed = 1f;

    private void OnEnable()
    {
        if (LastCube == null)
        {
            LastCube = GameObject.Find("StartCube").GetComponent<MovingCube>() ;
        }
        CurrentCube = this;
    }
    public void Stop()
    {
        moveSpeed = 0;
        float hangover = transform.position.z - LastCube.transform.position.z;
        float direction = hangover > 0 ? 1 : -1;
        SplitCubeOnZ(hangover, direction);
    }
    void SplitCubeOnZ(float hangover, float direction)
    {
        float newZSize = LastCube.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;

        float newZPosition = LastCube.transform.position.z + (hangover / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2 * direction);
        float fallingZPosition = cubeEdge + fallingBlockSize/2 * direction;

        SpawnFallCube(fallingZPosition, fallingBlockSize);
    }

    private void SpawnFallCube(float fallingZPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingZPosition);

        cube.AddComponent<Rigidbody>();
        Destroy(cube, 1);
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
}
