using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextTransition : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Text text;
    [SerializeField] string[] storyText;
    [SerializeField] bool quitOnEnd;

    int textIndex = 0;

    public void LoadText()
    {
        if(textIndex >= storyText.Length)
        {
            if(quitOnEnd)
            {
                PlayerPrefs.DeleteKey("CurrentScene");
                Destroy(animator);
                Application.Quit();
            }
            else SceneManager.LoadScene("Level 1");
        }
        else text.text = storyText[textIndex++];
    }

    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("idle") && Input.GetKeyDown(KeyCode.Return))
        {
            animator.Play("transition");
        }
    }
}
