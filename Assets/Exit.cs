using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null)
        {
            if(collision.gameObject.GetComponent<Player>().Bot == false)
            {
                FindObjectOfType<UIAnimation>().NextScene();
            }
        }
    }
}
