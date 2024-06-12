using UnityEngine;

public class ControllerInteraction : MonoBehaviour
{
    private GameManager.Condition cond;
    public GameObject buttons;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedButton"))
        {
            cond = other.gameObject.GetComponent<RedButton>().cond;

            GameManager.Instance.StartGame(cond);
        }
        if (other.CompareTag("TutorialButton"))
        {
            cond = other.gameObject.GetComponent<RedButton>().cond;

            GameManager.Instance.PlayTutorial(); ;
        }

        if (other.CompareTag("ProximityAd")|| other.CompareTag("Popup") || other.CompareTag("TutorialAd")) 
        {
            DarkPatternManager.Instance.removeDarkPatternFromList(other.gameObject);
        }
    }
}
