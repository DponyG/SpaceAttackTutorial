using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [Tooltip("In ms^ -1")][SerializeField] float xSpeed = 4;
    [SerializeField] float xClamp = 5;
    [SerializeField] float yClamp = 3;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = -5f;
    [SerializeField] float controlYawFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ProcessTranslation();
        ProcessRotation();

    }

    private void ProcessRotation()
    {

        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float yawDueToControlThrow = xThrow * controlYawFactor;
        float yawDueToPosition = transform.localPosition.x * positionYawFactor;

        float pitch = pitchDueToControlThrow + pitchDueToPosition;
        float yaw = yawDueToControlThrow + yawDueToPosition;
        float roll = xThrow * controlRollFactor; 

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float xOffsetThisFrame = xThrow * xSpeed * Time.deltaTime;
        float yOffsetThisFrame = yThrow * xSpeed * Time.deltaTime;

        float rawNewXPos = transform.localPosition.x + xOffsetThisFrame;
        float rawNewYPos = transform.localPosition.y + yOffsetThisFrame;

        float xPos = Mathf.Clamp(rawNewXPos, -xClamp, xClamp);
        float yPos = Mathf.Clamp(rawNewYPos, -yClamp, yClamp);

        transform.localPosition = new Vector3(xPos, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);
    }
}
