﻿using UnityEngine;
using System.Collections;

public class GravityGun : MonoBehaviour
{
    //public float targetScale = 0.1f;
    public float shrinkSpeed = 0.1f;
    public bool shrinking = false;
    public SpriteRenderer sprite;
    public GameObject PullOBJ;
    public float ForceSpeed;
    public Vector3 targetScale = new Vector3(0.1f, 0.1f, 0.1f);
    public bool inTrigger;

    public Transform startMarker;
    public Transform endMarker;

    // Movement speed in units/sec.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    

    // Follows the target position like with a spring
    
    void Start()
    {
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        inTrigger = false;
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == ("Pullable"))
        {
            PullOBJ = coll.gameObject;
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == ("Pullable"))
        {
            inTrigger = false;
        }
    }


    void Shrink()
    {
        if (PullOBJ.transform.localScale.sqrMagnitude > targetScale.sqrMagnitude)
            shrinking = true;
    }

    void Update()
    {

        if (inTrigger && Input.GetMouseButton(1))
        {
        Shrink();
        }
        if (shrinking)
        {
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength /2f;

            // Set our position as a fraction of the distance between the markers.
            

            PullOBJ.transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
            //(PullOBJ.transform.position,
            // transform.position,
            // ForceSpeed * Time.deltaTime);
            PullOBJ.transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
        }
        if (PullOBJ.transform.localScale.sqrMagnitude < targetScale.sqrMagnitude)
            shrinking = false;
    }
}