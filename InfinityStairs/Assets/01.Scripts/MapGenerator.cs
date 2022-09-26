using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] Transform player = null;

    [SerializeField] MapBlock[] blocks = null;
 
    float _offSetX = 0f;
    float _offSetY = 0f;

    private void Update()
    {
        if(player.position.y >= _offSetY)
        {

        }
    }

    private void GenerateMoreMap()
    {

    }

}
