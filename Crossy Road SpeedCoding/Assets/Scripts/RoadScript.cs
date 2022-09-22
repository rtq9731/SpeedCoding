using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoadType
{
    Road,
    Train,
    River
}

public class RoadScript : MonoBehaviour
{
    [SerializeField] Transform spawnPos = null;
    [SerializeField] Transform despawnPos = null;
    
    [Header("瞒 包访 可记")]
    [SerializeField] float carSpawnTimeMax = 3f;
    [SerializeField] float carSpawnTimeMin = 2f;

    [Header("扁瞒 包访 可记")]
    [SerializeField] float trainSpawnTime = 5f;

    [Header("烹唱公 包访 可记")]
    [SerializeField] float boatSpawnTimeMax = 3f;
    [SerializeField] float boatSpawnTimeMin = 2f;

    float timer = 0f;
    float spawnTime = 0f;

    bool _isSpawn = true;

    int _dir = 0;

    RoadType _type = RoadType.Road;

    public void InitRoad(RoadType type)
    {
        int randNumber = Random.Range(0, 5);
        gameObject.tag = "Road";
        if (randNumber > 3)
        {
            _isSpawn = false;
            GetComponent<MeshRenderer>().material.color = Color.white;
            ShuffleStartAndEnd();
            return;
        }

        _isSpawn = true;

        _type = type;
        switch (type)
        {
            case RoadType.Road:
                GetComponent<MeshRenderer>().material.color = Color.black;
                spawnTime = Random.Range(carSpawnTimeMin, carSpawnTimeMax);
                break;
            case RoadType.Train:
                GetComponent<MeshRenderer>().material.color = Color.gray;
                spawnTime = trainSpawnTime;
                break;
            case RoadType.River:
                
                Vector3 pos = transform.position;
                pos.y = -0.1f;

                transform.position = pos;

                gameObject.tag = "Car";
                GetComponent<MeshRenderer>().material.color = Color.blue;
                spawnTime = Random.Range(boatSpawnTimeMin, boatSpawnTimeMax);
                break;
            default:
                break;
        }
        ShuffleStartAndEnd();
    }

    public void ShuffleStartAndEnd()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                Transform temp = spawnPos;
                spawnPos = despawnPos;
                despawnPos = temp;
                break;
            case 1:
                break;
            default:
                break;
        }
        _dir = (int)(despawnPos.position - spawnPos.position).normalized.x;
    }

    private void Update()
    {
        if (PlayerMove.state >= GameState.Stay)
        {
            if (_isSpawn)
            {
                timer += Time.deltaTime;

                if (timer >= spawnTime)
                {
                    SpawnObstacle();
                }
            }
        }
    }

    private void SpawnObstacle()
    {
        switch (_type)
        {
            case RoadType.Road:
                spawnTime = Random.Range(carSpawnTimeMin, carSpawnTimeMax);
                timer = 0f;
                GenericPoolManager.GetPool<CarMove>("Car").GetPoolObject().InitObstacle(spawnPos, despawnPos, _dir);
                break;
            case RoadType.Train:
                timer = 0f;
                GenericPoolManager.GetPool<TrainMove>("Train").GetPoolObject().InitTrain(spawnPos, despawnPos, _dir);
                break;
            case RoadType.River:
                spawnTime = Random.Range(boatSpawnTimeMin, boatSpawnTimeMax);
                timer = 0f;
                GenericPoolManager.GetPool<BoatMove>("Boat").GetPoolObject().InitBoat(spawnPos, despawnPos, _dir);
                break;
            default:
                break;
        }
    }

}
