using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private MovingCube cubePref;
    [SerializeField]
    private MoveDirection moveDir;
    GameObject startCube;

    private void Start()
    {
        startCube = GameObject.Find("StartCube");
    }
    public void SpawnCube()
    {
        var cube = Instantiate(cubePref);

        if(MovingCube.LastCube != null && MovingCube.LastCube.gameObject != startCube)
        {
            float x = moveDir == MoveDirection.X ? transform.position.x : MovingCube.LastCube.transform.position.x;
            float z = moveDir == MoveDirection.Z ? transform.position.z : MovingCube.LastCube.transform.position.z;

            cube.transform.position = new Vector3(x, MovingCube.LastCube.transform.position.y + cubePref.transform.localScale.y, z);
        }
        else
        {
            cube.transform.position = transform.position;
        }
        cube.MoveDirection = moveDir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, cubePref.transform.localScale);
    }

}
    public enum MoveDirection
    {
        X,
        Z
    }
