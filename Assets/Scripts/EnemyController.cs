using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//https://www.youtube.com/watch?v=UjkSFoLxesw

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private PlayerController playerController;
    private Animator animator;

    public Color damageColor;
    public float damageDealt;
    public Renderer ColorRenderer;
    public float speed;
    public float health;

    
    public float attackDelay = 1f;
    private float timer;
    private bool dead = false;
    private bool shouldFollow = true;
    private bool shouldAttack = false;
    private Color originalColor;


    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        playerController = player.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        originalColor = ColorRenderer.material.color;
        
    }
    void Start()
    {
        timer = attackDelay;

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManagement.isGamePaused) return;
        if (!dead && shouldFollow)
        {
            transform.LookAt(player);
            //Vector3 direction = (player.transform.position - transform.position).normalized;            
            transform.Translate(Vector3.forward * speed / 10);
            //controller.Move(direction * speed / 10);
            //if (!controller.isGrounded)
            //{
            //    controller.Move(Vector3.down * gravity * Time.deltaTime);
            //}
        }
        if (health <= 0 && !dead)
        {
            dead = true;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject,2);
        yield return null;

    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(TakeDamageCore(damage));
    }

    IEnumerator TakeDamageCore(float damage)
    {
        if (!dead)
        {
            //animator.SetTrigger("Hit");
            health -= damage;
            ColorRenderer.material.color = damageColor;
            yield return new WaitForSeconds(0.1f);
            ColorRenderer.material.color = originalColor;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            TakeDamage(collision.gameObject.GetComponent<BulletController>().damage);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shouldAttack = true;
            shouldFollow = false;
            animator.SetBool("Attack", true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player") && !dead)
        {
            if (timer <= 0)
            {
                playerController.DamagePlayer(damageDealt);
                timer = attackDelay;
            }
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shouldFollow = true;
            shouldAttack = false;
            animator.SetBool("Attack", false);
        }
    }
}
