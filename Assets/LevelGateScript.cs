using UnityEngine;

public class LevelGateScript : MonoBehaviour
{
    public int level;
    public bool completed;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!completed)
        {
            SceneControllerScript.instance.LoadLevel(1 + 5*(level-1));
        }
    }


}
