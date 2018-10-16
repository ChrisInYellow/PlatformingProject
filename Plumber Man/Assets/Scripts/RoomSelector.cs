using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSelector : MonoBehaviour {

    public int type; 

    public void RoomDestruction()
    {
        Destroy(gameObject);
    }
}
