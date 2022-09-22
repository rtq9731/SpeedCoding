using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMove : MonoBehaviour
{
    [SerializeField] float _speed;

    float _plusRandSpeed = 0f;

    int _dir = 0;

    [SerializeField]
    Transform leftLast;
    [SerializeField]
    Transform rightLast;

    Transform _lastPoint = null;
    Transform _endPoint = null;

    private void Start()
    {
        PlayerMove.onGameStay += () => gameObject.SetActive(false);
        _plusRandSpeed = Random.Range(0f, 3f);
    }

    public void InitTrain(Transform startPos, Transform endPoint, int dir)
    {
        gameObject.SetActive(true);
        Vector3 pos = startPos.position + Vector3.right * -dir * 25;
        _endPoint = endPoint;
        switch (dir)
        {
            case -1:
                _lastPoint = rightLast;
                break;
            case 1:
                _lastPoint = leftLast;
                break;
            default:
                break;
        }
        transform.position = pos;
        _dir = dir;
    }

    private void Update()
    {
        if (Vector2.Distance(_lastPoint.position, _endPoint.position) <= 0.1f)
        {
            gameObject.SetActive(false);
        }
        transform.Translate(Vector3.right * _dir * (_speed + _plusRandSpeed) * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
    }
}
