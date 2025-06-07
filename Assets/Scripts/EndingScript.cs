using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    public CameraScript cameraScript;
    public Transform cameraTransform;
    // Update is called once per frame
    void Start()
    {
        cameraScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        cameraTransform = GameObject.Find("Main Camera").transform;
        cameraScript.enabled = false;
        cameraTransform.position = new Vector3(0,0,cameraTransform.position.z);
        StartCoroutine(WaitOpening());
    }

    IEnumerator WaitOpening()
    {

        yield return new WaitForSeconds(13f);

        Application.Quit();

        yield return new WaitForSeconds(0f);
    }
}
