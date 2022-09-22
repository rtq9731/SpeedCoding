using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMove : MonoBehaviour
{
    [SerializeField] float _speed;

    Transform _player = null;

    float _plusRandSpeed = 0f;

    int _dir = 0;

    Transform _endPoint = null;

    private void Start()
    {
        PlayerMove.onGameStay += () =>
        {
            if (_player != null)
                _player.SetParent(null);

            gameObject.SetActive(false);
        };

        _plusRandSpeed = Random.Range(0f, 3f);
    }

    private void OnDisable()
    {
    }

    public void InitBoat(Transform startPos, Transform endPoint, int dir)
    {
        gameObject.SetActive(true);
        Vector3 pos = startPos.position;
        pos.y = 0;
        transform.position = pos;
        _endPoint = endPoint;
        _dir = dir;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _endPoint.position) <= 0.1f)
        {
            gameObject.SetActive(false);
        }
        transform.Translate(Vector3.right * _dir * (_speed + _plusRandSpeed) * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            _player = collision.transform;
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
