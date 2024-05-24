using UnityEngine;

public class PlateBounds : MonoBehaviour
{
    public float wallHeight = 1.0f; 
    public float wallThickness = 0.1f; 
    private void Start()
    {
        CreateBounds();
    }

    private void CreateBounds()
    {
        Renderer renderer = GetComponent<Renderer>();
        Vector3 plateSize = renderer.bounds.size;

        float halfThickness = wallThickness / 2;
      

        Vector3 frontWallPos = new Vector3(0, wallHeight / 2, (plateSize.z/ 1.3f ) + halfThickness);
        Vector3 backWallPos = new Vector3(0, wallHeight / 2, -(plateSize.z / 1.3f) - halfThickness);
        Vector3 rightWallPos = new Vector3((plateSize.x / 1.3f) + halfThickness, wallHeight / 2, 0);
        Vector3 leftWallPos = new Vector3(-(plateSize.x / 1.3f) - halfThickness, wallHeight / 2, 0);

        CreateWall(new Vector3(plateSize.x + wallThickness, wallHeight, wallThickness), frontWallPos); // front wall
        CreateWall(new Vector3(plateSize.x + wallThickness, wallHeight, wallThickness), backWallPos); // back wall
        CreateWall(new Vector3(wallThickness, wallHeight, plateSize.z + wallThickness), rightWallPos); // right wall
        CreateWall(new Vector3(wallThickness, wallHeight, plateSize.z + wallThickness), leftWallPos); // left wall

  
    }

    private void CreateWall(Vector3 size, Vector3 position)
    {
        GameObject wall = new GameObject("Wall");
        wall.transform.parent = transform;
        wall.transform.localPosition = position;
        BoxCollider wallCollider = wall.AddComponent<BoxCollider>();
        wallCollider.size = size;
    }
}
