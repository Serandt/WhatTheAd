using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    public GameObject hmd;
    private Material defaultMaterial;
    public Material danger1;
    public Material danger2;

    private void Start()
    {
        defaultMaterial = GetComponent<MeshRenderer>().material;
        hmd = GameObject.Find("OVRCameraRig");
    }

    private void Update()
    {
        if (hmd.transform.position.x < -.7f && hmd.transform.position.x > -1.4f)
        {
            this.GetComponent<MeshRenderer>().material = danger1;
        }
        else if (hmd.transform.position.x > .7f && hmd.transform.position.x < 1.4f)
        {
            this.GetComponent<MeshRenderer>().material = danger1;
        }
        else if (hmd.transform.position.x < -1.4f || hmd.transform.position.x > 1.4f)
        {
            this.GetComponent<MeshRenderer>().material = danger2;
        }
        else
        {
            this.GetComponent<MeshRenderer>().material = defaultMaterial;
        }
    }
}
