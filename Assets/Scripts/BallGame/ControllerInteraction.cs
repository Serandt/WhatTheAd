using UnityEngine;

public class ControllerInteraction : MonoBehaviour
{
    public OVRInput.Controller controller;
    private float triggerValue;
    private bool ballSelected;
    private int originalLayer;
    public GameObject ball;

    public static ControllerInteraction Instance;

    private void Start()
    {
        ballSelected = false;
        Instance = this;
    }

    private void Update()
    {
        triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);

        if (ballSelected && triggerValue > 0.95f && ball != null)
        {
            ball.GetComponent<Rigidbody>().useGravity = false;
            ball.transform.position = transform.position;
        }
        else if (ballSelected && ball != null)
        {
            ball.GetComponent <Rigidbody>().useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedButton"))
        {
            GameManager.Instance.StartGame();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Ball"))
        {
            if (!ballSelected)
            {
                ball = other.gameObject;
                originalLayer = gameObject.layer;
                ball.layer = LayerMask.NameToLayer("IgnoreRaycastDuringDrag");
                ballSelected = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ballSelected = false;
            other.gameObject.layer = originalLayer;
            ball = null;
        }
    }

}
