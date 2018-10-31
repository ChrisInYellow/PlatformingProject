using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class GravityGun : MonoBehaviour
{
    public ParticleSystem system;
    public ParticleSystem system1;
    public ParticleSystem system2;
    public ParticleSystem system3;
    public ParticleSystem system4;
    public ParticleSystem system5;
    public float shrinkSpeed = 0.1f;
    
    public SpriteRenderer sprite;
   
    
    public Vector3 targetScale = new Vector3(1.2f, 1.2f, 1.2f);
    public bool smallObjectInTrigger;
    
    
    public List<GameObject> PullOBJ = new List<GameObject>();
    
    public List<GameObject> FirstStartMarker = new List<GameObject>();
    public GameObject endMarker;
    public float speed = 1.0F; 
     
    public float journeyLength;
    public GameObject gun;
    public List<GameObject> items = new List<GameObject>();
    public GameObject objectShot;
    
    public bool shrinking;
    JumpPad jumpPad;

    void Start()
    {
        shrinking = false;
        
        smallObjectInTrigger = false;
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == ("SmallObject") || coll.gameObject.tag == ("JumpPad"))
        {
            if (!PullOBJ.Contains(coll.gameObject))
            {
                
                
                PullOBJ.Add(coll.gameObject);

            }
            
            

        }
        
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == ("SmallObject") || coll.gameObject.tag == ("JumpPad"))
        {
            Debug.Log("Exit");
            PullOBJ.Remove(coll.gameObject);
            

        }
        
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
            

            items.Remove(items[items.Count - 1]);
            Debug.Log("Removed");
        
        
        
    }

    

    void Update()
    {

        if (items.Count < 3 && !shrinking && Input.GetMouseButtonDown(1))
        {
            
                Debug.Log("Copied");
            
            system.Play();
            system1.Play();
            system2.Play();
            system3.Play();
            system4.Play();
            system5.Play();
            foreach (GameObject pull in PullOBJ)
                {


                
                
                pull.GetComponent<BoxCollider2D>().isTrigger = true;
                StartCoroutine(ShrinkOverTime(0.3f, pull));

                }
            
            
            
        }

        
        if (!items.Count.Equals(0) && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shot");
            
            ShootObject();
        }

        
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

