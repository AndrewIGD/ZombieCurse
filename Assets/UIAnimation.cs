using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] string nextScene;

    private void Start()
    {
        PlayerPrefs.SetString("CurrentScene", SceneManager.GetActiveScene().name);
    }

    public void RestartScene()
    {
        GetComponent<Animator>().Play("transitionLeave");
        Invoke("Restart", 0.5f / GetComponent<Animator>().speed);
    }

    public void NextScene()
    {
        GetComponent<Animator>().Play("transitionLeave");
        Invoke("LoadScene", 0.5f / GetComponent<Animator>().speed);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
