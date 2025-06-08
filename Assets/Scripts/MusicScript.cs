using UnityEngine;

public class MusicScript : MonoBehaviour
{

    public static MusicScript instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {

        DontDestroyOnLoad(gameObject);

        /*
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/

    }

}
