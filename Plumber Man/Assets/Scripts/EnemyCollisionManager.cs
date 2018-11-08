using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionManager : MonoBehaviour {

    public int enemyHealth;
    public GameObject[] EnemyHealthLevels;
    public GameObject bloodSplatter;

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
            GameObject splatter = Instantiate(bloodSplatter, transform.position, Quaternion.identity);
            
            if(!splatter.GetComponent<ParticleSystem>().isPlaying)
            {
                StartCoroutine(PlayParticles(splatter.GetComponent<ParticleSystem>()));
                splatter.SetActive(false); 
            }
            gameObject.SetActive(false); 
        }
    }

    public IEnumerator PlayParticles(ParticleSystem blood)
    {
        Debug.Log("Playing...");
        blood.Play();
        yield return blood.main.duration;

        //yield return null; 
    }
}
