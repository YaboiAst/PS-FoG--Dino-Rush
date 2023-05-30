using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3Behaviour : MonoBehaviour
{
    public List<Transform> patrolPoints;
    private int currentPoint;
    private int previousPoint;
    public float idleTimer = 2f;
    public float idleTimerCounter;

    public float health = 4f;
    public float damage = 1f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    public PlayerCombat pc;

    private bool isRight = true;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = 0;
        GoToNextPoint();
    }

    private void Update() {
        if(idleTimerCounter <= 0)
            GoToNextPoint();
        else 
            idleTimerCounter -= Time.deltaTime;
    }

    private void GoToNextPoint(){
        previousPoint = currentPoint;
        if(currentPoint == patrolPoints.Count - 1)
            currentPoint = 0;
        else
            currentPoint++;
        idleTimerCounter = idleTimer;
        //transform.position = Vector2.Lerp(transform.position, patrolPoints[currentPoint].position, 1f);
        rb.velocity = new Vector2((patrolPoints[currentPoint].position.x - transform.position.x), jumpForce);
        if(rb.velocity.x < 0 && isRight){
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(rb.velocity.x > 0 && !isRight){
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void TakeDamage(float damageAmount){
        health -= damageAmount;
        if(health <= 0){
            Destroy(gameObject);
            pc.GetPoints(30);
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
