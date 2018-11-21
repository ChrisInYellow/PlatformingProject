using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyStates.StateMachine; 

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

          /*  RaycastHit2D hit = Physics2D.Raycast(transform.position, sprite.transform.forward);
            Debug.DrawRay(hit.transform.position,sprite.transform.forward); */
            

         /*  if(hit.collider.tag == "Player")
            {
                foundPlayer = true; 
            }*/
        }
    }

    private IEnumerator Hunt_Enter()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPos = player.transform.position;
        foundPlayer = false; 
        yield return null;
        float huntSpeed = speed *2;
        playerPos.x += huntSpeed * Time.deltaTime; 
        transform.position = movingRight ? transform.position + playerPos : transform.position - playerPos;
        yield return new WaitForSeconds(huntingDuration);
        fsm.ChangeState(States.Patrol, StateTransition.Safe); 
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
