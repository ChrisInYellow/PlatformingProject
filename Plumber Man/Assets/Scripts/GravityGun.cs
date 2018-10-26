using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class GravityGun : MonoBehaviour
{
    public float shrinkSpeed = 0.1f;
    public bool shrinkingSmallObject = false;
    public bool shrinkingLargeObject = false;
    public SpriteRenderer sprite;
    //public GameObject PullOBJ;
    public float ForceSpeed;
    public Vector3 targetScale = new Vector3(1.2f, 1.2f, 1.2f);
    public bool smallObjectInTrigger;
    public bool largeObjectInTrigger;
    //public GameObject startMarker;
    public List<GameObject> PullOBJ = new List<GameObject>();
    public List<GameObject> FirstPullOBJ = new List<GameObject>();
    public List<GameObject> startMarker = new List<GameObject>();
    public List<GameObject> FirstStartMarker = new List<GameObject>();
    public GameObject endMarker;
    public float speed = 1.0F; 
    public float startTime; 
    public float journeyLength;
    public GameObject gun;
    public List<GameObject> items = new List<GameObject>();
    public GameObject objectShot;
    public Transform shootPos;
    public bool shrinking;
    JumpPad jumpPad;
    void Start()
    {
        shrinking = false;
        largeObjectInTrigger = false;
        smallObjectInTrigger = false;
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == ("SmallObject") || coll.gameObject.tag == ("JumpPad"))
        {
            if (!PullOBJ.Contains(coll.gameObject))
            {
                
                //FirstStartMarker.Add(coll.gameObject);
                PullOBJ.Add(coll.gameObject);

            }
            //startMarker = coll.gameObject;
            //journeyLength = Vector3.Distance(startMarker[0].transform.position, endMarker.transform.position);
            //PullOBJ = coll.gameObject;
            
            //smallObjectInTrigger = true;
            largeObjectInTrigger = false;

        }
        //else if (coll.gameObject.tag == ("LargeObject"))
        //{
        //    startMarker.Add(coll.gameObject);

        //    //startMarker = coll.gameObject;
        //    journeyLength = Vector3.Distance(startMarker[0].transform.position, endMarker.transform.position);
        //    //PullOBJ = coll.gameObject;
        //    PullOBJ.Add(coll.gameObject);
        //    largeObjectInTrigger = true;
        //    smallObjectInTrigger = false;
        //}
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == ("SmallObject") || coll.gameObject.tag == ("JumpPad"))
        {
            Debug.Log("Exit");
            PullOBJ.Remove(coll.gameObject);
            //FirstStartMarker.Remove(coll.gameObject);
            //smallObjectInTrigger = false;

        }
        //else if (coll.gameObject.tag == ("LargeObject"))
        //{
        //    largeObjectInTrigger = false;
        //}
    }

    void ShootObject()
    {
        Debug.Log(PullOBJ);
            GameObject objectShot = items[items.Count - 1];
        
        
        objectShot.SetActive(true);
        objectShot.GetComponent<BoxCollider2D>().size = new Vector2(0.6f, 0.2f);
        objectShot.GetComponent<BoxCollider2D>().isTrigger = false;
        objectShot.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        objectShot.GetComponent<ItemShot>().Shoot();
        

        Debug.Log("Instantiated");
            items[items.Count - 1].transform.parent = null;
            StartCoroutine(ScaleOverTime(0.5f, objectShot));
            //objectShot = null;

            items.Remove(items[items.Count - 1]);
            Debug.Log("Removed");
        
        
        
    }

    //void ShrinkSmallObject()
    //{
        //foreach (GameObject pull in PullOBJ)
        //{
        //    if (pull != null && pull.transform.localScale.sqrMagnitude > targetScale.sqrMagnitude)
        //    {

        //        shrinkingSmallObject = true;
        //    }
        //}
        
    //}

    //void ShrinkLargeObject()
    //{
    //    if (PullOBJ != null && PullOBJ[0].transform.localScale.sqrMagnitude > targetScale.sqrMagnitude)
    //    {
    //        shrinkingLargeObject = true;
    //    }
    //}

    void Update()
    {

        if (items.Count < 3 && !shrinking && Input.GetMouseButtonDown(1))
        {
            
                Debug.Log("Copied");
                //PullOBJ = FirstPullOBJ;
                //startMarker = FirstStartMarker;
                
                foreach (GameObject pull in PullOBJ)
                {


                //if (pull.gameObject.tag == ("JumpPad"))
                //{
                //    pull.transform.GetChild(0).gameObject.SetActive(false);
                //}
                //Destroy(pull.GetComponent<FixedJoint2D);
                
                pull.GetComponent<BoxCollider2D>().isTrigger = true;
                StartCoroutine(ShrinkOverTime(0.3f, pull));

                }
            
            
            //ShrinkSmallObject();
        }

        //if (largeObjectInTrigger && Input.GetMouseButton(1))
        //{
        //ShrinkLargeObject();
        //}
        if (!items.Count.Equals(0) && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shot");
            
            ShootObject();
        }

        //if (shrinkingSmallObject)
        //{
        //    float distCovered = Time.deltaTime * speed;
        //    float fracJourney = distCovered / journeyLength;
        //    //for (int i = 0; i < PullOBJ.Count; i++)
        //    //{
        //    //    PullOBJ[i].GetComponent<BoxCollider2D>().enabled = false;

        //    //    PullOBJ[i].transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
        //    //}
        //    foreach (GameObject pull in PullOBJ.ToList())
        //    {



        //        pull.transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
        //        pull.transform.position = Vector3.MoveTowards(pull.transform.position, endMarker.transform.position, speed * Time.deltaTime / 2f);

        //        pull.GetComponent<BoxCollider2D>().isTrigger = true;

        //    }


        //    //foreach (GameObject starter in startMarker)
        //    //{
        //    //    starter.transform.position = Vector3.MoveTowards(starter.transform.position, endMarker.transform.position, speed * Time.deltaTime / 2f);

        //    //}

        //}

        //if (shrinkingLargeObject)
        //{

        //    float distCovered = Time.deltaTime * speed;
        //    float fracJourney = distCovered / journeyLength;
        //    PullOBJ[0].GetComponent<BoxCollider2D>().enabled = false;
        //    startMarker[0].transform.position = Vector3.MoveTowards(startMarker[0].transform.position, endMarker.transform.position, speed * Time.deltaTime / 2f);
        //    PullOBJ[0].transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed * 4f;
        //}
        //if (shrinkingLargeObject && PullOBJ != null && PullOBJ[0].transform.localScale.sqrMagnitude < targetScale.sqrMagnitude)
        //{

        //    smallObjectInTrigger = false;
        //    largeObjectInTrigger = false;
        //    shrinkingSmallObject = false;
        //    shrinkingLargeObject = false;
        //    startMarker.Remove(startMarker[0]);
        //    items.Add(PullOBJ[0]);
        //    PullOBJ[0].transform.parent = gun.transform;
        //    PullOBJ[0].transform.rotation = transform.parent.rotation;
        //    PullOBJ[0].SetActive(false);
        //    PullOBJ.Remove(PullOBJ[0]);

        //}
        //if (shrinkingSmallObject)
        //{
        //    foreach (GameObject pull in PullOBJ.ToList())
        //    {
        //        if (pull.transform.localScale.sqrMagnitude < targetScale.sqrMagnitude)
        //        {
        //            smallObjectInTrigger = false;
        //            largeObjectInTrigger = false;
        //            //shrinkingSmallObject = false;
        //            shrinkingLargeObject = false;
        //            //startMarker.Remove(startMarker[0]);
        //            items.Add(pull);
        //            pull.transform.parent = gun.transform;
        //            pull.transform.rotation = transform.parent.rotation;
        //            pull.SetActive(false);
        //            PullOBJ.Remove(pull);

        //        }
        //    }
        //    //smallObjectInTrigger = false;
        //    //largeObjectInTrigger = false;
        //    //shrinkingSmallObject = false;
        //    //shrinkingLargeObject = false;
        //    //startMarker.Remove(startMarker[0]);
        //    //items.Add(PullOBJ[0]);
        //    //PullOBJ[0].transform.parent = gun.transform;
        //    //PullOBJ[0].transform.rotation = transform.parent.rotation;
        //    //PullOBJ[0].SetActive(false);
        //    //PullOBJ.Remove(PullOBJ[0]);

        //}
        if (items.Count > 3)
        {
            items.Remove(items[items.Count - 1]);
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
    IEnumerator ShrinkOverTime(float time, GameObject pull)
    {
        pull.transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        pull.transform.GetComponent<Rigidbody2D>().isKinematic = true;
        float distCovered = Time.deltaTime * speed;
        float fracJourney = distCovered / journeyLength;
        shrinking = true;
        float currentTime = 0.0f;
        Debug.Log(currentTime);
        do
        {
            pull.transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
            pull.transform.position = Vector3.MoveTowards(pull.transform.position, endMarker.transform.position, speed * Time.deltaTime/ 1.5f);

            yield return null;
        } while (pull.transform.localScale.sqrMagnitude > targetScale.sqrMagnitude);
        
        shrinking = false;
        pull.GetComponent<BoxCollider2D>().isTrigger = false;
        items.Add(pull);
        pull.transform.parent = gun.transform;
        pull.transform.rotation = transform.parent.rotation;
        pull.SetActive(false);
        PullOBJ.Remove(pull);
        smallObjectInTrigger = false;
    }

}

