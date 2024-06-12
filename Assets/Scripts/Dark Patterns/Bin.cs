using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("FalsBall"))
        {
            DarkPatternManager.Instance.removeDarkPatternFromList(DarkPatternManager.Instance.activePatterns.Find(item => item.ID == other.GetComponent<FalseBall>().id).gameObject);
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
