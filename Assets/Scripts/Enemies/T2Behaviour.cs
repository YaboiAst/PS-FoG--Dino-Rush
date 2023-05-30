using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2Behaviour : MonoBehaviour
{
    public GameObject projPrefab;
    public Transform shootingPoint;
    public float projSpeed = 20f;
    public float directionFaced = 1f;

    public float idleTimer = 2f;
    public float idleTimerCounter;

    public float health = 2f;
    public float damage = 1f;

    private Rigidbody2D rb;
    public PlayerCombat pc;


    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if(idleTimerCounter <= 0)
            Shoot();
        else 
            idleTimerCounter -= Time.deltaTime;
    }

    private void Shoot(){
        GameObject proj = Instantiate(projPrefab, shootingPoint.position, shootingPoint.rotation);
        proj.GetComponent<Rigidbody2D>().AddForce(Vector2.right * directionFaced * projSpeed, ForceMode2D.Impulse);
        Destroy(proj, 5f);
        idleTimerCounter = idleTimer;
    }

    public void TakeDamage(float damageAmount){
        health -= damageAmount;
        if(health <= 0){
            Destroy(gameObject);
            pc.GetPoints(25);
            pc.hud.UpdateUI();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            pc = other.gameObject.GetComponent<PlayerCombat>();
            pc.TakeDamage(damage);
        }
    }
}
