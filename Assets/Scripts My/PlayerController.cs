using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

/* NOTE: All the Cinemachine related code will be left out until I learn more about the Cinemachine 
         functions and features and how to utilize them properly without destroying the project */

public class PlayerController : MonoBehaviour
{
    private Transform playerModel; // this is how we make the ship rotate by 90 degrees while moving along the x-axis
    bool controlsAreEnabled = true;

    [Header("Public References")]
    public Transform aimTarget;

    [Space]

    [Header("Settings")]
    public bool joystick = true;

    [Space]

    [Header("Parameters")]
    public float xySpeed = 0.2f;
    public float lookSpeed = 10000f;

    [Space]

    [Header("Particles")]
    public ParticleSystem trail;
    public ParticleSystem explosion;
    public ParticleSystem barrel;
    public ParticleSystem stars;

    [Header("Sound FX")]
    public AudioClip explosionSound;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerModel = transform.GetChild(0); //after creating the Transform playerModel above, we get the child of the object (ship model) to complete the rotation
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = joystick ? Input.GetAxis("Horizontal") : Input.GetAxis("Mouse X");
        float v = joystick ? Input.GetAxis("Vertical") : Input.GetAxis("Mouse Y");

        if (controlsAreEnabled)
        {
            LocalMove(h, -v, xySpeed);
            RotationLook(h, -v, lookSpeed);
            HorizontalLean(playerModel, h, 80, 0.035f);
        }
        

        if (Input.GetButtonDown("BumperLeft") || Input.GetButtonDown("BumperRight"))
        {
            int dir = Input.GetButtonDown("BumperLeft") ? -1 : 1; // if LT is pressed, roll left, else roll right
            QuickSpin(dir);
        }

    }

    void LocalMove(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void RotationLook(float horizontal, float vertical, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(horizontal, vertical, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
    }

    public void QuickSpin(int direction) 
    {
        if (!DOTween.IsTweening(playerModel)) 
        {
            playerModel.DOLocalRotate(new Vector3(playerModel.localEulerAngles.x, playerModel.localEulerAngles.y, 360 * -direction), 0.4f, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine); 
        }
    }

    void DistortionAmount(float x)
    {
        Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<LensDistortion>().intensity.value = x;
    }

    void Chromatic(float x)
    {
        Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<ChromaticAberration>().intensity.value = x;
    }

    void OnPlayerDeath() // called by string referene
    {
        controlsAreEnabled = false;
        explosion.Play();
        audioSource.PlayOneShot(explosionSound);
        print("control is frozen");
    }

}
