using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour {
    float xPos;
    float halfOfScreen;
    public float speed = 5f;
    TestMovement movement;
    GameObject player;
    public bool facingRight;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movement = GameObject.FindGameObjectWithTag("Feet").GetComponent<TestMovement>();
    }
    private void Update()
    {
        xPos = Input.mousePosition.x;
        halfOfScreen = Screen.width / 2;
        if (xPos > halfOfScreen)
        {
            player.transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
            
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
            Debug.Log(direction.x);
            
        }
        else
        {
            player.transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
            
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
            
        }

    }

    
}
