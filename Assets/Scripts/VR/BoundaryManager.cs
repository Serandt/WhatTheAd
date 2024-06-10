using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    public GameObject hmd;
    private Material defaultMaterial;
    public Material danger1;
    public Material danger2;
    public Material danger3;

    private Vector3 currentPosition;

    private void Start()
    {
        defaultMaterial = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        currentPosition = hmd.transform.position;

        if (InZone(GameZone.Instance.zones[3]))
        {
            this.GetComponent<MeshRenderer>().material = defaultMaterial;
        }
        else if (InZone(GameZone.Instance.zones[2])
            && !InZone(GameZone.Instance.zones[3]))
        {
            this.GetComponent<MeshRenderer>().material = danger1;
        }
        else if (InZone(GameZone.Instance.zones[1])
            && !InZone(GameZone.Instance.zones[2])
            && !InZone(GameZone.Instance.zones[3]))
        {
            this.GetComponent<MeshRenderer>().material = danger2;
        }
        else if (InZone(GameZone.Instance.zones[0])
            && !InZone(GameZone.Instance.zones[1])
            && !InZone(GameZone.Instance.zones[2])
            && !InZone(GameZone.Instance.zones[3]))
        {
            this.GetComponent<MeshRenderer>().material = danger3;
        }
    }

    private bool InZone(Rect zone)
    {
        if (currentPosition.x > zone.x && currentPosition.y > zone.y
            && currentPosition.x < zone.x + zone.width
            && currentPosition.y < zone.y + zone.height)
        {
            return true;
        }
        return false;
    }
}
