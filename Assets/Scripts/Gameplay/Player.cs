using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Player : MonoBehaviour
{
    public Coroutine moveForward, mine, moveForwardOnFinalObstacles, rotateCamera;
    private float moveSpeed = 4f;
    public float moveSpeedMultipier = 1;

    public GameObject leftMover, rightMover, chestMover, toolObject, cameraGO;

    public ToolProperies currentTool, freeHands;

    public Animator animator;

    public TwoBoneIKConstraint leftWeight, rightWaight, chestWeight;

    public static Player current;

    private void Awake()
    {
        current = this;
        EventManager.onPlayButtonPress.AddListener(OnPlayButtonPress);
        EventManager.onPlayerFinished.AddListener(OnPlayerFineshed);
        EventManager.onPlayerComplete.AddListener(PlayerComplete);
        EventManager.onPlayerFailed.AddListener(PlayerFailed);
        EventManager.onPlayerWin.AddListener(PlayerWin);
    }

    private void Start()
    {
        animator.SetTrigger(Animations.OnStart);
    }

    void StartMoveForward()
    {
        if (moveForward != null) { StopCoroutine(moveForward); }
        moveForward = StartCoroutine(MoveForward());
    }

    IEnumerator MoveForward()
    {
        SoundManager.current.Play(SoundManager.current.walk);
        while (IsPassedPath())
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime * moveSpeedMultipier;
            yield return null;
        }
        Vibration.Vibrate(17, true);
        SoundManager.current.Stop(SoundManager.current.walk);
        TurnOnIdleAnimation();
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

    IEnumerator MoveForwardOnFinalObstacles()
    {
        SoundManager.current.Play(SoundManager.current.walk);
        while (IsPassedPath())
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime * moveSpeedMultipier;
            yield return null;
        }
        SoundManager.current.Stop(SoundManager.current.walk);
        EventManager.onPlayerComplete.Invoke();
    }

    public void SetTool(ToolProperies newToolProperies, bool movement = true)
    {
        currentTool = newToolProperies;
        SetMoversPositions();
        SetWeights();
        PutToolPrefab();
        if (IsPassedPath())
        {
            SetAnimations();
            if (movement) { StartMoveForward(); }
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
        StartMoveForward();
    }

    public void OnPlayerFineshed()
    {
        rotateCamera = StartCoroutine(RotateCamera());
        moveSpeedMultipier = 1;
        StopCoroutine(moveForward);
        SoundManager.current.Stop(SoundManager.current.walk);
        moveForwardOnFinalObstacles = StartCoroutine(MoveForwardOnFinalObstacles());
    }

    IEnumerator RotateCamera()
    {
        float pointer = 0, speed = 4;
        Vector3 startPosition = cameraGO.transform.localPosition,
            endPosition = new Vector3(5.11f, 3.14f, -4.3f);
        Quaternion startRotation = cameraGO.transform.localRotation,
            endRotation = Quaternion.Euler(25, -45, 0);
        while (pointer < 1)
        {
            pointer += Time.deltaTime * speed;
            cameraGO.transform.localPosition = Vector3.Lerp(startPosition, endPosition, pointer);
            cameraGO.transform.localRotation = Quaternion.Lerp(startRotation, endRotation, pointer);
            yield return null;
        }
    }

    public IEnumerator MoveToFinishCenter()
    {
        Vector3 startPosition = transform.position;
        SoundManager.current.Play(SoundManager.current.walk);
        while (startPosition.z + 1.5f > transform.position.z)
        {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime * moveSpeedMultipier;
            yield return null;
        }
        SoundManager.current.Stop(SoundManager.current.walk);
        TurnOnIdleAnimation();
    }

    public void PlayerComplete()
    {
        StopCoroutine(moveForwardOnFinalObstacles);
        SoundManager.current.Stop(SoundManager.current.walk);
        TurnOnIdleAnimation();
        cameraGO.transform.parent = null;
        transform.position -= new Vector3(0, 0, 0.5f);
        StartCoroutine(GoToNextLevel());
        Vibration.Vibrate(25, true);
    }

    public void PlayerFailed()
    {
        StopCoroutine(moveForwardOnFinalObstacles);
        SoundManager.current.Stop(SoundManager.current.walk);
        StopCoroutine(rotateCamera);
        StartCoroutine(MoveToFinishCenter());
        StartCoroutine(ReloadLevel());
        StartCoroutine(DelayFailVibro());
    }

    IEnumerator DelayFailVibro()
    {
        yield return new WaitForSeconds(0.5f);
        Vibration.Vibrate(80, true);
    }

    public void PlayerWin()
    {
        StopCoroutine(moveForwardOnFinalObstacles);
        SoundManager.current.Stop(SoundManager.current.walk);
        StartCoroutine(MoveToPodium());
    }

    public IEnumerator MoveToPodium()
    {
        Vector3 curentPos = transform.position, targetPos = FinalPodium.current.PlayerPosition.transform.position;

        transform.position = new Vector3(transform.position.x, targetPos.y, transform.position.z);

        float pointer = 0, speed = 4;

        while (pointer < 1)
        {
            pointer += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(curentPos, targetPos, pointer);
            yield return null;
        }
        SetTool(freeHands, false);
        cameraGO.transform.parent = null;

        pointer = 0;
        speed = 4;
        Quaternion startPlayerRot = transform.localRotation, endPlayerRot = Quaternion.Euler(0, 145, 0);
        Vector3 cameraStartPos = cameraGO.transform.position, cameraEndPos = cameraGO.transform.position + new Vector3(3f, 2.26f, -1.59f);
        while (pointer < 1)
        {
            pointer += Time.deltaTime * speed;
            cameraGO.transform.position = Vector3.Lerp(cameraStartPos, cameraEndPos, pointer);
            transform.localRotation = Quaternion.Lerp(startPlayerRot, endPlayerRot, pointer);
            yield return null;
        }
        animator.SetTrigger("Victory");
        StartCoroutine(GoToNextLevel());
    }

    IEnumerator GoToNextLevel()
    {
        GameData.SetInt(GameData.Valuse.level, GameData.GetInt(GameData.Valuse.level) + 1);
        GameData.Save();
        yield return new WaitForSeconds(2.4f);
        GameSceneManager.LoadNextSceneAsync();
    }

    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(2f);
        GameSceneManager.LoadNextSceneAsync();
    }

    void TurnOnIdleAnimation()
    {
        animator.SetTrigger(Animations.Idle);
    }

    void TurnOnMoveAnimation()
    {
        animator.SetTrigger(currentTool.moveAnimation);
    }

}
