using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class gameManager : MonoBehaviour
{
    public bool isPause = false;
    [SerializeField] private GameObject menuPause;
    [SerializeField] private GameObject menuCredits;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private patrol patrol;


    private void Start()
    {
        pauseButton.Select();
    }
    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (Input.GetButtonDown("PAUSE"))
            {
                if (!isPause)
                {
                    menuPause.SetActive(true);
                    pauseButton.Select();
                    Time.timeScale = 0;
                    isPause = true;

                }
                else if (isPause)
                {
                    Time.timeScale = 1;
                    isPause = false;

                }


            }
        }
        
    }

    public void resume()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }
    public void quitOnGame()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
        SceneManager.LoadScene(0);
    }

    public void play()
    {
        patrol.onPlay();
        menuPause.SetActive(false);

    }

    public void QuitOnMenu()
    {
        Application.Quit();
    }

    public void credit()
    {
        menuPause.SetActive(false);
        menuCredits.SetActive(true);
        quitButton.Select();
    }

    public void quiOnCredits()
    {
        menuPause.SetActive(true);
        menuCredits.SetActive(false);
        pauseButton.Select();
    }
}
