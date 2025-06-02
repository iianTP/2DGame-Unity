using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class EndLevelScript : MonoBehaviour
{
    public PlayerScript playerScript;

    public Animator animator;

    public Sprite background;

    public bool active;

    private void Awake()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        animator = gameObject.GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Activate());
        }
    }

    IEnumerator Activate()
    {
        animator.SetBool("IsActive",true);

        yield return new WaitForSeconds(2f);

        SceneControllerScript.instance.LoadLevel(0);

        yield return new WaitForSeconds(0f);
    }
}
