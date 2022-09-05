using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    [SerializeField] float carSpeed;

    float plusRandSpeed = 0f;

    int _dir = 0;

    Transform _endPoint = null;

    private void Start()
    {
        PlayerMove.onGameStay += () => gameObject.SetActive(false);
        plusRandSpeed = Random.Range(0f, 2f);
    }
    
    public void InitCar(Transform startPos, Transform endPoint, int dir)
    {
        gameObject.SetActive(true);
        transform.position = startPos.position;
        _endPoint = endPoint;
        _dir = dir;
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, _endPoint.position) <= 0.1f)
        {
            gameObject.SetActive(false);
        }
        transform.Translate(Vector3.right * _dir * (carSpeed + plusRandSpeed) * Time.deltaTime);
    }
}
