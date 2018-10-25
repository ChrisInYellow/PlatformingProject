using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCarver : MonoBehaviour
{


    public Transform[] startingPositions;
    public GameObject[] rooms; //0 == LR, 1 == LRB, 2 == LBT, 3 == LRBT
    public LayerMask roomLayer;

    public float moveIncrement;
    public float startTimeBtwRoom = 0.25f;

    /* public float minX;
       public float maxX;
       public float minY; */

    private float timeBtwRoom;
    private bool levelFinished;

    private int downCounter;

    private enum Directions { None, Right, Left, Down }
    private Directions carvingDir;
    private int dir;

    private void Awake()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;

        CalculateRoute();
    }

    private void Update()
    {
        if(!levelFinished)
        {
            CarveLevel();
        }
    }

    private void CarveLevel()
    {

    }

    private void CalculateRoute()
    {
        dir = Random.Range(1, 6); 

        if(dir >5)
        {
            if(dir %2 == 0)
            {
                carvingDir = Directions.Right; 
            }
            else
            {
                carvingDir = Directions.Left; 
            }
        }
        else if (dir == 5)
        {
            carvingDir = Directions.Down; 
        }
        
    }

}

	
