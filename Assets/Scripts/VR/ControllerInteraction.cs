using UnityEngine;

public class ControllerInteraction : MonoBehaviour
{
    private GameManager.Condition cond;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedButton"))
        {
            cond = other.gameObject.GetComponent<RedButton>().cond;

            if (cond != GameManager.Condition.None)
            {
                GameManager.Instance.StartGame(cond);
                other.gameObject.GetComponent<RedButton>().PopUps.SetActive(true);
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("ProximityAd")|| other.CompareTag("Popup")) 
        {
            DarkPatternManager.Instance.removeDarkPatternFromList(other.gameObject);
        }
    }
}
