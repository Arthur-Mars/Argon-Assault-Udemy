using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Particles")]
    public ParticleSystem trail;
    public ParticleSystem explosion;

    [Header("Sound FX")]
    public AudioClip explosionSound;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other) // detects collision with the particles (bullets)
    {
        explosion.Play();
        audioSource.PlayOneShot(explosionSound);
        Destroy(this.gameObject);
    }
}
