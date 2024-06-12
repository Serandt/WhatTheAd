using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("FalseBall"))
        {
            Debug.Log("ID Ball: " + other.GetComponent<FalseBall>().id);
            Debug.Log("False Friend DarkPattern: " + DarkPatternManager.Instance.activePatterns.Find(item => item.ID == other.GetComponent<FalseBall>().id));
            Debug.Log("False Friend GameObject: " + DarkPatternManager.Instance.activePatterns.Find(item => item.ID == other.GetComponent<FalseBall>().id).GameObject);
            DarkPatternManager.Instance.removeDarkPatternFromList(DarkPatternManager.Instance.activePatterns.Find(item => item.ID == other.GetComponent<FalseBall>().id).GameObject);
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
