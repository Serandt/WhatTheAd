using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseBall : MonoBehaviour
{
    public GameObject obj;
    public Vector3 spawnPosition;

    private void Update()
    {
        if (this.transform.position.y < -0.2f)
        {
            transform.position = spawnPosition;
        }
    }
}
