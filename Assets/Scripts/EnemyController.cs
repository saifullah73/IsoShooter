using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//https://www.youtube.com/watch?v=UjkSFoLxesw

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private PlayerController playerController;
    //private CharacterController controller;
    private Animator animator;
    public Color damageColor;
    public float damageDealt;
    public Renderer ColorRenderer;
    private bool dead = false;
    public float speed;
    private float gravity = 9.8f;
    public float attackDelay = 1f;
    private float timer;
    public float health = 5f;
    private bool shouldFollow = true;
    private bool shouldAttack = false;

    private Color originalColor;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        playerController = player.GetComponent<PlayerController>();
        //controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        originalColor = ColorRenderer.material.color;
        //originalColor = gameObject.GetComponent<Renderer>().material.color;
        
    }
    void Start()
    {
        timer = attackDelay;

    }

    // Update is called once per frame
    void Update()
    {
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
            Debug.Log("dying");
            dead = true;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

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
        
        if (other.gameObject.CompareTag("Player"))
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

    //public void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.gameObject.CompareTag("Player"))
    //    {
    //        if (timer < 0)
    //        {
    //            animator.SetTrigger("Attack");
    //            playerController.DamagePlayer(1);
    //            timer = attackDelay;
    //        }
    //        else
    //        {
    //            timer -= Time.deltaTime;
    //        }
    //    }
    //}


}
