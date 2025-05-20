using UnityEngine;

public class SpawnPointScript : MonoBehaviour
{
    public PlayerScript playerScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerScript.spawnPoint = transform.position;
        }
    }
}
