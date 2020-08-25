using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager instance { get; private set; }

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void audioPlay(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
