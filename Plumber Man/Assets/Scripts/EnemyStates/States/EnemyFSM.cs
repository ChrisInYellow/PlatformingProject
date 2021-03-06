﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyStates.StateMachine; 

//FSM-architecture for one type of enemy. State-implementation opens up for additional enemy-types for potential expanded product.
public class EnemyFSM : IEnemyStates {

    public bool movingRight = true;
    public bool requiresFlipping = false;
    public float speed;
    public float distance; 
    public float huntingDuration; 

    private Vector2 currentTargetPosition;
    private int pathIndex = 0;
    private float maxX;
    private float minX;
    private StateMachine<States> fsm;
    private SpriteRenderer sprite;
    private bool foundPlayer;
    private GameObject player; 

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        fsm = StateMachine<States>.Initialize(this); 
    }

    void Start () {

        fsm.ChangeState(States.Patrol); 
	}
	
    private void Patrol_Enter()
    {
        minX = transform.position.x - distance;
        maxX = transform.position.x + distance;

        currentTargetPosition = transform.position;
        currentTargetPosition.x += maxX;

        print("Starting Patrol"); 
    }

    private void Patrol_Update()
    {
        if(foundPlayer)
        {
            fsm.ChangeState(States.Hunt, StateTransition.Overwrite); 
                Debug.Log("Hunting");
        }
        else
        {
            Vector3 newPos = new Vector3();
            newPos.x += speed * Time.deltaTime;
            transform.position = movingRight ? transform.position + newPos : transform.position - newPos;

            CheckEndofPath();
        }
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
