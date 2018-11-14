using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour {
    float xPos;
    float halfOfScreen;
    public float speed = 5f;
    TestMovement movement;
    GameObject player;
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
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
            Debug.Log("SCREEN");
        }
        else
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
            player.transform.localScale = new Vector3(-1, 1, 1);
        }
        //if (transform.eulerAngles.z >= 270 && transform.eulerAngles.z <= 360 || transform.eulerAngles.z >= 0 && transform.eulerAngles.z <= 90)
        //{

        //    //movement.sprite.flipX = false;
        //    //movement.gunSprite.flipX = false;
        //}
        //else if (transform.eulerAngles.z <= 269 && transform.eulerAngles.z >= 91)
        //{

        //    //movement.sprite.flipX = true;
        //    //movement.gunSprite.flipX = true;

        //}

        //Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        //float angle = Mathf.Atan2(direction.y*-1, direction.x*-1) * Mathf.Rad2Deg;
        //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

    }

    
}
