using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            PlayerCombat pc = other.gameObject.GetComponent<PlayerCombat>();
            pc.GetAmmo();
            Destroy(gameObject);
        }
    }
}
