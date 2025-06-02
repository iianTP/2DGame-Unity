using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public Transform portalOutTransform;
    public Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerTransform.position = portalOutTransform.position;
        }
    }
}
