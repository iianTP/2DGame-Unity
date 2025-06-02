using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneControllerScript.instance.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
