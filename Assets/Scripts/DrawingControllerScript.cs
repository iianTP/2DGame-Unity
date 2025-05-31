using UnityEngine;

public class DrawingControllerScript : MonoBehaviour
{

    public Animator gate1animator;
    public Animator gate2animator;
    public Animator gate3animator;

    public GameObject tiles1;
    public GameObject tiles2;
    public GameObject tiles3;

    public Sprite back0;
    public Sprite back1;
    public Sprite back2;
    public Sprite back3;
    public SpriteRenderer backSr;

    public int stage = 0;

    private void Update()
    {
        if (stage == 1) { StartStage1(); }
        if (stage == 2 && !tiles2.activeInHierarchy) { StartStage2(); }
        if (stage == 3 && !tiles3.activeInHierarchy) { StartStage3(); }
    }

    void StartStage1()
    {
        backSr.sprite = back1;
        gate1animator.SetBool("IsColored", true);
        gate2animator.SetBool("IsColored", true);
        gate3animator.SetBool("IsColored", true);
    }
    void StartStage2()
    {
        backSr.sprite = back2;
        tiles2.SetActive(true);
        tiles1.SetActive(false);
    }
    void StartStage3()
    {
        backSr.sprite = back3;
        tiles3.SetActive(true);
        tiles2.SetActive(false);
    }
}
