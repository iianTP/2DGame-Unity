using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ProjectileScript : MonoBehaviour
{

    public Transform playerTransform;

    public Rigidbody2D rb;

    public float speed;

    private bool shooting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
        transform.position = playerTransform.position;
        Vector3 newScale = transform.localScale;
        newScale.x *= - playerTransform.localScale.x / Mathf.Abs(playerTransform.localScale.x);
        transform.localScale = newScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (shooting) { return; }
        StartCoroutine(Shoot());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Shoot()
    {
        shooting = true;
        rb.linearVelocityX = speed*playerTransform.localScale.x;

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);

        yield return new WaitForSeconds(0f);
    }
}
