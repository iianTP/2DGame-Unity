using UnityEngine;

public class PassageScript : MonoBehaviour
{

    public Transform cameraTransform;
    public BoxCollider2D playerCollider;
    private Vector3 position = new Vector3(0,10,0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            cameraTransform.position += position;
            position *= -1;
        }
    }
    
}
