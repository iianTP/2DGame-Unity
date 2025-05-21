using UnityEngine;

public class GravityScript : MonoBehaviour
{

    public PlayerScript playerScript;

    public string direction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerScript.GravityShift(direction);
        }
    }
}
