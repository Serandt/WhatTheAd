using UnityEngine;

public class ControllerInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedButton"))
        {
            GameManager.Instance.StartGame();
            Destroy(other.gameObject);
        }
    }

}
