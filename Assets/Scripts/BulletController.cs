using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float timeToDie = 2f;
    public float damage = 10f;
    public GameObject hit;
    public GameObject body;
    public GameObject decal;
    public AudioClip hitConcrete;
    public AudioClip hitFlesh;
    public AudioSource hitAudio;
    private Rigidbody rb;
    private SphereCollider collider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToDie <= 0f)
        {
            Destroy(gameObject);
        }
        timeToDie -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        Quaternion rot2 = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        GameObject obj = Instantiate(hit, contact.point, rot);
        obj.transform.parent = gameObject.transform;
        if (collision.gameObject.CompareTag("enemy"))
        {
            hitAudio.Stop();
            hitAudio.clip = hitConcrete;
            hitAudio.Play();
        }
        if (collision.gameObject.CompareTag("obstacle"))
        {
            GameObject obj2 = Instantiate(decal, contact.point, rot2);
            obj2.transform.parent = gameObject.transform;
        }
        Destroy(collider);
        Destroy(rb);
        Destroy(body);
        Destroy(gameObject, 2); //destroy whole bullet object

    }
}
