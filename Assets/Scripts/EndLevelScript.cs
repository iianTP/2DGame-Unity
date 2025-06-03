using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndLevelScript : MonoBehaviour
{
    public PlayerScript playerScript;

    public Animator animator;

    public SpriteRenderer backgroundSr;
    public Sprite backgroundSprite;

    public GameObject oldTiles;
    public GameObject newTiles;

    public bool active;

    private string[] activeAnimations = new string[3] { "ActiveWindAnimation", "ActiveWaterAnimation", "ActiveSolarAnimation" };

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
        yield return new WaitForSeconds(1f);

        if (animator) animator.Play(activeAnimations[(SceneManager.GetActiveScene().buildIndex / 5) - 1]);
        backgroundSr.sprite = backgroundSprite;
        oldTiles.SetActive(false);
        newTiles.SetActive(true);

        yield return new WaitForSeconds(2f);

        playerScript.stage = SceneManager.GetActiveScene().buildIndex / 5;
        SceneControllerScript.instance.LoadLevel(0);

        yield return new WaitForSeconds(0f);
    }
}
