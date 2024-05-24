using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedButton : MonoBehaviour
{
    public OVRInput.Controller controller;
    private float triggerValue;
    private bool isInCollider;

    private void OnTriggerEnter(Collider other)
    {
        triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller);

        if (isInCollider && other.gameObject.CompareTag("RedButton"))
        {
            if (triggerValue > 0.95f )
            {
                GameManager.Instance.StartGame();
                Destroy(other.gameObject);
            }

        }
    }

    
}
