using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    Rigidbody rb;
    public float fallSpeed;
    private bool exploded;
    public GameObject body;
    private SphereCollider blastTrigger;
    private CapsuleCollider rocketCollider;
    public ParticleSystem explosionEffect;
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
            Destroy(blastTrigger, 0.1f);
            Destroy(gameObject, 2);
            //play particle effects and kill object when they end
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        explosionEffect.Play();
        exploded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("enemy") && exploded)
        {
            other.gameObject.GetComponent<EnemyController>().TakeDamage(50);
            //other.attachedRigidbody.AddForce(-other.transform.forward * 100, ForceMode.Impulse);
        }
    }
}
