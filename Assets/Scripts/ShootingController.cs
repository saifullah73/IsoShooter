using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject bulletTemplate;
    public GameObject attackPoint;
    public float shootDelay = 0.1f;
    public GameObject gun;
    public GameObject pivot;
    public ParticleSystem muzzleFlash;
    private Vector2 mouseOnScreen;

    public FixedJoystick aimJoystick;



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
#if UNITY_EDITOR
        mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        // invesring angle becuase unity rotates anti-clockwise and adding 45 to offset for isometric
        angle = -angle - 45;


#else
      //if (Input.touchCount > 0)
      //  {
      //      Vector3 pos = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y,0);
      //      mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(pos);
      //  }
        Vector2 direction = aimJoystick.Direction;
        float angle = AngleBetweenTwoPoints(Vector2.up, direction);
        angle = scale(0, 180, 0, 360, angle);
        angle = -angle + 45;
#endif

        gun.transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
        pivot.transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void PauseShooting()
    {
        gun.SetActive(false);
        muzzleFlash.Stop();
        CancelInvoke("Shoot");
    }

    public void ResumeShooting(float startdelay = 0f)
    {
        gun.SetActive(true);
        Invoke("startMuzzleFlash", startdelay);
        InvokeRepeating("Shoot",startdelay, shootDelay);
    }

    public void startMuzzleFlash()
    {
        muzzleFlash.Play();
    }


    void Shoot()
    {
        GameObject bullet = Instantiate(bulletTemplate, attackPoint.transform.position, attackPoint.transform.rotation);
        //bullet.transform.forward = attackPoint.transform.forward;
        bullet.GetComponent<Rigidbody>().velocity = attackPoint.transform.forward.normalized * 100;
    }


    public float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }
}
