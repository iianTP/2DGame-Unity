using UnityEngine;

public class DrawingControllerScript : MonoBehaviour
{

    public GameObject gate1;
    public GameObject gate2;
    public GameObject gate3;

    public GameObject tiles1;
    public GameObject tiles2;
    public GameObject tiles3;

    public Sprite back0;
    public Sprite back1;
    public Sprite back2;
    public Sprite back3;
    public SpriteRenderer backSr;

    public PlayerScript playerScript;
    public int stage;

    private void Awake()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        stage = playerScript.stage;

        if (stage == 1) { StartStage1(); }
        if (stage == 2 && !tiles2.activeInHierarchy) { StartStage2(); }
        if (stage == 3 && !tiles3.activeInHierarchy) { StartStage3(); }

        gate1.SetActive(true);
        gate2.SetActive(true);
        gate3.SetActive(true);

    }

    //private void Update()
    //{

    //}

    void StartStage1()
    {
        backSr.sprite = back1;
        gate1.GetComponent<Animator>().SetBool("IsColored", true);
        gate1.GetComponent<LevelGateScript>().completed = true;
        playerScript.hasDoubleJump = true;
    }
    void StartStage2()
    {
        StartStage1();
        backSr.sprite = back2;
        gate2.GetComponent<Animator>().SetBool("IsColored", true);
        gate2.GetComponent<LevelGateScript>().completed = true;
        tiles2.SetActive(true);
        tiles1.SetActive(false);
        playerScript.hasDash = true;
    }
    void StartStage3()
    {
        StartStage2();
        backSr.sprite = back3;
        gate3.GetComponent<Animator>().SetBool("IsColored", true);
        gate3.GetComponent<LevelGateScript>().completed = true;
        tiles3.SetActive(true);
        tiles2.SetActive(false);
        playerScript.hasAttack = true;
    }

}
