using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShip : MonoBehaviour
{

    [Tooltip("In ms^-1")] [SerializeField] float speed = 0.4f;
    [Tooltip("In m")] [SerializeField] float xRange = 0.11f;
    [Tooltip("In m")] [SerializeField] float yRange = 0.083f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;

        float rawXPosition = transform.localPosition.x + xOffset;
        float rawYPosition = transform.localPosition.y + yOffset;

        float clampedXPosition = Mathf.Clamp(rawXPosition, -xRange, xRange);
        float clampedYPosition = Mathf.Clamp(rawYPosition, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPosition, clampedYPosition, transform.localPosition.z);
    }
}
