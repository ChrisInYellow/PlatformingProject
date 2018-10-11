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
        
        GameObject bullet = Instantiate(objectShot, shootPos.position, shootPos.rotation);
        Debug.Log(objectShot.transform.localScale);
        StartCoroutine(ScaleOverTime(0.5f, bullet));
        items.Remove(items[Random.Range(0, items.Count)]);        
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

        if (smallObjectInTrigger && Input.GetMouseButton(1))
        {
            ShrinkSmallObject();
        }

        if (largeObjectInTrigger && Input.GetMouseButton(1))
        {
        ShrinkLargeObject();
        }
        if (!items.Count.Equals(0) && Input.GetMouseButtonUp(0))
        {
            ShootObject();
        }

        if (shrinkingSmallObject)
        {
            float distCovered = Time.deltaTime * speed;
            float fracJourney = distCovered / journeyLength;
            
            startMarker.transform.position = Vector3.MoveTowards(startMarker.transform.position, endMarker.transform.position, speed * Time.deltaTime / 2f);
            PullOBJ.transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
        }

        if (shrinkingLargeObject)
        {

            float distCovered = Time.deltaTime * speed;
            float fracJourney = distCovered / journeyLength;
           
            startMarker.transform.position = Vector3.MoveTowards(startMarker.transform.position, endMarker.transform.position, speed * Time.deltaTime / 2f);
            PullOBJ.transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed * 4f;
        }

        if (PullOBJ != null && PullOBJ.transform.localScale.sqrMagnitude < targetScale.sqrMagnitude)
        {
            
            smallObjectInTrigger = false;
            largeObjectInTrigger = false;
            shrinkingSmallObject = false;
            shrinkingLargeObject = false;
            startMarker = null;
            items.Add(PullOBJ);
            PullOBJ.transform.parent = gun.transform;
            PullOBJ.SetActive(false);
            PullOBJ = null;

        }

    }

    IEnumerator ScaleOverTime(float time, GameObject bullet)
    {
        Debug.Log(bullet.transform.localScale);
        Vector3 originalScale = bullet.transform.localScale;

        Vector3 destinationScale = new Vector3(0.5f, 0.5f, 0.5f);

        float currentTime = 0.0f;

        do
        {
            Debug.Log("Hey");
            bullet.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);


    }

}

