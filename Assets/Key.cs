using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject[] locks;
    [SerializeField] AudioClip sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null)
        {
            if(collision.gameObject.GetComponent<Player>().Bot == false)
            {
                foreach(GameObject lockDoor in locks)
                {
                    lockDoor.SetActive(false);
                }
                AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, PlayerPrefs.GetFloat("SFXVolume", 1));
                gameObject.SetActive(false);

            }
        }
    }
}
