using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInteraction : MonoBehaviour
{
    public OVRInput.Controller controller;
    private float triggerValue;
    private bool isInCollider;
    private int originalLayer;
    private bool isGrabbing;
    private GameObject ball;

    private void Update()
    {
        if (isGrabbing)
        {
            ball.transform.position = this.transform.position;
        }

        triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);

        if (triggerValue > 0.95f) 
        {
            Debug.Log("Trigger pressed");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        isInCollider = true;
        triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);

        if (isInCollider && triggerValue > 0.95f)
        {
            if (collision.gameObject.CompareTag("RedButton"))
            {
                Debug.Log("Red Button Collision");
                GameManager.Instance.StartGame();
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("Ball"))
            {
                Debug.Log("Ball Collision");
                originalLayer = gameObject.layer;
                collision.gameObject.layer = LayerMask.NameToLayer("IgnoreRaycastDuringDrag");
                ball = collision.gameObject;
                isGrabbing = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isInCollider = false;
        if (collision.gameObject.CompareTag("Ball"))
        {
            isGrabbing = false;
            collision.gameObject.layer = originalLayer;
        }
    }

}
