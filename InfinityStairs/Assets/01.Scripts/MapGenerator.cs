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
        if(player.position.y >= _offSetY - 10)
        {
            GenerateMoreMap();
        }
    }

    private void GenerateMoreMap()
    {
        MapBlock block = Instantiate<MapBlock>(blocks[Random.Range(0, blocks.Length)]);

        _offSetX += block._sizeInfo._xSize;
        _offSetY += block._sizeInfo._ySize;

        block.SetMapBlock(new Vector2(_offSetX, _offSetY));
    }

}
