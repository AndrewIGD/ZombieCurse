using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int team;
    [SerializeField] float damage;
    [SerializeField] bool destroyOnHit = false;
    [SerializeField] bool directionOfPlayer = false;
    [SerializeField] Player player;
    [SerializeField] float knockBack;

    bool damageMob = true;

    List<GameObject> players = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null)
        {
            if(collision.gameObject.GetComponent<Player>().Team != team && damageMob && players.Contains(collision.gameObject) == false)
            {

                collision.gameObject.GetComponent<Player>().Damage(damage, new Vector2((directionOfPlayer && player != null) ? (player.FaceOfPlayer == Player.Face.Right ? knockBack : -knockBack) : ((GetComponent<Rigidbody2D>() != null) ? GetComponent<Rigidbody2D>().velocity.x >= 0 ? knockBack : -knockBack : 0), 0));
                players.Add(collision.gameObject);
                if(destroyOnHit)
                Destroy(gameObject);
            }
        }
        else if(collision.gameObject.GetComponent<Hitbox>() == null && destroyOnHit)
        {
            damageMob = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<BoxCollider2D>().size = new Vector2(0.01f, 0.01f);
            GetComponent<BoxCollider2D>().isTrigger = false;
            Destroy(gameObject, 2f);
        }
    }

    public void ResetList()
    {
        players = new List<GameObject>();
    }
}
