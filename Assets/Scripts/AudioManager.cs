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
    public AudioClip UIClip;
    public AudioClip GameStartClip;
    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    public void PlayHealthPickup()
    {
        audioFX.clip = healthPickup;
        audioFX.Play();
    }

    public void PlayUISoundEffect()
    {
        audioFX.clip = UIClip;
        audioFX.Play();
    }

    public void PlayGameStartAudio()
    {
        audioFX.clip = GameStartClip;
        audioFX.Play();
    }
}
