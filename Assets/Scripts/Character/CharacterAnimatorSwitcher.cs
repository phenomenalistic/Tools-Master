using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorSwitcher : MonoBehaviour
{
    public Animator animator;
    public Animation animation;

    public GameObject leftMover, rightMover, chestMover;

    public void Start()
    {
        animator.SetTrigger(Animations.Idle);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) { animator.SetTrigger(Animations.Idle); }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { animator.SetTrigger(Animations.CalmRunning); }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { animator.SetTrigger(Animations.OnStart); }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { animator.SetTrigger(Animations.LowRunning);; }
        if (Input.GetKeyDown(KeyCode.Space)) { PrintPositions(); }
    }


    public void PrintPositions()
    {
        Debug.Log("LeftMover");
        Debug.Log(leftMover.transform.localPosition);
        Debug.Log(leftMover.transform.localEulerAngles);

        Debug.Log("RightMover");
        Debug.Log(rightMover.transform.localPosition);
        Debug.Log(rightMover.transform.localEulerAngles);

        Debug.Log("ChestMover");
        Debug.Log(chestMover.transform.localPosition);
        Debug.Log(chestMover.transform.localEulerAngles);
    }

}

public static class Animations
{
    public static string
        Idle = "Idle",
        FastRunning = "FastRunning",
        CalmRunning = "CalmRunning",
        DanceBboyHipHop = "DanceBboyHipHop",
        Walking = "Walking",
        Victory = "Victory",
        OnStart = "OnStart",
        LowRunning = "LowRunning";

}
