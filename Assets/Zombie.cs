using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Player
{
    [Space(2)]
    [Header("Zombie Attributes")]

    [SerializeField] float distanceFromPlayer;
    [SerializeField] bool kamikaze = false;
    [SerializeField] GameObject explosion;
    [SerializeField] bool archer = false;
    [SerializeField] bool wizard = false;
    [SerializeField] GameObject[] mobs;

    Player target;

    void SpawnMobs()
    {
        for(int i=0;i<8;i++)
        {
            GameObject mob = Instantiate(mobs[Random.Range(0, 999999) % mobs.Length]);
            mob.transform.position = new Vector2(Random.Range(-15f, 9f), -1f);

            mob.SetActive(true);

            mob.GetComponent<Zombie>().active = true;
        }

        Invoke("SpawnMobs", 15f);
    }

    private void OnDestroy()
    {
        if(wizard)
        {
            FindObjectOfType<UIAnimation>().GetComponent<Animator>().speed = 0.1f;
            FindObjectOfType<UIAnimation>().NextScene();
        }
    }

    protected override void Start()
    {
        base.Start();

        if (wizard && called == false)
        {
            active = true;
            called = true;
            Invoke("SpawnMobs", 5f);
        }

        foreach (Player player in FindObjectsOfType<Player>())
        {
            if (player.GetComponent<Zombie>() == null)
            {
                target = player;
                break;
            }
        }
    }

    public bool active = false;
    bool called = false;

    private void OnWillRenderObject()
    {

        active = true;
    }

    protected override void LateUpdate()
    {
        if (CanMove && active)
        {
            if (target != null)
            {
                if (target.transform.position.x < transform.position.x)
                    MoveLeft();
                else MoveRight();

                if (kamikaze)
                {
                    if (Vector2.Distance(target.transform.position, transform.position) < distanceFromPlayer)
                    {
                        GameObject expl = Instantiate(explosion);
                        expl.SetActive(true);
                        expl.transform.position = transform.position;
                        PlayExplosion();
                        Destroy(gameObject);
                    }
                }
                else if(archer)
                {
                    if (Vector2.Distance(target.transform.position, transform.position) < distanceFromPlayer)
                        animator.Play("basic1");
                }
                else
                {
                    if (Vector2.Distance(target.transform.position, transform.position) < distanceFromPlayer)
                        animator.Play("attack");
                }

            }
            else Idle();
        }
    }
}
