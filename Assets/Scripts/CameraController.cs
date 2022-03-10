using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float animationDelay = 2f;
    public Camera camera;
    // Update is called once per frame

    private void Start()
    {
        StartCoroutine(Animate());
    }


    void LateUpdate()
    {
        transform.position = player.transform.position;
        Animate();
    }


    IEnumerator Animate()
    {
        while (animationDelay >= 0)
        {
            animationDelay -= Time.deltaTime;
            yield return null;
        }
        while (camera.orthographicSize < 15)
        {
            camera.orthographicSize += 3 * Time.deltaTime;
            yield return null;
        }
    }
}
