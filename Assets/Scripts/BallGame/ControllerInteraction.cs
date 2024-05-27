using UnityEngine;

public class ControllerInteraction : MonoBehaviour
{
    public OVRInput.Controller controller;
    private float triggerValue;
    private bool ballSelected = false;
    private int originalLayer;
    private bool isGrabbing;
    private GameObject ball;
    private GameObject interaction;

    private void Update()
    {
        if (ballSelected)
        {
            ball.transform.position = transform.position;
        }

        if (isGrabbing)
        {
            Debug.Log("current trigger value: " + OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller));
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        ballSelected = true;
        interaction = other.gameObject;

        if (other.CompareTag("RedButton"))
        {
            GameManager.Instance.StartGame();
            Destroy(interaction.gameObject);
        }

        if (other.CompareTag("Ball"))
        {
            Debug.Log("Ball Triggered");
            originalLayer = gameObject.layer;
            other.gameObject.layer = LayerMask.NameToLayer("IgnoreRaycastDuringDrag");
            ball = other.gameObject;
            ballSelected = true;
            other.GetComponent<Rigidbody>().useGravity = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ballSelected = false;
        if (other.gameObject.CompareTag("Ball"))
        {
            isGrabbing = false;
            other.gameObject.layer = originalLayer;
        }
    }

}
