using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleAtkSpeed : MonoBehaviour
{
    [SerializeField] AudioClip sound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null)
        {
            if (collision.gameObject.GetComponent<Player>().Bot == false)
            {
                collision.gameObject.GetComponent<Player>().AddAtkSpeed();
                AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, PlayerPrefs.GetFloat("SFXVolume", 1));
                Destroy(gameObject);
            }
        }
    }
}
