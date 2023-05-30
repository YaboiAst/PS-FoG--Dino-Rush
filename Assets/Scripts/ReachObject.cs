using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachObject : MonoBehaviour
{   
    public GameManager gm;
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            gm.Win();
        }
    }
}
