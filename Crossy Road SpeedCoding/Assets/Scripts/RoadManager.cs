using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoadManager : MonoBehaviour
{
    [SerializeField] List<RoadScript> roads = new List<RoadScript>();
    [SerializeField] List<Vector3> originPos = new List<Vector3>();

    int curRoadNum = 0;

    float nextBlockPosZ = 0f;

    private void Start()
    {
        PlayerMove.onGameStay += ResetAllRoad;
        PlayerMove.onPlayerMove += CheckAndMakeNext;

        for (int i = 0; i < roads.Count; i++)
        {
            originPos.Add(roads[i].transform.position);
        }
        ResetAllRoad();
    }

    private void ResetAllRoad()
    {
        nextBlockPosZ = 0f;
        for (int i = 0; i < roads.Count; i++)
        {
            roads[i].transform.position = originPos[i];
            roads[i].InitRoad((RoadType)Random.Range(0, 3));
        }
    }

    private void CheckAndMakeNext(float playerZ)
    {
        nextBlockPosZ = playerZ + 20f;
        if (!roads.Find(item => item.transform.position.z == nextBlockPosZ))
        {
            curRoadNum++;
            roads[curRoadNum % roads.Count].transform.position = Vector3.forward * nextBlockPosZ; //- Vector3.up * 3f;
            //roads[curRoadNum % roads.Count].transform.DOMoveY(0, 1f);
            roads[curRoadNum % roads.Count].InitRoad((RoadType)Random.Range(0, 3));
        }
    }
}
