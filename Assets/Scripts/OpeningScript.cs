using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningScript : MonoBehaviour
{

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(WaitOpening());
    }

    IEnumerator WaitOpening()
    {
        yield return new WaitForSeconds(16f);

        SceneManager.LoadSceneAsync(0);

        yield return new WaitForSeconds(0f);
    }
}
