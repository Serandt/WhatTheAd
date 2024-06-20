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
        bool isSelectButtonPressed = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
        if (other.CompareTag("RedButton") && isSelectButtonPressed)
        {
            cond = other.gameObject.GetComponent<RedButton>().cond;

            GameManager.Instance.StartGame(cond);
        }
        if (other.CompareTag("TutorialButton") && isSelectButtonPressed)
        {
            cond = other.gameObject.GetComponent<RedButton>().cond;

            GameManager.Instance.PlayTutorial(); ;
        }

        if (other.CompareTag("ProximityAd") ||  other.CompareTag("TutorialAd")) 
        {
            DarkPatternManager.Instance.removeDarkPatternFromList(other.gameObject);
            ControllerVibration();
        }
        if (other.CompareTag("Popup") )
        {
            if (other.GetComponent<MessagePattern>().canBeClosed)
            {
                DarkPatternManager.Instance.removeDarkPatternFromList(other.gameObject);
                ControllerVibration();
            }
        }
    }
        public void ControllerVibration()
        {
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
            Invoke("StopControllerVibration", 0.2f);
        }

        void StopControllerVibration()
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }
    }
