using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCarver : MonoBehaviour {


    public Transform[] startingPositions;
    public GameObject[] rooms;

    public float moveAmount;
    public float startTimeBtwRoom = 0.25f; 

    private int direction;
    private float timeBtwRoom; 

	// Use this for initialization
	void Start () {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);
	}
	
	// Update is called once per frame
	void Update () {
        if (timeBtwRoom <= 0)
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
            Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
            transform.position = newPos; 
        }
        else if(direction == 3 || direction ==4)
        {
            Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
            transform.position = newPos;
        }
        else if(direction == 5)
        {
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
            transform.position = newPos;
        }

        Instantiate(rooms[0], transform.position, Quaternion.identity);
        direction = Random.Range(1, 6);
    }
}
