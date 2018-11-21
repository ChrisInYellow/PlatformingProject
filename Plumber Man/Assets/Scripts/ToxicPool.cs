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
            //playerHealth.health -= damage;
            StartCoroutine(DamageOverTime());
            //knockbackDir = new Vector2(collision.gameObject.transform.position.x - transform.position.x,
            //    collision.transform.position.y - transform.position.y) * recoil;

            //print(knockbackDir);

            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDir, ForceMode2D.Impulse);
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
