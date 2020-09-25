using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float height;
    [SerializeField] AudioClip jumpSfx;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null)
        {
            collision.gameObject.GetComponent<Player>().Jump(height);
            AudioSource.PlayClipAtPoint(jumpSfx, Camera.main.transform.position, PlayerPrefs.GetFloat("SFXVolume", 1));
        }
        else if(collision.gameObject.GetComponent<Box>() != null)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, height);
            AudioSource.PlayClipAtPoint(jumpSfx, Camera.main.transform.position, PlayerPrefs.GetFloat("SFXVolume", 1));
        }
    }
}
