using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControllerEnemie : MonoBehaviour
{
    public float damage = 2f;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Obstacle") Destroy(gameObject);
        if(other.gameObject.tag == "Player"){
            PlayerCombat player = other.gameObject.GetComponent<PlayerCombat>();
            player.TakeDamage(damage);
        }
    }
}
