using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float fallSpeed;
    private bool exploded;
    public GameObject body;
    private Rigidbody rb;
    private SphereCollider blastTrigger;
    private CapsuleCollider rocketCollider;
    public ParticleSystem explosionEffect;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.down * fallSpeed, ForceMode.Impulse);
        blastTrigger = GetComponent<SphereCollider>();
        rocketCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (exploded)
        {
            Destroy(body);
            Destroy(rb);
            Destroy(rocketCollider);
            //destroying blastTrigger with 0.1f delay, to account for physics update for Trigger Stay
            Destroy(blastTrigger, 0.1f);
            Destroy(gameObject, 2);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        explosionEffect.Play();
        blastTrigger.enabled = true;
        exploded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy") && exploded)
        {
            other.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            //adding knockback to enemies
            //other.attachedRigidbody.AddForce(-other.transform.forward * 100, ForceMode.Impulse);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
