using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GravityGun : MonoBehaviour
{
    public float shrinkSpeed = 0.1f;
    public bool shrinkingSmallObject = false;
    public bool shrinkingLargeObject = false;
    public SpriteRenderer sprite;
    public GameObject PullOBJ;
    public float ForceSpeed;
    public Vector3 targetScale = new Vector3(1.2f, 1.2f, 1.2f);
    public bool smallObjectInTrigger;
    public bool largeObjectInTrigger;
    public GameObject startMarker;
    public GameObject endMarker;
    public float speed = 1.0F; 
    public float startTime; 
    public float journeyLength;
    public GameObject gun;
    public List<GameObject> items = new List<GameObject>();
    public GameObject objectShot;
    public Transform shootPos;
    
    void Start()
    {
        
        largeObjectInTrigger = false;
        smallObjectInTrigger = false;
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == ("SmallObject"))
        {
            startMarker = coll.gameObject;
            journeyLength = Vector3.Distance(startMarker.transform.position, endMarker.transform.position);
            PullOBJ = coll.gameObject;
            smallObjectInTrigger = true;
            largeObjectInTrigger = false;
        }
        else if (coll.gameObject.tag == ("LargeObject"))
        {
            startMarker = coll.gameObject;
            journeyLength = Vector3.Distance(startMarker.transform.position, endMarker.transform.position);
            PullOBJ = coll.gameObject;
            largeObjectInTrigger = true;
            smallObjectInTrigger = false;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == ("SmallObject"))
        {
            smallObjectInTrigger = false;
        }
        else if (coll.gameObject.tag == ("LargeObject"))
        {
            largeObjectInTrigger = false;
        }
    }

    void ShootObject()
    {
        Debug.Log(PullOBJ);
            GameObject objectShot = items[items.Count - 1];
        
        
        objectShot.SetActive(true);
        objectShot.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        objectShot.GetComponent<ItemShot>().Shoot();
        

        Debug.Log("Instantiated");
            items[items.Count - 1].transform.parent = null;
            StartCoroutine(ScaleOverTime(0.5f, objectShot));
            //objectShot = null;

            items.Remove(items[items.Count - 1]);
            Debug.Log("Removed");
        
        
        
    }

    void ShrinkSmallObject()
    {
        if (PullOBJ != null && PullOBJ.transform.localScale.sqrMagnitude > targetScale.sqrMagnitude)
        {
            shrinkingSmallObject = true;
        }
    }

    void ShrinkLargeObject()
    {
        if (PullOBJ != null && PullOBJ.transform.localScale.sqrMagnitude > targetScale.sqrMagnitude)
        {
            shrinkingLargeObject = true;
        }
    }

    void Update()
    {

        if (items.Count < 3 && smallObjectInTrigger && Input.GetMouseButton(1))
        {
            ShrinkSmallObject();
        }

        if (largeObjectInTrigger && Input.GetMouseButton(1))
        {
        ShrinkLargeObject();
        }
        if (!items.Count.Equals(0) && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shot");
            
            ShootObject();
        }

        if (shrinkingSmallObject)
        {
            float distCovered = Time.deltaTime * speed;
            float fracJourney = distCovered / journeyLength;
            PullOBJ.GetComponent<BoxCollider2D>().enabled = false;
            startMarker.transform.position = Vector3.MoveTowards(startMarker.transform.position, endMarker.transform.position, speed * Time.deltaTime / 2f);
            PullOBJ.transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
        }

        if (shrinkingLargeObject)
        {

            float distCovered = Time.deltaTime * speed;
            float fracJourney = distCovered / journeyLength;
            PullOBJ.GetComponent<BoxCollider2D>().enabled = false;
            startMarker.transform.position = Vector3.MoveTowards(startMarker.transform.position, endMarker.transform.position, speed * Time.deltaTime / 2f);
            PullOBJ.transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed * 4f;
        }
        if (shrinkingLargeObject && PullOBJ != null && PullOBJ.transform.localScale.sqrMagnitude < targetScale.sqrMagnitude)
        {

            smallObjectInTrigger = false;
            largeObjectInTrigger = false;
            shrinkingSmallObject = false;
            shrinkingLargeObject = false;
            startMarker = null;
            items.Add(PullOBJ);
            PullOBJ.transform.parent = gun.transform;
            PullOBJ.transform.rotation = transform.parent.rotation;
            PullOBJ.SetActive(false);
            PullOBJ = null;

        }
        if (shrinkingSmallObject && PullOBJ != null && PullOBJ.transform.localScale.sqrMagnitude < targetScale.sqrMagnitude)
        {
            
            smallObjectInTrigger = false;
            largeObjectInTrigger = false;
            shrinkingSmallObject = false;
            shrinkingLargeObject = false;
            startMarker = null;
            items.Add(PullOBJ);
            PullOBJ.transform.parent = gun.transform;
            PullOBJ.transform.rotation = transform.parent.rotation;
            PullOBJ.SetActive(false);
            PullOBJ = null;

        }

    }

    IEnumerator ScaleOverTime(float time, GameObject objectShot)
    {
        
        
        Vector3 originalScale = objectShot.transform.localScale;

        Vector3 destinationScale = new Vector3(1f, 1f, 1f);
        
        float currentTime = 0.0f;
        Debug.Log(currentTime);
        do
        {
            Debug.Log("Hey");
            objectShot.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            
            yield return null;
        } while (currentTime <= time);


    }

}

