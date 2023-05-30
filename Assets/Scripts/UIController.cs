using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public PlayerCombat player;

    public TMP_Text health;
    public TMP_Text ammo;
    public TMP_Text score;

    public void UpdateUI(){
        score.text = "Score: " + string.Format("{0:0000}", player.score); 
        health.text = string.Format("{0:00}/{1:00}", player.currentHealth, player.maxHealth);
        ammo.text = string.Format("{0:00}", player.ammoAmmount);
    }
}
