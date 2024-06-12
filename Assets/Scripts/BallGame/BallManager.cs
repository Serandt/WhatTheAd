using Oculus.Interaction;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public Material redMaterial;
    public Material blueMaterial;
    public Material yellowMaterial;

    public string ballMaterialTag;
    private Material ballMaterial;

    void Start()
    {
        SetRandomMaterial();
    }



    public void SetRandomMaterial()
    {
        int materialIndex = Random.Range(0, 3);
        switch (materialIndex)
        {
            case 0:
                ballMaterial = redMaterial;
                ballMaterialTag = GameManager.MaterialTag.Red.ToString();
                break;
            case 1:
                ballMaterial = blueMaterial;
                ballMaterialTag = GameManager.MaterialTag.Blue.ToString();
                break;
            case 2:
                ballMaterial = yellowMaterial;
                ballMaterialTag = GameManager.MaterialTag.Yellow.ToString();
                break;
        }
        transform.GetComponent<MeshRenderer>().material = ballMaterial;
    }

    /*void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other.CompareTag("RedBasket") && ballMaterial == redMaterial ||
            other.CompareTag("BlueBasket") && ballMaterial == blueMaterial ||
            other.CompareTag("YellowBasket") && ballMaterial == yellowMaterial)
        {
            GameManager.Instance.AddScore(1);
        }
        else
        {
            GameManager.Instance.AddScore(-1);
        }
        Destroy(gameObject);
    }*/
}