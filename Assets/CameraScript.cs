using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Transform playerTransform;

    private void Update()
    {
        CameraMovement();
    }

    void CameraMovement()
    {

        transform.position = new Vector3(transform.position.x,playerTransform.position.y,transform.position.z);

    }


}
