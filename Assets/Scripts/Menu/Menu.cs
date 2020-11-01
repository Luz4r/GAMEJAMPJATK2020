using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("demoLevel");
    }
    public void Exit()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }
}
