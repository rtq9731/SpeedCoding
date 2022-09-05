using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScript : MonoBehaviour
{
    [SerializeField] Transform spawnPos = null;
    [SerializeField] Transform despawnPos = null;
    [SerializeField] float carSpawnTimeMax = 3f;
    [SerializeField] float carSpawnTimeMin = 2f;

    float timer = 0f;
    float randSpawnTime = 0f;

    bool _isSpawn = true;

    int _dir = 0;

    private void Start()
    {
        randSpawnTime = Random.Range(carSpawnTimeMin, carSpawnTimeMax);
        InitRoad();
    }

    public void InitRoad()
    {
        int randNumber = Random.Range(0, 5);
        if (randNumber > 3)
        {
            _isSpawn = false;
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
        else
        {
            _isSpawn = true;
            GetComponent<MeshRenderer>().material.color = Color.black;
        }
        ShuffleStartAndEnd();
    }

    private void Update()
    {
        if(PlayerMove.state >= GameState.Stay && _isSpawn)
        {
            timer += Time.deltaTime;
            if (timer >= randSpawnTime && _isSpawn)
            {
                SpawnCar();
            }
        }
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

    private void SpawnCar()
    {
        randSpawnTime = Random.Range(carSpawnTimeMin, carSpawnTimeMax);
        timer = 0f;
        GenericPoolManager.GetPool<CarMove>("Car").GetPoolObject().InitCar(spawnPos, despawnPos, _dir);
    }
}
