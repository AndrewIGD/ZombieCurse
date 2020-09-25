using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : MonoBehaviour
{
    [Header("Player Properties")]

    [SerializeField] float health;
    [SerializeField] protected float speed;
    [SerializeField] float arrowSpeed;
    [SerializeField] float animationSpeed;
    [SerializeField] int team;
    [SerializeField] bool bot = true;
    [SerializeField] bool canInterrupt = true;
    [SerializeField] bool hasBow = false;

    [Space(2)]
    [Header("Player Objects")]
    [SerializeField] GameObject arrowToInstantiate;
    [SerializeField] GameObject animationArrow;
    [SerializeField] GameObject[] animationTripleArrows;
    [SerializeField] SpriteRenderer[] limbs;
    [SerializeField] Hitbox[] hitboxes;
    [SerializeField] Text healthText;
    [SerializeField] Image ability1;
    [SerializeField] Image ability2;
    [SerializeField] GameObject healthBar;
    [SerializeField] AudioClip attack;
    [SerializeField] AudioClip damage;
    [SerializeField] AudioClip arrow;
    [SerializeField] AudioClip dash;
    [SerializeField] AudioClip dirt;
    [SerializeField] AudioClip powerfulHit;
    [SerializeField] AudioClip expl;
    [SerializeField] AudioClip switchWeapon;
    [SerializeField] Image swordImage;
    [SerializeField] Image bowImage;
    [SerializeField] RuntimeAnimatorController swordAnimations;
    [SerializeField] RuntimeAnimatorController bowAnimations;
    [SerializeField] GameObject sword;
    [SerializeField] GameObject bow;
    [SerializeField] GameObject weaponList;


    Face m_face = Face.Right;

    //Cached components
    Rigidbody2D m_rb;
    BoxCollider2D m_box;
    protected Animator animator;

    bool m_abil1 = true;
    bool m_abil2 = true;

    float maxHealth;

    public int Team
    {
        get
        {
            return team;
        }
    }

    public bool Bot
    {
        get
        {
            return bot;
        }
    }

    public Face FaceOfPlayer
    {
        get
        {
            return m_face;
        }
    }

    protected bool CanMove
    { 
        get
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName("idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("run");
        }
    }


    protected virtual void Start()
    {
        maxHealth = health;

        m_rb = GetComponent<Rigidbody2D>();
        m_box = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        animator.speed = animationSpeed;

        if (hasBow)
        {
            swordImage.color = new Color32(255, 255, 255, 255);
            bowImage.color = new Color32(255, 255, 255, 128);
            weaponList.SetActive(true);
        }
    }

    private void Update()
    {
        if (m_rb.gravityScale > 0)
        {
            transform.eulerAngles = new Vector3(0, m_face == Face.Right ? 0 : 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(-180, m_face == Face.Right ? 0 : 180, 0);
        }
    }

    protected virtual void LateUpdate()
    {

        if (CanMove)
        {
            if (hasBow)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    if (animator.runtimeAnimatorController != swordAnimations)
                    {
                        animator.runtimeAnimatorController = swordAnimations;
                        swordImage.color = new Color32(255, 255, 255, 255);
                        bowImage.color = new Color32(255, 255, 255, 128);
                        sword.SetActive(true);
                        bow.SetActive(false);
                        AudioSource.PlayClipAtPoint(switchWeapon, Camera.main.transform.position, PlayerPrefs.GetFloat("SFXVolume", 1));
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    if (animator.runtimeAnimatorController != bowAnimations)
                    {
                        animator.runtimeAnimatorController = bowAnimations;
                        swordImage.color = new Color32(255, 255, 255, 128);
                        bowImage.color = new Color32(255, 255, 255, 255);
                        sword.SetActive(false);
                        bow.SetActive(true);
                        AudioSource.PlayClipAtPoint(switchWeapon, Camera.main.transform.position, PlayerPrefs.GetFloat("SFXVolume", 1));
                    }
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                MoveRight();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                MoveLeft();
            }
            else Idle();

            if (Input.GetKey(KeyCode.H))
            {
                animator.Play("basic1");
            }
            else if (Input.GetKey(KeyCode.J) && m_abil1)
            {
                animator.Play("attack1");
                m_abil1 = false;
                ability1.color = new Color32(128, 128, 128, 255);
                Invoke("CanUseFirstAbility", 5f);
            }
            else if (Input.GetKey(KeyCode.K) && m_abil2)
            {
                animator.Play("attack2");
                m_abil2 = false;
                ability2.color = new Color32(128, 128, 128, 255);
                Invoke("CanUseSecondAbility", 5f);
            }
        }
    }

    public void ActivateBow()
    {
        hasBow = true;
        swordImage.color = new Color32(255, 255, 255, 255);
        bowImage.color = new Color32(255, 255, 255, 128);
        weaponList.SetActive(true);
    }

    public void AddAtkSpeed()
    {
        animationSpeed = 1.5f;
        animator.speed = 1.5f;
    }
    public void PlayAttack()
    {
        AudioSource.PlayClipAtPoint(attack, Camera.main.transform.position, PlayerPrefs.GetFloat("SFXVolume", 1));
    }
    public void PlayArrow()
    {
        AudioSource.PlayClipAtPoint(arrow, Camera.main.transform.position, PlayerPrefs.GetFloat("SFXVolume", 1));
    }
    public void PlayDash()
    {
        AudioSource.PlayClipAtPoint(dash, Camera.main.transform.position, PlayerPrefs.GetFloat("SFXVolume", 1));
    }
    public void PlayDirt()
    {
        AudioSource.PlayClipAtPoint(dirt, Camera.main.transform.position, PlayerPrefs.GetFloat("SFXVolume", 1));
    }
    public void PlayPowerfulHit()
    {
        AudioSource.PlayClipAtPoint(powerfulHit, Camera.main.transform.position, PlayerPrefs.GetFloat("SFXVolume", 1));
    }
    public void PlayExplosion()
    {
        AudioSource.PlayClipAtPoint(expl, Camera.main.transform.position, PlayerPrefs.GetFloat("SFXVolume", 1));
    }

    void CanUseFirstAbility()
    {
        m_abil1 = true;
        ability1.color = new Color32(255, 255, 255, 255);
    }

    void CanUseSecondAbility()
    {
        m_abil2 = true;
        ability2.color = new Color32(255, 255, 255, 255);
    }

    public void Jump(float height)
    {
        m_rb.velocity = new Vector2(m_rb.velocity.x, height);
    }

    public void Damage(float damage, Vector2 direction)
    {
        health -= damage;
        AudioSource.PlayClipAtPoint(this.damage, Camera.main.transform.position, PlayerPrefs.GetFloat("SFXVolume", 1));
        if (canInterrupt)
        m_rb.velocity = direction;
        if (health <= 0)
        {
            health = 0;
            animator.Play("death");
            m_box.enabled = false;
            if(bot == false)
            {
                FindObjectOfType<UIAnimation>().RestartScene();
            }
        }
        else if(canInterrupt)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("hurt1"))
            {
                animator.Play("hurt2");
            }
            else animator.Play("hurt1");
        }

        if (bot == false)
        {
            healthText.text = health.ToString();
        }
        else healthBar.transform.localScale = new Vector3(health / maxHealth, 1);

        foreach(SpriteRenderer limb in limbs)
        {
            limb.color = new Color32(255, 98, 98, 255);
        }

        CancelInvoke("StopDamage");
        Invoke("StopDamage", 0.25f);
    }

    public void EliminatePlayer()
    {
        Destroy(gameObject);
    }

    void StopDamage()
    {
        foreach (SpriteRenderer limb in limbs)
        {
            limb.color = new Color32(255, 255, 255, 255);
        }
    }

    public void ResetHitboxes()
    {
        foreach (Hitbox hitbox in hitboxes)
            hitbox.ResetList();
    }

    public void ShootArrow()
    {
        GameObject arr = Instantiate(arrowToInstantiate);
        arr.transform.position = animationArrow.transform.position;

        arr.SetActive(true);

        arr.transform.eulerAngles = animationArrow.transform.eulerAngles;
        arr.GetComponent<Rigidbody2D>().velocity = new Vector2((m_face == Face.Right ? arrowSpeed : -arrowSpeed), 0);

        arr.GetComponent<Hitbox>().team = team;

    }

    public void ShootTripleArrows()
    {
        foreach(GameObject arrow in animationTripleArrows)
        {

                GameObject arr = Instantiate(arrowToInstantiate);
                arr.transform.position = arrow.transform.position;

                arr.SetActive(true);

                arr.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, arrow.transform.eulerAngles.z - 45f);
                arr.GetComponent<Rigidbody2D>().velocity = arr.transform.up * arrowSpeed;
                arr.transform.eulerAngles = arrow.transform.eulerAngles;
                arr.GetComponent<Rigidbody2D>().gravityScale = 4;

                arr.GetComponent<Hitbox>().team = team;
        }
    }

    public void Dash()
    {
        m_rb.velocity = new Vector2((m_face == Face.Right ? speed : -speed) * 2, 0);
    }

    public void DashBack()
    {
        m_rb.velocity = new Vector2((m_face == Face.Right ? -speed : speed) * 1.5f, 0);
    }

    public void MoveForward()
    {
        m_rb.velocity = new Vector2(m_face == Face.Right ? speed : -speed, m_rb.velocity.y);
    }

    public void StopMoving()
    {
        m_rb.velocity = new Vector2(0, m_rb.velocity.y);
    }

    public void ContinueBasicAttacks()
    {
        ResetHitboxes();

        StopMoving();

        if (bot == false)
        {
            if (Input.GetKey(KeyCode.D))
            {
                m_face = Face.Right;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                m_face = Face.Left;
            }
            RotatePlayer();

            if (Input.GetKey(KeyCode.H))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("basic1"))
                    animator.Play("basic2");
                else animator.Play("basic1");
            }
            else if (Input.GetKey(KeyCode.J) && m_abil1)
            {
                animator.Play("attack1");
                m_abil1 = false;
                ability1.color = new Color32(128, 128, 128, 255);
                Invoke("CanUseFirstAbility", 5f);
            }
            else if (Input.GetKey(KeyCode.K) && m_abil2)
            {
                animator.Play("attack2");
                m_abil2 = false;
                ability2.color = new Color32(128, 128, 128, 255);
                Invoke("CanUseSecondAbility", 5f);
            }
        }
    }

    protected void Idle()
    {
        animator.Play("idle");
        m_rb.velocity = new Vector2(0, m_rb.velocity.y);
    }

    protected void MoveRight()
    {
        animator.Play("run");
        m_rb.velocity = new Vector2(speed, m_rb.velocity.y);
        m_face = Face.Right;
        RotatePlayer();
    }

    protected void MoveLeft()
    {
        animator.Play("run");
        m_rb.velocity = new Vector2(-speed, m_rb.velocity.y);
        m_face = Face.Left;
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        transform.eulerAngles = new Vector3(m_rb.gravityScale > 0 ? 0 : -180, m_face == Face.Right ? 0 : 180, 0);
    }
}
