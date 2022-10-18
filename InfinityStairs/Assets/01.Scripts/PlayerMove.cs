using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{

    Timer timer = null;

    [SerializeField] Transform playerPlag = null;
    [SerializeField] float MoveAmount = 1f;

    Vector3 lastPos = Vector3.zero;

    int dir = -1;

    bool isFirst = true;

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        if (PlayerPrefs.HasKey("LastPoint"))
        {
            playerPlag.position = StringToVector3(PlayerPrefs.GetString("LastPoint"));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            dir = -dir;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            lastPos = transform.position;
            transform.Translate((Vector2.right * dir + Vector2.up) * MoveAmount);
            CheckBlock();

            if (isFirst)
            {
                FindObjectOfType<Timer>().GameStart();
                isFirst = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            lastPos = transform.position;
            transform.Translate((Vector2.right * dir + Vector2.up) * MoveAmount);
            CheckBlock();

            if (isFirst)
            {
                FindObjectOfType<Timer>().GameStart();
                isFirst = false;
            }
        }
    }

    private void CheckBlock()
    {
        if(!Physics2D.Raycast(transform.position, Vector2.down, 1))
        {
            GameOver();
        }
        else
        {
            timer.PlusTime();
        }
    }

    public void GameOver()
    {
        if(PlayerPrefs.HasKey("LastPoint"))
        {
            if(StringToVector3(PlayerPrefs.GetString("LastPoint")).y <= transform.position.y)
            PlayerPrefs.SetString("LastPoint", lastPos.ToString());
        }
        else
        {
            PlayerPrefs.SetString("LastPoint", lastPos.ToString());
        }

        SceneManager.LoadScene(0);
    }

    private Vector3 StringToVector3(string vectorString)
    {
        vectorString = vectorString.Remove(vectorString.Length - 1, 1);
        vectorString = vectorString.Remove(0, 1);

        Debug.Log(vectorString);


        var split =  vectorString.Split(',');

        return new Vector3(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]));
    }
}
