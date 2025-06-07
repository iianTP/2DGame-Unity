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
            SceneControllerScript.instance.LoadLevel(1 + 5*(level-1));
            if (level == 4)
            {
                DontDestroyOnLoad(camera);
            }
        }
    }


}
