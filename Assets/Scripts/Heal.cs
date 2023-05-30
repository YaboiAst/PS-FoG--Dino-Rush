using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    float healAmount = 3f;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            PlayerCombat pc = other.gameObject.GetComponent<PlayerCombat>();
            if(pc.currentHealth < pc.maxHealth){
                Destroy(gameObject);
                pc.currentHealth += healAmount;
                if(pc.currentHealth > pc.maxHealth) pc.currentHealth = pc.maxHealth;
                pc.hud.UpdateUI();
            }
        }
    }
}
