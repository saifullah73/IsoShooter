using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float timeToDie = 2f;
    public float damage = 10f;
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
        if (collision.gameObject.CompareTag("enemy"))
        {
            //collision.gameObject.GetComponent<EnemyController>().DamageEnemy(damage);
        }
        Destroy(gameObject);
    }
}
