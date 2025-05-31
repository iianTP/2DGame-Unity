using UnityEngine;

public class BackgroundScript : MonoBehaviour
{

    public Transform cameraTransform;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        cameraTransform = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraTransform.position + Vector3.forward*20;

    }
}
