using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionManager : MonoBehaviour {

    public int enemyHealth;
    public Vector3 targetScale; 
    public GameObject bloodSplatter;

    private bool dead;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<EnemyShot>() != null)
        {
            enemyHealth -= 1;
            anim.SetBool("Hit", true); 
            EnemyHealthCheck();
        }
    }

    private void EnemyHealthCheck()
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
       /* yield return new WaitForSeconds(0.095f);
        anim.SetBool("Hit", false);*/
        
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Hit", false);
        Debug.Log("Coroutine activated");
        splatter.SetActive(false);

        yield return new WaitForEndOfFrame();
        if(this.dead == true)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Vector3 newScale = transform.localScale * 0.75f;
            gameObject.transform.localScale = newScale;
            yield return null; 
        }
    }

}
