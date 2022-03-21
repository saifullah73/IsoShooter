using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioSource audioFX;
    [SerializeField]
    private AudioClip healthPickup;
    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    public void PlayHealthPickup()
    {
        audioFX.clip = healthPickup;
        audioFX.Play();
    }
}
