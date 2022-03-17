using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float timeToDie = 2f;
    public float damage = 10f;
    public GameObject hit;
    public GameObject decal;
    // Start is called before the first frame update
    void Start()
    {   
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
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward,contact.normal);
        Quaternion rot2 = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        GameObject obj = Instantiate(hit, contact.point,rot);
        if (!collision.gameObject.CompareTag("enemy"))
        {
            Instantiate(decal, contact.point, rot2);
        }
        Destroy(obj, 1);
        Destroy(gameObject);

    }
}
