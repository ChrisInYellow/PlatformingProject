using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionManager : MonoBehaviour {

    public int enemyHealth;
    public GameObject[] EnemyHealthLevels;
    public GameObject bloodSplatter;
    private bool dead; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<EnemyShot>() != null)
        {
            enemyHealth -= 1;
            EnemyHealthCheck();
        }
        
    }

    private void EnemyHealthCheck()
    { 
        if (enemyHealth<=0)
        {
            this.dead = true; 
            GameObject splatter = Instantiate(bloodSplatter, transform.position, Quaternion.identity);
            ParticleSystem particleSystem = splatter.GetComponent<ParticleSystem>();

            StartCoroutine(HandleDamage(splatter, 0.55f));

        }
    }

    public IEnumerator HandleDamage(GameObject splatter, float time)
    {
        yield return new WaitForSeconds(0.65f);
        Debug.Log("Coroutine activated");
        splatter.SetActive(false);
        yield return new WaitForEndOfFrame();
        if(this.dead == true)
            {
                gameObject.SetActive(false);
            }
    }
}
