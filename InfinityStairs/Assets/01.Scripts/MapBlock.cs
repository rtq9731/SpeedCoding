using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    public MapBlockSizeInfo _sizeInfo;

    public void SetMapBlock(Vector2 offSet)
    {
        transform.position += (Vector3)offSet;
    }
}

[Serializable]
public class MapBlockSizeInfo
{
    public int _xSize;
    public int _ySize;

    public bool isSizePlus()
    {
        return _xSize >= 0;
    }
}

