using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {

    public float propulsionForce; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.tag + " has been triggered."); 
        if(other.tag == "Player")
        {
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2
                (other.transform.position.x, propulsionForce), ForceMode2D.Impulse);
        }
    }
}
