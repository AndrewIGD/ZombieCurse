using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void OpenMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        foreach(Player player in FindObjectsOfType<Player>())
        {
            player.enabled = !pauseMenu.activeInHierarchy;
        }
        Time.timeScale = pauseMenu.activeInHierarchy ? 0.000001f : 1f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
    }
}
