using UnityEngine;

public class Basket : MonoBehaviour
{
    public GameManager.MaterialTag correctMaterialTag;

    private void OnTriggerEnter(Collider other)
    {
        BallManager ball = other.GetComponent<BallManager>();
        if (ball != null)
        {
            // Überprüfe das Material der Kugel basierend auf dem Tag
            if (ball.ballMaterialTag == correctMaterialTag.ToString())
            {
                GameManager.Instance.AddScore(1);
            }
            else
            {
                GameManager.Instance.AddScore(-1);
            }
            Destroy(other.gameObject);
        }
    }
}
