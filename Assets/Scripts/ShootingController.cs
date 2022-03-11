using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject bulletTemplate;
    public GameObject attackPoint;
    public float shootDelay = 0.1f;
    public GameObject gun;


    void Start()
    {
        ResumeShooting(2f);
    }

    
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    if (isShooting)
        //    {
        //        PauseShooting();
        //        isShooting = false;
        //    }
        //    else
        //    {
        //        ResumeShooting(true);
        //        isShooting = true;
        //    }

        //}
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

    public void ResumeShooting(float startdelay = 0f)
    {
        gun.SetActive(true);
        InvokeRepeating("Shoot",startdelay, shootDelay);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletTemplate, attackPoint.transform.position, attackPoint.transform.rotation);
        bullet.transform.forward = attackPoint.transform.forward;
        bullet.GetComponent<Rigidbody>().velocity = attackPoint.transform.forward.normalized * 100;
    }
}
