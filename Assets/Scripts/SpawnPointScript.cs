using UnityEngine;

public class SpawnPointScript : MonoBehaviour
{
    public PlayerScript playerScript;
    public Transform playerTransform;

    private void Awake()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        playerTransform = GameObject.Find("Player").transform;
        playerTransform.position = transform.position;
        playerScript.spawnPoint = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerScript.spawnPoint = transform.position;
        }
    }
}
