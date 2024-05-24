using UnityEngine;

public class BasketManager : MonoBehaviour
{
    public GameObject redBasket;
    public GameObject blueBasket;
    public GameObject yellowBasket;

    public Transform[] spawnPositions;

    void Start()
    {
        ShuffleAndPlaceBaskets();
    }

    void ShuffleAndPlaceBaskets()
    {
        // Randomize the spawn positions
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            int rnd = Random.Range(0, spawnPositions.Length);
            Transform temp = spawnPositions[rnd];
            spawnPositions[rnd] = spawnPositions[i];
            spawnPositions[i] = temp;
        }

        // Place baskets at randomized positions
        redBasket.transform.position = spawnPositions[0].position;
        blueBasket.transform.position = spawnPositions[1].position;
        yellowBasket.transform.position = spawnPositions[2].position;
    }
}