using TMPro;
using UnityEngine;

public class MessagePattern : DarkPattern
{
    private (string, string)[] Messages = new (string, string)[] {    
        ("Your score is only ", " point(s) below average! Keep going!"),   
        ("Current score at least ", "% down the average of others players at current time..."),   
        ("In average players closed the last ad at least ", "% faster than you..."),
        ("You can ignore me, you won't reach the highscore anyways :)", null),
        ("Are you trying to lose on purpose? Because you're doing an excellent job!", null)
    };

    private System.Random random = new System.Random();
    private GameObject Player;

    void Start()
    {
        Player = GameObject.Find("CenterEyeAnchor");
        GameObject child = transform.GetChild(3).gameObject;
        child.GetComponent<TextMeshPro>().text = GenerateMessage();
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
}
