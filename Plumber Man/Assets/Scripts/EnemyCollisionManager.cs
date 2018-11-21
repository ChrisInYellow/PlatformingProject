using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionManager : MonoBehaviour {

    public int enemyHealth;
    public float enemyKnockback;
    public Vector3 targetScale; 
    public GameObject bloodSplatter;

    private bool dead;
    private Animator anim;
    private Vector2 knockbackDir;

    private void Awake()
    {
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<EnemyShot>() != null || (collision.gameObject.GetComponent<ItemShot>() != null))
        {
            if(collision.gameObject.GetComponent<EnemyShot>() != null)
            {
                collision.gameObject.GetComponent<EnemyCollisionManager>().enabled = false; 
            }
            
            enemyHealth -= 1;
            anim.SetBool("Hit", true); 
            EnemyHealthCheck();
        }
    }

    public void EnemyHealthCheck()
    {
        GameObject splatter = Instantiate(bloodSplatter, transform.position, Quaternion.identity);
        ParticleSystem particleSystem = splatter.GetComponent<ParticleSystem>();
        if (enemyHealth<=0)
        {
            this.dead = true; 

            StartCoroutine(HandleDamage(splatter, 0.55f));
        }
        else
        {
            StartCoroutine(HandleDamage(splatter, 0.55f));
        }
    }

    public IEnumerator HandleDamage(GameObject splatter, float time)
    {

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Hit", false);
        splatter.SetActive(false);

        yield return new WaitForEndOfFrame();
        if(this.dead == true)
        {
            gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            Vector3 newScale = transform.localScale * 0.75f;
            gameObject.transform.localScale = newScale;
            yield return new WaitForSeconds(0.4f);
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            yield return null; 
        }
    }

}
