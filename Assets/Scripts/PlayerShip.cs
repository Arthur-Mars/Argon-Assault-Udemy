using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShip : MonoBehaviour
{

    [Tooltip("In ms^-1")] [SerializeField] float speed = 0.4f;
    [Tooltip("In m")] [SerializeField] float xRange = 0.16f;
    [Tooltip("In m")] [SerializeField] float yRange = 0.083f;

    [SerializeField] float positionPitchFactor = 5f; // fields to move the pitch
    [SerializeField] float controlPitchFactor = 15f;

    [SerializeField] float positionYawFactor = 5f; // fields to move the yaw
    [SerializeField] float controlYawFactor = 15f;

    [SerializeField] float controlRollFactor = -15f;

    float xThrow, yThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float yawDueToPosition = transform.localPosition.y * positionYawFactor;
        float yawDueToControlThrow = xThrow * controlYawFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = yawDueToPosition + yawDueToControlThrow;
        float roll = xThrow * controlRollFactor;


        transform.localRotation = Quaternion.Euler(pitch, yaw, roll); // Euler(pitch, yaw, roll) < x, y, z
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = -yThrow * speed * Time.deltaTime;

        float rawXPosition = transform.localPosition.x + xOffset;
        float rawYPosition = transform.localPosition.y + yOffset;

        float clampedXPosition = Mathf.Clamp(rawXPosition, -xRange, xRange);
        float clampedYPosition = Mathf.Clamp(rawYPosition, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPosition, clampedYPosition, transform.localPosition.z);
    }
}
