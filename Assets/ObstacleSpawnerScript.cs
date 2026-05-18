using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{
    public float speed;
    public float spawnTimeDistance;
    public GameObject spikePrefab;
    public GameObject stepPrefab;
    
    private float _timeSinceLastSpawn;
    private readonly List<GameObject> _spawnedObjects = new();

    public void Start()
    {
        this._timeSinceLastSpawn = this.spawnTimeDistance; // don't wait for first obstacle
    }

    public void Update()
    {
        // check delta time
        var deltaTime = Time.deltaTime;
        this._timeSinceLastSpawn += deltaTime;

        foreach (var spawnedObject in this._spawnedObjects.ToArray())
        {
            // move spike
            spawnedObject.transform.Translate(Vector3.left * (this.speed * deltaTime));
            // delete spike if out of map
            if (spawnedObject.transform.position.x < -this.transform.position.x)
            {
                this._spawnedObjects.Remove(spawnedObject);
                Destroy(spawnedObject);
            }
        }
        
        // spawn new spikes if the time has come
        if (this._timeSinceLastSpawn >= this.spawnTimeDistance)
        {
            this.SpawnRandomObstacle();
            this._timeSinceLastSpawn = 0;
        }
    }

    private void SpawnRandomObstacle()
    {
        var prefabType = Random.Range(0, 2) switch
        {
            0 => this.stepPrefab,
            _ => this.spikePrefab
        };
        
        var amount = Random.Range(1, 4);

        this.SpawnGameObject(prefabType, amount, 1);
    }

    private void SpawnGameObject(GameObject prefab, int amount, float space)
    {
        if (prefab is null)
            throw new System.Exception($"GameObject '{nameof(prefab)}' prefab is null!");
        
        var spawnPosition = this.transform.position;
        for (var i = 0; i < amount; i++)
        {
            this._spawnedObjects.Add(Instantiate(prefab, spawnPosition, Quaternion.identity));
            spawnPosition.x += space;
        }
    }
}
