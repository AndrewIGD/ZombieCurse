using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null && collision.transform.name.Contains("Wizard") == false)
        {
            collision.gameObject.GetComponent<Player>().Damage(99999, Vector2.zero);
            Destroy(collision.attachedRigidbody);
        }
    }
}
