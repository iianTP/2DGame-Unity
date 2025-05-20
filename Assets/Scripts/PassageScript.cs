using UnityEngine;

public class PassageScript : MonoBehaviour
{

    public Transform cameraTransform;
    public Transform playerTransform;
    private Vector3 position = new Vector3(0,10,0);
    private float movePlayer = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cameraTransform.position += position;
            playerTransform.position += Vector3.up * movePlayer;
            position *= -1;
            movePlayer *= -1;
        }
    }
    
}
