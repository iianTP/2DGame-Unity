using UnityEngine;

public class LevelGateScript : MonoBehaviour
{
    public int level;
    public bool completed;
    public GameObject camera;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!completed && collision.tag == "Player")
        {
            if (level == 4)
            {
                DontDestroyOnLoad(camera);
                DontDestroyOnLoad(GameObject.Find("Music"));
            }
            else
            {
                Destroy(GameObject.Find("Music"));
            }
                SceneControllerScript.instance.LoadLevel(1 + 5 * (level - 1));
            

        }
    }


}
