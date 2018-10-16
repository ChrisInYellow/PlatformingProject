using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    // Use this for initialization

    public GameObject[] objects;
    private GameObject test; 

	void Start () {
        int rand = Random.Range(0, objects.Length);
        GameObject instance = (GameObject)Instantiate(objects[rand], 
            transform.position, Quaternion.identity);
        instance.transform.parent = transform; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
