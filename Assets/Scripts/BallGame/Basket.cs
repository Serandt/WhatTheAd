using UnityEngine;

public class Basket : MonoBehaviour
{
    public GameManager.MaterialTag correctMaterialTag;

    private void OnTriggerEnter(Collider other)
    {
        BallManager ball = other.GetComponent<BallManager>();
        if (ball != null)
        {
            if (ball.ballMaterialTag == correctMaterialTag.ToString())
            {
                GameManager.Instance.AddScore();
            }
            else
            {
                GameManager.Instance.AddError();
            }
            Destroy(other.gameObject);
        }

        if (other.CompareTag("FalseBall"))
        {
            other.GetComponent<Transform>().position = other.GetComponent<FalseBall>().spawnPosition;
        }
    }
}
