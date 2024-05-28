using UnityEngine;

public class SpawnPopUps : MonoBehaviour
{
    public GameObject popup1;
    public GameObject popup2;
    public GameObject popup3;
    public GameObject popup4;

    private void Awake()
    {
        //TODO: Set time lapse

        Invoke("ActivatePopup1", 5.0f);
        Invoke("ActivatePopup2", 15.0f);
        Invoke("ActivatePopup3", 30.0f);
        Invoke("ActivatePopup4", 45.0f);
    }

    private void ActivatePopup1()
    {
        popup1.gameObject.SetActive(true);
    }

    private void ActivatePopup2()
    {
        popup2.gameObject.SetActive(true);
    }
    private void ActivatePopup3()
    {
        popup3.gameObject.SetActive(true);
    }
    private void ActivatePopup4()
    {
        popup4.gameObject.SetActive(true);
    }
}
