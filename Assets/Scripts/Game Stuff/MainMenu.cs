using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject controls;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Settings()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Controls()
    {
        controls.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Back()
    {
        settings.SetActive(false);
        controls.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
