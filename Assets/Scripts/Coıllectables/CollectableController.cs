
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    [SerializeField] List<GameObject> Collectables=new List<GameObject>();
    [SerializeField] float SpawnRate;
    [SerializeField] private float timeElapsed = 0f;
    private bool isTimerRunning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= SpawnRate)
            {
                isTimerRunning = false;
                timeElapsed = 0f;

                OnTimerCompleted();
            }
        }
    }
    public void OnTimerCompleted()
    {
        StopTimer();
        ResetTimer();
        Spawn();
        StartTimer();
    }

    private void Spawn()
    {
        Vector3 SpawnPos = MapBorderChecker.GetRandomPointInMap();
        Instantiate(Collectables[Random.Range(0,Collectables.Count)], SpawnPos, Quaternion.identity, null);
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        timeElapsed = 0f;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        timeElapsed = 0f;
    }

    public void ResetTimer()
    {
        timeElapsed = 0f;
    }
}
