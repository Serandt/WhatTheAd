using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    public GameObject hmd;
    private Material defaultMaterial;
    public Material danger1;
    public Material danger2;
    public Material danger3;


    public float threshold1 = 25f; 
    public float threshold2 = 50f;
    public float threshold3 = 75f;

    private Vector3 maxDistances;


    private Vector3 currentPosition;

    private void Start()
    {
        defaultMaterial = GetComponent<MeshRenderer>().material;
        Bounds bounds = GetComponent<MeshFilter>().mesh.bounds;
        maxDistances = bounds.extents;
    }

    private void Update()
    {
        /*  currentPosition = hmd.transform.position;

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
        
    */
        currentPosition = hmd.transform.position;
        Vector3 toPlayer = currentPosition - transform.position;
        Vector3 distance = new Vector3(
            Mathf.Abs(toPlayer.x),
            Mathf.Abs(toPlayer.y),
            Mathf.Abs(toPlayer.z)
        );


        Vector3 distancePercentages = new Vector3(
            distance.x / maxDistances.x,
            distance.y / maxDistances.y,
            distance.z / maxDistances.z
        ) * 100;


        float minDistancePercentage = Mathf.Min(distancePercentages.x, distancePercentages.z);


        if (minDistancePercentage <= threshold1)
        {
            this.GetComponent<MeshRenderer>().material = defaultMaterial;
        }
        else if (minDistancePercentage <= threshold2)
        {
            this.GetComponent<MeshRenderer>().material = danger1;

        }
        else if (minDistancePercentage <= threshold3)
        {
            this.GetComponent<MeshRenderer>().material = danger2;
        }
        else
        {
            this.GetComponent<MeshRenderer>().material = danger3;
        }
    }
}

   /* private bool InZone(Rect zone)
    {
        if (currentPosition.x > zone.x && currentPosition.z > zone.y
            && currentPosition.x < zone.x + zone.width
            && currentPosition.z < zone.y + zone.height)
        {
            return true;
        }
        return false;
    }*/

