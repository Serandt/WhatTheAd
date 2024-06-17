using UnityEngine;

public class Bin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FalseBall"))
        {
            DarkPatternManager.Instance.removeDarkPatternFromList(other.GetComponent<FalseBall>().obj);
            ControllerInteraction.Instance.ControllerVibration();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
