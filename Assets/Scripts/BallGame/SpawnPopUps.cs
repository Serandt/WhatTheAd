using UnityEngine;

public class SpawnPopUps : MonoBehaviour
{
    public GameObject popup1;
    public GameObject popup2;
    public GameObject popup3;
    public GameObject popup4;

    private float gameTime;

    void Start()
    {
        gameTime = GameManager.gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameTime)
        {
            case 250f:
                popup1.SetActive(false);
                break;
            case 190f:
                popup2.SetActive(false);
                break;
            case 110f:
                popup3.SetActive(false);
                break;
            case 70f:
                popup4.SetActive(false);
                break;
        }
    }
}
