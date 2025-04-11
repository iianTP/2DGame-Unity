using UnityEngine;

public class SwordScript : MonoBehaviour
{

    public float damage;
    public GameObject player;
    Transform playerTransform;
    PlayerScript playerScript;
    float timer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.GetComponent<Transform>();
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        setDirection();
        yielding();
    }

    void setDirection()
    {
        Vector3 offset;
        Quaternion rotation;

        switch (playerScript.direction)
        {
            case "up":
                offset = new Vector3(0, 0.4f, 0);
                rotation = Quaternion.Euler(Vector3.forward * 0);
                break;
            case "down":
                offset = new Vector3(0, -0.4f, 0);
                rotation = Quaternion.Euler(Vector3.forward * 0);
                break;
            case "left":
                offset = new Vector3(-0.4f, 0, 0);
                rotation = Quaternion.Euler(Vector3.forward * 90);
                break;
            case "right":
                offset = new Vector3(0.4f, 0, 0);
                rotation = Quaternion.Euler(Vector3.forward * 90);
                break;
            default:
                return;
        }

        transform.SetPositionAndRotation(playerTransform.position + offset, rotation);
    }

    void yielding()
    {
        

        if (timer < 0.2)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

            
    }

}
