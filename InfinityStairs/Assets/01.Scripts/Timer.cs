using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] PlayerMove player = null;

    [SerializeField] Image timerImage;

    [SerializeField] float timerPlusAmount;

    [SerializeField] float timerMinusAmount;

    float timer = 0f;
    float maxTime = 10f;

    GameState state = GameState.Ready;

    enum GameState
    {
        Ready,
        GoOn
    }

    private void Start()
    {
        timer = maxTime;
    }

    public void GameStart()
    {
        state = GameState.GoOn;
    }

    private void Update()
    {
        if(state == GameState.GoOn)
        {
            timer -= timerMinusAmount * Time.deltaTime;
            timerMinusAmount += Time.deltaTime / 10;
            RefreshTimer();
            if (timer <= 0)
            {
                player.GameOver();
            }
        }
    }

    public void PlusTime()
    {
        timer += timerPlusAmount;
        if (timer >= maxTime)
            timer = maxTime;
    }

    public void RefreshTimer()
    {
        timerImage.fillAmount = timer / maxTime;
    }
}
