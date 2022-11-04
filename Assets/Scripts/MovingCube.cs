using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set; }
    public static MovingCube LastCube { get; private set; }
    public MoveDirection MoveDirection { get; set; }

    public float moveSpeed = 1f;

    private void OnEnable()
    {
        if (LastCube == null)
        {
            LastCube = GameObject.Find("StartCube").GetComponent<MovingCube>() ;
        }
        CurrentCube = this;

        transform.localScale =  new Vector3 (LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
    }
    public void Stop()
    {
        moveSpeed = 0;
        float hangover = GetHangover();

        float max = MoveDirection == MoveDirection.Z ? LastCube.transform.localScale.z : LastCube.transform.localScale.x;
        if(Mathf.Abs(hangover) >= max)
        {
            LastCube = null;
            CurrentCube = null;
            // reset scene here
        }

        float direction = hangover > 0 ? 1 : -1;

        if(MoveDirection == MoveDirection.Z)
        {
            SplitCubeOnZ(hangover, direction);
        }
        else
            SplitCubeOnX(hangover, direction);

        LastCube = this;
    }

    private float GetHangover()
    {
        if(MoveDirection == MoveDirection.Z)
        {
            return transform.position.z - LastCube.transform.position.z;
        }
        else
            return transform.position.x - LastCube.transform.position.x;
    }

    void SplitCubeOnX(float hangover, float direction)
    {
        float newXSize = LastCube.transform.localScale.x - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.x - newXSize;

        float newXPosition = LastCube.transform.position.x + (hangover / 2);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXSize / 2 * direction);
        float fallingXPosition = cubeEdge + fallingBlockSize / 2 * direction;

        SpawnFallCube(fallingXPosition, fallingBlockSize);
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
        if(CurrentCube.gameObject != GameObject.Find("StartCube").gameObject)
        {
            print("here");
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            if(MoveDirection == MoveDirection.Z)
            {
                cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
                cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingZPosition);
            }
            else
            {
                cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
                cube.transform.position = new Vector3(fallingZPosition, transform.position.y, transform.position.z);
            }

            cube.AddComponent<Rigidbody>();
            Destroy(cube, 1);
        }
    }

    void Update()
    {
        if(MoveDirection == MoveDirection.Z)
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
        else
            transform.position += transform.right * Time.deltaTime * moveSpeed;
    }
}
