using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversePad : MonoBehaviour
{
    [SerializeField] AudioClip jumpSfx;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null || collision.gameObject.GetComponent<Box>() != null)
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>().gravityScale == 1)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = -1;
                
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
               
            }
            AudioSource.PlayClipAtPoint(jumpSfx, Camera.main.transform.position, PlayerPrefs.GetFloat("SFXVolume", 1));
        }
    }
}
