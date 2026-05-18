using System.Collections.Generic;
using UnityEngine;

namespace Obstacles.Scripts
{
    public class SpikesScript : MonoBehaviour
    {
        public float speed;
        public float spawnTimeDistance;
        public GameObject spikePrefab;
        public Transform spawnerPosition;
        
        private float _timeSinceLastSpawn;
        private readonly List<GameObject> _spawnedObjects = new();
    
        public void Start()
        {
            this._timeSinceLastSpawn = this.spawnTimeDistance;
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
                if (spawnedObject.transform.position.x < -this.spawnerPosition.position.x)
                {
                    this._spawnedObjects.Remove(spawnedObject);
                    Destroy(spawnedObject);
                }
            }
            
            // spawn new spikes if the time has come
            if (this._timeSinceLastSpawn >= this.spawnTimeDistance)
            {
                this.SpawnRandomAmountOfSpikes();
                this._timeSinceLastSpawn = 0;
            }
        }

        private void SpawnSingleSpike(Vector3 position)
        {
            if (this.spikePrefab is null)
                throw new System.Exception("Spike prefab is null!");
            
            this._spawnedObjects.Add(Instantiate(this.spikePrefab, position, Quaternion.identity));
        }

        private void SpawnSpecificAmountOfSpikes(byte amount)
        {
            var spawnPosition = this.transform.position;
            for (var i = 0; i < amount; i++)
            {
                this.SpawnSingleSpike(spawnPosition);
                spawnPosition.x++;
            }
        }

        private void SpawnRandomAmountOfSpikes()
        {
            var amountOfSpikes = (byte) Random.Range(1, 3);
            this.SpawnSpecificAmountOfSpikes(amountOfSpikes);
        }
    }
}
