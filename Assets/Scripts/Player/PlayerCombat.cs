using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header ("Score")]
    public float score = 0f;

    [Header ("Health")]
    public float maxHealth = 10f;
    public float currentHealth;

    [Header ("Shooter Control")]
    public float projSpeed = 10f;
    public int ammoAmmount = 0;

    [Header ("Projectile")]
    public GameObject projPrefab;
    public Transform shootingOrb;

    [Header ("HUD")]
    public UIController hud;

    private void Start() {
        currentHealth = maxHealth;
    }

    private void Update() {
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }    
    }

    public void GetAmmo(){
        if(!shootingOrb.gameObject.activeSelf) shootingOrb.gameObject.SetActive(true);
        ammoAmmount++;
        GetPoints(10);
        hud.UpdateUI();
    }

    private void Shoot(){
        if(ammoAmmount > 0){
            GameObject proj = Instantiate(projPrefab, shootingOrb.position, shootingOrb.rotation);
            Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
            projRb.rotation -= 90f;
            projRb.AddForce(Vector2.right * GetComponent<PlayerController>().isFacing * projSpeed, ForceMode2D.Impulse);
            ammoAmmount--;
            GetPoints(-5);
            hud.UpdateUI();
            if(ammoAmmount == 0)
                shootingOrb.gameObject.SetActive(false);
        }
    }

    public void GetPoints(float pointAmount){
        score += pointAmount;
    }

    public void TakeDamage(float damageAmount){
        currentHealth -= damageAmount;
        if(currentHealth <= 0){
            // Reset
            currentHealth = maxHealth;
        }
        hud.UpdateUI();
    }
}
