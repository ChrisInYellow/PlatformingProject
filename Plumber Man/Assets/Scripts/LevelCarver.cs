/*using System.Collections;
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
       public float minY; 

    public float width;
    public float depth; 

    private float timeBtwRoom;
    private bool levelFinished;

    private int downCounter;



    private void Awake()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[1], transform.position, Quaternion.identity);
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
        if(carvingDir == Directions.Right)
        {
            if(transform.position.x < width)
            {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x + moveIncrement, transform.position.y);
                transform.position = pos; 

                int randRoom = Random.Range(1, 4);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

            }
            else
            {
                carvingDir = Directions.Down; 
            }
        }
        else if(carvingDir == Directions.Left)
        {
            if(transform.position.x > 0)
            {
                downCounter = 0;
                Vector2 pos = new Vector2(transform.position.x - moveIncrement, transform.position.y);
                transform.position = pos;

                int randRoom = Random.Range(1, 4);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);
            }
            else
            {
                carvingDir = Directions.Down; 
            }
        }
        else if (carvingDir == Directions.Down)
        {
            downCounter++; 
            if(transform.position.y > depth)
            {
                Collider2D previousRoom = Physics2D.OverlapCircle(transform.position, 1, roomLayer); 

                if (previousRoom.GetComponent<RoomSelector>().type != 4 && previousRoom.GetComponent<RoomSelector>().type != 2)
                {
                    if(downCounter >=2)
                    {
                        previousRoom.GetComponent<RoomSelector>().RoomDestruction();
                        Instantiate(rooms[4], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        previousRoom.GetComponent<RoomSelector>().RoomDestruction();
                        int randRoomDownOpening = Random.Range(2, 5);
                        if (randRoomDownOpening == 3)
                        {
                            randRoomDownOpening = 2; 
                        }
                        Instantiate(rooms[randRoomDownOpening], transform.position, Quaternion.identity);
                    }
                }

                Vector2 pos = new Vector2(transform.position.x, transform.position.y - moveIncrement);
                transform.position = pos;

                int randRoom = Random.Range(3, 5);
                Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

                dir = Random.Range(1, 6);
            }
            else
            {
                levelFinished = true; 
            }
        }
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

}*/
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCarver : MonoBehaviour
{
    public Transform[] startingPositions;
    public Transform[] endPositons; 
    public float moveIncrement;
    public ObjectSpawner startRoom;
    public ObjectSpawner endRoom; 

    private ObjectSpawner[] path;
    private bool pathCreated;
    private enum Directions { None, Right, Left, Down }
    private Directions carvingDir;
    private int dir;
    private float width; 
    private int downCounter;

    private void Awake()
    {
        var spawningPoints = GameObject.FindObjectsOfType<ObjectSpawner>();
        CalculateGrid();
        CarvePath(spawningPoints); 
    }

    public void CarvePath(ObjectSpawner[] spawningPoints)
    {
        int randStartPos = Random.Range(0, 5);
        transform.position = startingPositions[randStartPos].position;
        
        if(startingPositions[randStartPos].GetComponent<ObjectSpawner>())
        {
            startRoom = startingPositions[randStartPos].GetComponent<ObjectSpawner>();
            startRoom.type = 1; 
        }

        for (int i = 0; i<spawningPoints.Length; i++)
        {
            CalculateDirection();

            if(transform.position == spawningPoints[i].transform.position)
            {
                var currentRoom = spawningPoints[i].GetComponent<ObjectSpawner>();

                if (carvingDir != Directions.Down)
                {
                    int randRoom = Random.Range(1, 4);
                    currentRoom.type = randRoom; 
                }
                if(carvingDir == Directions.Down)
                {
                    if (downCounter >= 2)
                    {
                        //previousRoom.GetComponent<RoomSelector>().RoomDestruction();
                        //Instantiate(rooms[4], transform.position, Quaternion.identity);
                        currentRoom.type = 4; 
                    }
                    else
                    {
                        //previousRoom.GetComponent<RoomSelector>().RoomDestruction();
                        int randRoomDownOpening = Random.Range(2, 5);
                        if (randRoomDownOpening == 3)
                        {
                            randRoomDownOpening = 2;
                        }
                        currentRoom.type = randRoomDownOpening; 
                    }
                }
            }
            else
            {

            }
        }
    }

    public void CalculateDirection()
    {
        dir = Random.Range(1, 6);

        if (dir > 5)
        {
            if (dir == 1 || dir == 2)
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

    public void CalculateGrid()
    {
        for(int i = 0; i < startingPositions.Length; i++)
        {
            if(startingPositions[i].transform.position.x<startingPositions[i+1].transform.position.x)
            {
                width = startingPositions[i].transform.position.x;
            }
        }
    }
}
*/



