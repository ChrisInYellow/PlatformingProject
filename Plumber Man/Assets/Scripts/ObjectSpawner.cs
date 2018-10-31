using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {
    public GameObject[] objects;
   // public int type;

	void Start () {

        /*var objectPrefabToSpawn = Resources.Load("Room_" + type);
        Instantiate(objectPrefabToSpawn, transform);*/

        int rand = Random.Range(0, objects.Length);
        GameObject instance = (GameObject)Instantiate(objects[rand], 
        transform.position, Quaternion.identity);
        instance.transform.parent = transform; 
	}
}
