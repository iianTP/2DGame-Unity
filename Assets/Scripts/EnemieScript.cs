using System.Collections;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemieScript : MonoBehaviour
{

    private int life = 10;

    public bool isBoss;

    public GameObject[] enemies;

    public GameObject ending;

    public Animator vignette;

    private void Awake()
    {
        if (isBoss)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }

    private void LateUpdate()
    {
        if (transform.localScale.x < 0 && enemies.All(e => !e))
        {
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x);
            transform.localScale = newScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            Destroy(collision.gameObject);
            TakeDamage();
        }
    }
    

    void TakeDamage()
    {
        if (!isBoss)
        {
            Destroy(gameObject);
        }
        else if (enemies.All(e => !e))
        {
            if (life > 0) { life--; }
            else {
                StartCoroutine(EndGame());
                //Destroy(gameObject);
            }
        }
    }

    IEnumerator EndGame()
    {
        vignette.enabled = true;
        vignette.Play("VignetteAnimation");
        yield return new WaitForSeconds(4f);
        ending.SetActive(true);
    }

}
