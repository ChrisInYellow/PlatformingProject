using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    Vector2 currentTargetPosition;

    public bool movingRight = true;
    public bool needsFlipping = false; 
    public float speed;
    public float distance = 1f; 

    private int pathIndex = 0; 
    private float maxX;
    private float minX;

    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        minX = transform.position.x - distance;
        maxX = transform.position.x + distance;

        currentTargetPosition = transform.position;
        currentTargetPosition.x += maxX;
    }

    void Start ()
    {
 /*     minX = transform.position.x - distance;
        maxX = transform.position.x + distance;

        currentTargetPosition = transform.position;
        currentTargetPosition.x += maxX; */
	}
	
	void Update ()
    {
        Vector3 newPos = new Vector3();
        newPos.x += speed * Time.deltaTime;
        transform.position = movingRight ? transform.position + newPos : transform.position - newPos;

        CheckEndofPath();
	}

    void CheckEndofPath()
    {
        if(movingRight)
        {
            if(maxX - transform.position.x < .1f || transform.position.x > maxX)
            {
                movingRight = false; 
            }
        }
        else
        {
            if(transform.position.x - minX < .1f || transform.position.x < minX)
            {
                movingRight = true; 
            }
        }
    }
}
