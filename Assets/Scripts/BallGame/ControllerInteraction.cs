using UnityEngine;

public class ControllerInteraction : MonoBehaviour
{
    public OVRInput.Controller controller;
    private float triggerValue;
    private int originalLayer;
    public GameObject ball;

    public static ControllerInteraction Instance;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);

        if (ball != null)
        {
            if (triggerValue > 0.95f)
            {
                ball.GetComponent<Rigidbody>().useGravity = false;
                ball.transform.position = transform.position;
            }
            else
            {
                ball.GetComponent<Rigidbody>().useGravity = true;
            }
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
            if (ball == null)
            {
                ball = other.gameObject;
                originalLayer = gameObject.layer;
                ball.layer = LayerMask.NameToLayer("IgnoreRaycastDuringDrag");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            other.gameObject.layer = originalLayer;
            ball = null;
        }
    }
}
