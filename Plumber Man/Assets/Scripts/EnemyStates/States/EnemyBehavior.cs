﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyStates.StateMachine; 

public class EnemyBehavior : MonoBehaviour {

    public bool movingRight = true;
    public bool requiresFlipping = false;
    public float speed;
    public float distance; 

    public enum States
    {
        Idle,
        Patrol,
        Hunt,
        Attack,
        Jump
    }

    private Vector2 currentTargetPosition;
    private int pathIndex = 0;
    private float maxX;
    private float minX; 
    private StateMachine<States> fsm;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        fsm = StateMachine<States>.Initialize(this); 
    }
    // Use this for initialization
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
        Vector3 newPos = new Vector3();
        newPos.x += speed * Time.deltaTime;
        transform.position = movingRight ? transform.position + newPos : transform.position - newPos;

        CheckEndofPath();
    }

    private void Attack_Enter()
    {
        print("Beginning Attack!");
    }
	// Update is called once per frame
	void Update () {
		
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
