using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float MoveAmount = 1f;

    float _horizontalInput;

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        if (_horizontalInput != 0)
        {
            if (_horizontalInput > 0)
                transform.Translate((Vector2.right + Vector2.up) * MoveAmount);
            else
                transform.Translate((Vector2.left + Vector2.up) * MoveAmount);

        }
    }
}
