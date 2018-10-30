using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public int type;

	void Start () {

        var objectPrefabToSpawn = Resources.Load("Room_" + type);
        Instantiate(objectPrefabToSpawn, transform);

        //GameObject instance = (GameObject)Instantiate(objects[rand], 
        //    transform.position, Quaternion.identity);
        //instance.transform.parent = transform; 
	}
}
