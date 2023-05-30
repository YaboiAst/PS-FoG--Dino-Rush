using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject winScreen, lossScreen, pauseScreen, overlay;

    public TMP_Text winScore, lossScore;
    public PlayerCombat pc;
    // Start is called before the first frame update
    public void Win(){
        // Win screen
        overlay.SetActive(true);

        pauseScreen.SetActive(false);
        lossScreen.SetActive(false);
        winScreen.SetActive(true);

        Time.timeScale = 0;
        winScore.text = string.Format("{0:0000}", pc.score);
    }

    public void Loss(){
        // Lose screen
        overlay.SetActive(true);

        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
        lossScreen.SetActive(true);

        Time.timeScale = 0;
        lossScore.text = string.Format("{0:0000}", pc.score);
    }

    private void Pause(){
        overlay.SetActive(true);

        lossScreen.SetActive(false);
        winScreen.SetActive(false);
        pauseScreen.SetActive(true);

        Time.timeScale = 0;
    }

    public void Resume(){
        pauseScreen.SetActive(false);
        overlay.SetActive(false);
        Time.timeScale = 1;
    }

    public void NextLevel(){
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Menu();
    }

    public void Restart(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Botões
    public void Menu(){
        SceneManager.LoadScene("Menu");
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("Esc");
            Pause();
        }
    }
}
