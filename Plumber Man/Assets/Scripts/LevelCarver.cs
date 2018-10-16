using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCarver : MonoBehaviour {


    public Transform[] startingPositions;
    public GameObject[] rooms; //0 == LR, 1 == LRB, 2 == LBT, 3 == LRBT
    public LayerMask roomLayer; 

    public float moveAmount;
    public float startTimeBtwRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY; 

    private int direction;
    private float timeBtwRoom;
    private bool levelFinished;

    private int downCounter; 

	// Use this for initialization
	void Start () {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);
	}
	
	// Update is called once per frame
	void Update () {
        if (timeBtwRoom <= 0 && levelFinished == false)
        {
            CarveRoute();
            timeBtwRoom = startTimeBtwRoom; 
        }
        else{
            timeBtwRoom -= Time.deltaTime; 
        }
	}

    void CarveRoute()
    {
        if(direction == 1 || direction == 2)
        {
            if(transform.position.x < maxX)
            {
                downCounter = 0; 
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity); 

                direction = Random.Range(1, 6); 

                if(direction == 3)
                {
                    direction = 2; 
                }
                else if(direction == 4)
                {
                    direction = 5; 
                }
            }
            else
            {
                direction = 5; 
            }
        }
        else if(direction == 3 || direction ==4)
        {
            if(transform.position.x > minX)
            {
                downCounter = 0; 
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5; 
            }
        }
        else if(direction == 5)
        {
            downCounter++; 

            if (transform.position.y>minY)
            {
                Collider2D roomDetector = Physics2D.OverlapCircle(transform.position, 1, roomLayer);

                if(roomDetector.GetComponent<RoomSelector>().type != 1 && roomDetector.GetComponent<RoomSelector>().type != 3)
                {
                    if(downCounter>=2)
                    {
                        roomDetector.GetComponent<RoomSelector>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDetector.GetComponent<RoomSelector>().RoomDestruction();

                        int randBottomRoom = Random.Range(1, 4); 
                        if(randBottomRoom == 2)
                        {
                            randBottomRoom = 1; 
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);

                    }

                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else
            {
                levelFinished = true; 
            }
        }

        //Instantiate(rooms[0], transform.position, Quaternion.identity);
        //direction = Random.Range(1, 6);
    }
}
