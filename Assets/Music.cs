using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectsOfType<Music>().Length >= 2)
        {
            bool ok = true;
            foreach (Music music in FindObjectsOfType<Music>())
            {
                if (music.GetComponent<AudioSource>().clip == GetComponent<AudioSource>().clip && music.gameObject != gameObject)
                {
                    ok = false;
                    Destroy(gameObject);
                }
            }
            if(ok)
            {
                foreach (Music music in FindObjectsOfType<Music>())
                {
                    if(music.gameObject != gameObject)
                    Destroy(music.gameObject);
                }
                DontDestroyOnLoad(gameObject);
            }
        }
        else DontDestroyOnLoad(gameObject);

        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume", 1);
    }
}
