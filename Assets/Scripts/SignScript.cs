using UnityEngine;

public class SignScript : MonoBehaviour
{

    public GameObject textPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            textPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            textPanel.SetActive(false);
        }
    }
}
