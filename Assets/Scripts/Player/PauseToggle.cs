using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseToggle : MonoBehaviour
{
    public GameObject pause;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause.activeSelf) pause.SetActive(false);
            else pause.SetActive(true);
        }

        if (pause.activeSelf) Time.timeScale = 0f;
        else Time.timeScale = 1f;

    }
}
