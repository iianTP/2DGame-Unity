using UnityEngine;

public class GravityScript : MonoBehaviour
{

    public PlayerScript playerScript;

    public string direction;

    private void Awake()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerScript.GravityShift(direction);
        }
    }
}
