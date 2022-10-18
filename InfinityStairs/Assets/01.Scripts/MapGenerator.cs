using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] Transform player = null;

    [SerializeField] MapBlock[] blocks = null;
 
    [SerializeField] float _offSetX = 0;
    [SerializeField] float _offSetY = 1f;

    private void Update()
    {
        if(player.position.y >= _offSetY - 20)
        {
            GenerateMoreMap();
        }
    }

    private void GenerateMoreMap()
    {
        MapBlock block = Instantiate<MapBlock>(blocks[Random.Range(0, blocks.Length)]);

        block.SetMapBlock(new Vector2(_offSetX, _offSetY));

        _offSetX += block._sizeInfo._xSize;
        _offSetY += block._sizeInfo._ySize;
    }

}
