using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health;
    public float movementSpeed;
    public float dashSpeedMultiplier;
    public float turnSmoothTime;
    public float dashLength;    

    public Renderer renderer;


    private Color originalColor;
    private Animator animator;
    private Vector3 forward;
    private Vector3 right;
    private CharacterController controller;


    private float gravity = 9.8f;
    private float turnSmoothVelocity;
    private float dashTimeLeft;
    private bool isDashing = false;
    private bool isDead = false;
    private float totalHealth;
    private ShootingController shootingController;
    private SceneManagement sceneManagement;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        shootingController = GetComponent<ShootingController>();
        originalColor = renderer.material.color;
        totalHealth = health;
    }
    // Start is called before the first frame update
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMovementInput() && !isOpening() && !isDashing && !isDead)
        {
            Move();
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            StartCoroutine(StartDash());
        }
        if (!controller.isGrounded)
        {
            controller.Move(Vector3.down * gravity * Time.deltaTime);
        }
    }

    private void Move()
    {
        Vector3 rightMovement = right * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 forwardMovement = forward * Time.deltaTime * Input.GetAxis("Vertical");
        Vector3 heading = Vector3.Normalize(rightMovement + forwardMovement);
        float targetAngle = Mathf.Atan2(heading.x, heading.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        animator.SetFloat("Speed", heading.magnitude);
        controller.Move(heading * movementSpeed / 10);
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    IEnumerator StartDash()
    {
        isDashing = true;
        animator.SetTrigger("StartDash");
        shootingController.PauseShooting();
        dashTimeLeft = dashLength;
        while (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "anim_cloed_Roll_Loop")
            yield return null;
        while (dashTimeLeft >= 0)
        {
            controller.Move(transform.forward * movementSpeed * dashSpeedMultiplier * Time.deltaTime);
            dashTimeLeft -= Time.deltaTime;
            yield return null;
        }
        animator.SetTrigger("StopDash");
        shootingController.ResumeShooting();
        isDashing = false;
    }

    private bool isOpening()
    {
        return animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "anim_open" || animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "GunMount";
    }

    private bool IsMovementInput()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("health"))
        {
            health = Mathf.Clamp(health + 10, 0, totalHealth);
            Destroy(other.gameObject);
            UIManager.instance.updateHealth(health, totalHealth);
        }
    }

    IEnumerator DamagePlayerAndFlash(float damage)
    {
        health -= damage;
        UIManager.instance.updateHealth(health,totalHealth);
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        renderer.material.color = originalColor;
        if (health <= 0 && !isDead)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        isDead = true;
        shootingController.PauseShooting();
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(2);
        SceneManagement.instance.RestartGame();

    }

    public void DamagePlayer(float damage)
    {
        StartCoroutine(DamagePlayerAndFlash(damage));
    }
}
