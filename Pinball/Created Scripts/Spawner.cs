using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> ballPrefabs = new List<GameObject>();
    public List<GameObject> balls = new List<GameObject>();
    public SceneData sceneData;
    public bool spawnAll = false;
    public int ballCount = 5;
    public float spawnDelay = 5f;
    public float spawnCounter = 4f;
    public float spawnVelocityMultiplier = 1f;

    void Awake()
    {
        sceneData = FindObjectOfType<SceneData>();
        if (spawnAll) ballCount = sceneData.CurrentPinballs.Count;
        SetUpBalls();
    }

    void Update()
    {
        spawnCounter += Time.deltaTime;
        if (spawnCounter > spawnDelay)
        {
            spawnCounter -= spawnDelay;
            SpawnBall();
        }
    }

    //OLD
    void SetUpBallsRandom()
    {
        for (int i = 0; i < ballCount; i++)
        {
            balls.Add(Instantiate(ballPrefabs[Random.Range(0, ballPrefabs.Count)]));
            balls[i].SetActive(false);
        }
    }

    void SetUpBalls()
    {
        for (int i = 0; i < ballCount; i++)
        {
            balls.Add(Instantiate(ballPrefabs[Random.Range(0, ballPrefabs.Count)]));
            balls[i].GetComponent<Pinball>().pinballData = sceneData.CurrentPinballs[i];
            balls[i].SetActive(false);
        }
    }

    public bool SpawnBall()
    {
        foreach(GameObject ball in balls)
        {
            if(!ball.activeSelf)
            {
                ball.SetActive(true);
                ball.transform.position = transform.position;
                ball.GetComponent<Rigidbody>().velocity = GetSpawnVelocity();
                return true;
            }
        }
        return false;
    }

    Vector3 GetSpawnVelocity()
    {
        Vector3 spawnVelocity = new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),0);
        spawnVelocity *= spawnVelocityMultiplier;
        return spawnVelocity;
    }
}
