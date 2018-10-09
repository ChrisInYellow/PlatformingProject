using UnityEngine;
using System.Collections;

public class GravityGun : MonoBehaviour
{
    //public float targetScale = 0.1f;
    public float shrinkSpeed = 0.1f;
    public bool shrinking = false;
    public SpriteRenderer sprite;
    public GameObject PullOBJ;
    public float ForceSpeed;
    public Vector3 targetScale = new Vector3(0.2f, 0.2f, 0.2f);
    public bool inTrigger;
    
    public GameObject startMarker;
    public GameObject endMarker;

    // Movement speed in units/sec.
    public float speed = 1.0F;

    // Time when the movement started.
    public float startTime;

    // Total distance between the markers.
    public float journeyLength;

    

    // Follows the target position like with a spring
    
    void Start()
    {
        

        // Calculate the journey length.
        
        inTrigger = false;
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == ("Pullable"))
        {
            startMarker = coll.gameObject;
            journeyLength = Vector3.Distance(startMarker.transform.position, endMarker.transform.position);
            PullOBJ = coll.gameObject;
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == ("Pullable"))
        {
            //startMarker = null;
            //PullOBJ = null;
            inTrigger = false;
        }
    }


    void Shrink()
    {
        if (PullOBJ != null && PullOBJ.transform.localScale.sqrMagnitude > targetScale.sqrMagnitude)
        {
            
            shrinking = true;
        }
    }

    void Update()
    {

        if (inTrigger && Input.GetMouseButton(1))
        {
        Shrink();
        }
        if (shrinking)
        {
            
            float distCovered = Time.deltaTime * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;
            Debug.Log(distCovered);
            // Set our position as a fraction of the distance between the markers.
            

            PullOBJ.transform.position = Vector3.Lerp(startMarker.transform.position, endMarker.transform.position, fracJourney);
            //(PullOBJ.transform.position,
            // transform.position,
            // ForceSpeed * Time.deltaTime);
            PullOBJ.transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
        }
        if (PullOBJ != null && PullOBJ.transform.localScale.sqrMagnitude < targetScale.sqrMagnitude)
        {
            shrinking = false;
            startMarker = null;
            PullOBJ.SetActive(false);
            PullOBJ = null;
            
        }
    }
}
