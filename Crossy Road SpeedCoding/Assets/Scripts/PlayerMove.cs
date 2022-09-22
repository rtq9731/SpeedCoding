using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum GameState
{
    Stay,
    Ready,
    Play
}

public class PlayerMove : MonoBehaviour
{
    [SerializeField] CarMove carPrefab = null;
    [SerializeField] TrainMove trainPrefab = null;
    [SerializeField] BoatMove boatPrefab = null;

    public static event Action<float> onPlayerMove;
    public static event Action onGameStay;

    Coroutine cor = null;

    [SerializeField] float moveDist = 2f;
    [SerializeField] float inputCool = 1f;

    int minCharX = -8;
    int maxCharX = 8;

    public static GameState state = GameState.Stay;

    float transformZ = 0f;
    float inputTimer = 0f;

    private void Awake()
    {
        GenericPoolManager.CratePool<CarMove>("Car", carPrefab, GameObject.Find("CarParent").transform, 10);
        GenericPoolManager.CratePool<TrainMove>("Train", trainPrefab, GameObject.Find("CarParent").transform, 5);
        GenericPoolManager.CratePool<BoatMove>("Boat", boatPrefab, GameObject.Find("CarParent").transform, 10);
    }

    void Update()
    {
        Vector3 vec = Camera.main.transform.localPosition;
        vec.x = 2;
        Camera.main.transform.localPosition = vec;

        switch (state)
        {
            case GameState.Stay:
                break;
            case GameState.Ready:
                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    state = GameState.Play;
                    inputTimer = 0f;

                    transform.DOMoveY(3f, 0.2f).OnComplete(() => transform.DOMoveY(1f, 0.2f));
                    transform.DOMove(transform.position + Vector3.forward * moveDist, 0.4f).OnComplete(() => onPlayerMove?.Invoke(transformZ += 2));
                }
                return;
            case GameState.Play:
                inputTimer += Time.deltaTime;
                if(inputTimer >= inputCool)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        cor = StartCoroutine(Move(Vector3.forward, moveDist, 0.4f, () => onPlayerMove?.Invoke(transformZ += 2)));
                    }
                    if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x - 2f >= minCharX)
                    {
                        cor = StartCoroutine(Move(Vector3.right, -moveDist, 0.4f, () => { }));
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x + 2f <= maxCharX)
                    {
                        cor = StartCoroutine(Move(Vector3.right, moveDist, 0.4f, () => { }));
                    }
                }

                if(transform.position.x < minCharX || transform.position.x > maxCharX)
                {
                    GameOver();
                }
                break;
            default:
                break;
        }
    }

    private IEnumerator Move(Vector3 dir, float moveDist, float moveTime, System.Action onComplete)
    {
        float moveTimer = 0f;
        Vector3 dest = transform.position + moveDist * dir;

        inputTimer = 0f;

        while (moveTime > moveTimer)
        {
            moveTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, dest, moveTimer / moveTime);
            yield return null;
        }
        onComplete();
    }

    public void GameOver()
    {
        StopCoroutine(cor);
        DOTween.CompleteAll();
        transformZ = 0f;
        transform.position = Vector3.up * 3;
        state = GameState.Stay;
        onGameStay?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Car"))
        {
            GameOver();
        }

        if (other.CompareTag("Road") && state == GameState.Stay)
        {
            state = GameState.Ready;
        }
    }
}
