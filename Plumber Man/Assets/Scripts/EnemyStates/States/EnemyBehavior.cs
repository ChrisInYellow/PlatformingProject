using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyStates.StateMachine; 

public class EnemyBehavior : MonoBehaviour {

    public enum States
    {
        Idle,
        Patrol,
        Hunt,
        Attack,
        Jump
    }

    private StateMachine<States> fsm;

    private void Awake()
    {
        fsm = StateMachine<States>.Initialize(this); 
    }
    // Use this for initialization
    void Start () {

        fsm.ChangeState(States.Patrol); 
	}
	
    private void Patrol_Enter()
    {
        print("Starting Patrol"); 
    }

    private void Patrol_Update()
    {

        if(Input.anyKeyDown)
        {
            print("Receiving input");
            fsm.ChangeState(States.Attack); 
        }

    }

    private void Attack_Enter()
    {
        print("Beginning Attack!");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
