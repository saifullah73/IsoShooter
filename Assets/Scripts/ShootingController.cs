using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject bulletTemplate;
    public GameObject attackPoint;
    public float shootDelay = 0.1f;
    private bool isShooting = false;
    public GameObject gun;
    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (isShooting)
            {
                PauseShooting();
                isShooting = false;
            }
            else
            {
                ResumeShooting(true);
                isShooting = true;
            }

        }
        AimGun();

    }

    void AimGun()
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        // invesring angle becuase unity rotates anti-clockwise and adding 45 to offset for isometric
        angle = -angle - 45;
        gun.transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void PauseShooting()
    {
        gun.SetActive(false);
        CancelInvoke("Shoot");
    }

    public void ResumeShooting(bool resumeShooting = false)
    {
        gun.SetActive(true);
        if (resumeShooting)
            InvokeRepeating("Shoot", 0, shootDelay);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletTemplate, attackPoint.transform.position, attackPoint.transform.rotation);
        bullet.transform.forward = attackPoint.transform.forward;
        bullet.GetComponent<Rigidbody>().velocity = attackPoint.transform.forward.normalized * 100;
        //bullet.GetComponent<Rigidbody>().AddForce(attackPoint.transform.forward.normalized * 100, ForceMode.Impulse);
    }
}
