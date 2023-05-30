using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float damage = 2f;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Obstacle") Destroy(gameObject);
        if(other.gameObject.tag == "Enemie1"){
            T1Behaviour enemie1 = other.gameObject.GetComponent<T1Behaviour>();
            enemie1.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Enemie2"){
            T2Behaviour enemie2 = other.gameObject.GetComponent<T2Behaviour>();
            enemie2.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Enemie3"){
            T3Behaviour enemie3 = other.gameObject.GetComponent<T3Behaviour>();
            enemie3.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
