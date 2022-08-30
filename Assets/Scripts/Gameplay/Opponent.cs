using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Opponent : MonoBehaviour
{
    public Coroutine moveForward;
    private float moveSpeed = 4f;

    public GameObject leftMover, rightMover, chestMover, toolObject;

    public ToolProperies currentTool;

    public Animator animator;

    public TwoBoneIKConstraint leftWeight, rightWaight, chestWeight;

    public static Opponent current;

    public List<ToolProperies> tools = new List<ToolProperies>() { };

    private void Awake()
    {
        current = this;
        EventManager.onPlayButtonPress.AddListener(OnPlayButtonPress);
        EventManager.onPlayerFinished.AddListener(OnPlayerFineshed);
        EventManager.onOpponentFinished.AddListener(OnOpponetFineshed);
    }

    private void Start()
    {
        animator.SetTrigger(Animations.OnStart);
        SetAICompexyty();
    }

    

    RaycastHit hitPlayer = new RaycastHit(), hitTool = new RaycastHit();
    bool IsPassedPath()
    {
        Vector3 raycastPlayerShift = new Vector3(0, -1f, 0.3f), raycastToolShift = new Vector3(0, -1f, currentTool.raycastShift);
        Physics.Raycast(transform.position + raycastPlayerShift, Vector3.up, out hitPlayer, 10);
        Physics.Raycast(transform.position + raycastToolShift, Vector3.up, out hitTool, 10);
        //Debug.DrawRay(playerGO.transform.position + raycastShift, Vector3.up * 10, Color.yellow);
        
        if (hitPlayer.collider == null || hitTool.collider == null) { return true; }
        else
        {
            if (currentTool.IsSameTag(hitPlayer.collider.tag) || hitPlayer.collider.tag == "Untagged" || currentTool.IsSameTag(hitTool.collider.tag) || hitTool.collider.tag == "Untagged") { return true; }
            else { return false; }
        }
    }

    string GetCurrentTag()
    {
        Vector3 raycastPlayerShift = new Vector3(0, -1f, 0.3f), raycastToolShift = new Vector3(0, -1f, currentTool.raycastShift);
        Physics.Raycast(transform.position + raycastPlayerShift, Vector3.up, out hitPlayer, 10);
        Physics.Raycast(transform.position + raycastToolShift, Vector3.up, out hitTool, 10);
        //Debug.DrawRay(playerGO.transform.position + raycastShift, Vector3.up * 10, Color.yellow);
        return hitTool.collider.tag;
    }

    ToolProperies GetTool(string tag)
    {
        foreach (ToolProperies tp in tools)
        {
            if (tp.IsSameTag(tag)) { return tp; }
        }
        return tools[0];
    }



    public void SetTool(ToolProperies newToolProperies)
    {
        currentTool = newToolProperies;
        SetMoversPositions();
        SetWeights();
        PutToolPrefab();
        if (IsPassedPath())
        {
            SetAnimations();
            StartMoveForward();
        }
        moveSpeed = currentTool.moveSpeed;
    }

    void SetMoversPositions()
    {
        leftMover.transform.localPosition = currentTool.positions[0];
        leftMover.transform.localEulerAngles = currentTool.positions[1];

        rightMover.transform.localPosition = currentTool.positions[2];
        rightMover.transform.localEulerAngles = currentTool.positions[3];

        chestMover.transform.localPosition = currentTool.positions[4];
        chestMover.transform.localEulerAngles = currentTool.positions[5];
    }

    void SetWeights()
    {
        leftWeight.weight = currentTool.leftWeight;
        rightWaight.weight = currentTool.rightWeight;
        chestWeight.weight = currentTool.chestWeight;
    }

    void SetAnimations()
    {
        animator.SetTrigger(currentTool.moveAnimation);
    }

    void PutToolPrefab()
    {
        foreach (Transform children in toolObject.transform)
        {
            Destroy(children.gameObject);
        }
        GameObject go = Instantiate(currentTool.prefab, toolObject.transform);
    }

    public void OnPlayButtonPress()
    {
        TurnOnMoveAnimation();
        StartMoveForward();
    }

    public void OnPlayerFineshed()
    {
        //StopAllCoroutines();
        //TurnOnIdleAnimation();
        int level = GameData.GetInt(GameData.Valuse.level);
        if (level > GameSceneManager.trainingLevelCount) // если это не тренированчный уровень
        {
            GameData.SetInt(GameData.Valuse.gameCount, GameData.GetInt(GameData.Valuse.gameCount) + 1);
            GameData.Save();
        }
    }

    public void OnOpponetFineshed()
    {
        StopAllCoroutines();
        StartCoroutine(MoveToFinishCenter());
    }

    public IEnumerator MoveToFinishCenter()
    {
        Vector3 startPosition = transform.position;

        while (startPosition.z + 1.5f > transform.position.z)
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
            yield return null;
        }
        TurnOnIdleAnimation();
    }

    void TurnOnIdleAnimation()
    {
        animator.SetTrigger(Animations.Idle);
    }

    void TurnOnMoveAnimation()
    {
        animator.SetTrigger(currentTool.moveAnimation);
    }

    void StartMoveForward()
    {
        if (moveForward != null) { StopCoroutine(moveForward); }
        moveForward = StartCoroutine(MoveForward());
    }

    IEnumerator MoveForward()
    {
        while (IsPassedPath())
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;

            yield return null;
        }
        CollisionWithNewObstacle();
    }

    void CollisionWithNewObstacle()
    {
        StartCoroutine(SetToolW());
        
    }

    private float rightChoicePercentage = 95, reactionTime = 0.3f, correctionTime = 0.7f;
    IEnumerator SetToolW()
    {
        yield return new WaitForSeconds(reactionTime + Random.Range(-0.1f, 0.1f));

        if (Random.Range(1, 100 + 1) <= rightChoicePercentage)
        {
            SetRightTool();
        }
        else
        {
            SetNotRightTool();
            yield return new WaitForSeconds(correctionTime + Random.Range(-0.1f, 0.1f));
            SetRightTool();
        }
    }

    void SetRightTool()
    {
        SetTool(GetTool(GetCurrentTag()));
    }

    void SetNotRightTool()
    {
        SetTool(GetNotRightTool(GetCurrentTag()));
    }

    ToolProperies GetNotRightTool(string tag)
    {
        int t;
        for (int i = 0; i < 5; i++)
        {
            t = Random.Range(0, tools.Count);
            if (!tools[t].IsSameTag(tag)) { return tools[t]; }
        }
        return tools[0];
    }

    private void SetAICompexyty()
    {
        if (!GameData.HasInt(GameData.Valuse.aiComplexityCycleLenght))
        {
            GameData.SetInt(GameData.Valuse.aiComplexityCycleLenght, 3);
            GameData.Save();
        }
        int cycle = GameData.GetInt(GameData.Valuse.aiComplexityCycleLenght);

        int level = GameData.GetInt(GameData.Valuse.level);
        if (level <= GameSceneManager.trainingLevelCount) { SetLowComplexity(); } // если это тренированчный уровень
        else
        {
            level = GameData.GetInt(GameData.Valuse.gameCount);
            int AILevel = (level % cycle) + 1;

            if (AILevel == 1) { SetLowComplexity(); }
            else if (AILevel == cycle) { SetHardComplexity(); }
            else { SetMiddleComplexity(); }
        }

        if (level % cycle == 0)
        {
            GameData.SetInt(GameData.Valuse.aiComplexityCycleLenght, Random.Range(3, 5+1));
            GameData.Save();
        }
    }
    private void SetLowComplexity()
    {
        rightChoicePercentage = 40;
        reactionTime = 1f;
        correctionTime = 2f;
    }

    private void SetMiddleComplexity()
    {
        rightChoicePercentage = 80;
        reactionTime = 0.5f;
        correctionTime = 0.7f;
    }

    private void SetHardComplexity()
    {
        rightChoicePercentage = 95;
        reactionTime = 0.05f;
        correctionTime = 0.1f;
    }


}
