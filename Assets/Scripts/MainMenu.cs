using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    // wariables
    [SerializeField]
    private Canvas options;

    // loads the next scene in order "shoudbe the game scene"
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    // closes the game 
    // in editor wont work 
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void Options()
    {
        // enables Options menu
        options.enabled = true;
    }
    public void Back()
    {
        // disabless menu
        options.enabled = false;
    }
}
