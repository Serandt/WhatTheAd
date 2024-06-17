using UnityEngine;

public class ControllerInteraction : MonoBehaviour
{
    private GameManager.Condition cond;

    public static ControllerInteraction Instance;

    private void Start()
    {
        Instance = this;
    }

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
            ControllerVibration();
        }
    }
    public void ControllerVibration()
    {
        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        Invoke("StopControllerVibration", 0.2f);
    }

    void StopControllerVibration()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
}
