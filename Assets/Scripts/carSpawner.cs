using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carSpawner : MonoBehaviour
{
    public List<GameObject> CarPrefabs;
    public float minTime, maxTime;

    public float timer;
    public float spawnTime;

    private void Start()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime)
        {
            timer = 0;
            var car = CarPrefabs[Random.Range(0, CarPrefabs.Count)];

            var spawnedCar = Instantiate(car, transform.position, transform.rotation, transform);

            spawnedCar.AddComponent<carController>();

            Destroy(spawnedCar.gameObject, 6f);
            

            spawnTime = Random.Range(minTime, maxTime);
        }
        
    }
}
