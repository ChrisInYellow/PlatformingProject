using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicPool : MonoBehaviour
{

    public int damage;
    public float recoil;
    private HealthManager playerHealth;
    private Vector2 knockbackDir;

    private void Awake()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<HealthManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(DamageOverTime());
        }
    }

    IEnumerator DamageOverTime()
    {
        playerHealth.health -= damage;
        yield return new WaitForSeconds(1.5f);
        playerHealth.health -= damage;
        yield return new WaitForSeconds(1.5f);
        playerHealth.health -= damage;
        yield return new WaitForSeconds(1.5f);
        playerHealth.health -= damage;
        yield return null;




    }
}
