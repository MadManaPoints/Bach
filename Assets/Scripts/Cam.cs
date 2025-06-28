using UnityEngine;

public class Cam : MonoBehaviour
{
    PlayerMovement player;

    void Start()
    {
        player = PlayerMovement.player; 
    }

    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x + 1.5f, transform.position.y, transform.position.z);
    }
}
