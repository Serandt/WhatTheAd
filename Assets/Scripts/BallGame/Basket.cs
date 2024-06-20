using UnityEngine;

public class Basket : MonoBehaviour
{
    public GameManager.MaterialTag correctMaterialTag;

    private bool ballIsInContact = false;
    private Collider currentBallCollider = null;

    private void Update()
    {
        if (ballIsInContact && !AnyButtonsPressed())
        {
            ProcessBall(currentBallCollider);
            ballIsInContact = false; 
            currentBallCollider = null; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallManager>() != null || other.CompareTag("FalseBall"))
        {
            ballIsInContact = true;
            currentBallCollider = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == currentBallCollider)
        {
            ballIsInContact = false;
            currentBallCollider = null;
        }
    }

    private void ProcessBall(Collider other)
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

    private bool AnyButtonsPressed()
    {
        return OVRInput.Get(OVRInput.Button.Any);
    }
}
