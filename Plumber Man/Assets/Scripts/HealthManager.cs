using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HealthManager : MonoBehaviour {
    public int health;
    public int numbofHearts;

    public Image[] hearts;
    public Sprite full_Heart;
    public Sprite empty_Heart; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdatePlayerHealth();
        DrawHearts();
    }

    public void UpdatePlayerHealth()
    {
            print("Current health is: " + health 
                + " Current numberOfHearts is: " + numbofHearts); 
        if (health > numbofHearts)
        {
            health = numbofHearts;
        }

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void DrawHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = full_Heart;
            }
            else
            {
                hearts[i].sprite = empty_Heart;
            }

            if (i < numbofHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

    }

}


