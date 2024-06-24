using System.Collections;
using TMPro;
using UnityEngine;

public class MessagePattern : DarkPattern
{
    private (string, string)[] Messages = new (string, string)[] {
        ("Your score is only ", " point(s) below average!"),
        ("Your score is at least ", "% below the average."),
        ("Others closed their last ad ", "% faster than you."),
        ("Ignore this, you won't hit the highscore :)", null),
        ("Trying to lose? You're excelling at it!", null),
        ("Still playing? Your score says otherwise.", null),
        ("Others are improving, you’re just lagging behind.", null),
        ("That effort was pathetic, honestly.", null),
        ("Ever thought of quitting? Now might be the time.", null),
        ("Is this your best? Let’s hope not.", null),
        ("You're getting worse. Consider stopping.", null),
        ("Maybe gaming isn’t for you. Ever tried knitting?", null),
        ("Don’t feel bad, some just aren’t cut out for this.", null),
        ("Why keep trying? You’re not making any progress.", null),
        ("Ouch, that performance was hard to watch.", null)
    };

    private System.Random random = new System.Random();
    private GameObject Player;
    private float closeTime = 5;
    public bool canBeClosed;
    public GameObject closeText;
    public GameObject closeCounter;

    void Start()
    {
        canBeClosed = false;
        Player = GameObject.Find("CenterEyeAnchor");
        GameObject child = transform.GetChild(3).gameObject;
        child.GetComponent<TextMeshPro>().text = GenerateMessage();
        closeText.GetComponent<TextMeshPro>().color = Color.grey;

        StartCoroutine(CountdownTimer());


    }

    private void Update()
    {
        FacePlayer();
  
    }

    string GenerateMessage()
    {
        (string, string) message = Messages[random.Next(0, Messages.Length)];

        if (message.Item2 == null)
        {
            return message.Item1;
        }

        float value = random.Next(1, 15);
        return $"{message.Item1}{value}{message.Item2}";

    }

    void FacePlayer()
    {
        // Calculate the direction from the spawned object to the player
        Vector3 directionToPlayer = Player.transform.position - this.transform.position;

        // Calculate the rotation needed to face the player
        Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);

        // Apply the rotation to the spawned object
        this.transform.rotation = rotationToPlayer;
    }

    void SetButtonInteractable()
    {
        canBeClosed = true;
        closeText.GetComponent<TextMeshPro>().color = Color.white;
    }

    private IEnumerator CountdownTimer()
    {
        while (closeTime > 0)
        {
            closeCounter.GetComponent<TextMeshPro>().text = "can be closed in: " + ((int)closeTime).ToString();
            yield return new WaitForSeconds(1f);
            closeTime -= 1f;
        }
        closeCounter.GetComponent<TextMeshPro>().text = ""; // Clear counter text when done
        SetButtonInteractable(); // Consider calling this here if timing is critical
    }


  
}
