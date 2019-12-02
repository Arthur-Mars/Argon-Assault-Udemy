using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Particles")]
    public ParticleSystem trail;

    [Space]

    [SerializeField] GameObject deathFX;

    // Start is called before the first frame update
    void Start()
    {
        //AddNonTriggerBoxCollider(); commented function out due to box colliders being way too large
    }

    /*private void AddNonTriggerBoxCollider()
    {
        Collider QordBoxCollider = this.gameObject.AddComponent<BoxCollider>();
        QordBoxCollider.isTrigger = false;
    }*/


    private void OnParticleCollision(GameObject other) // detects collision with the particles (bullets)
    {
        Instantiate(deathFX, transform.position, Quaternion.identity); // create deathFX, at transform position, and don't rotate it
        print("This is working");
        Destroy(this.gameObject);
    }
}