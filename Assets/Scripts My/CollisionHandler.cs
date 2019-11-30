using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ok as long as this is the onlyn script that loads scenes

public class CollisionHandler : MonoBehaviour
{

    //[Header("Particles")]
    [Tooltip("Level delay")] [SerializeField] float loadLevelDelay = 1.5f;

    void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        Invoke("ReloadScene", loadLevelDelay);
    }

    private void StartDeathSequence()
    {
        print("player dying");
        SendMessage("OnPlayerDeath");
    }

    private void ReloadScene()  // string referenced
    {
        SceneManager.LoadScene(1);
    }
}
