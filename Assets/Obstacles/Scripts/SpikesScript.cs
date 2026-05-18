using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Obstacles.Scripts
{
    public class SpikesScript : MonoBehaviour
    {
        [SerializeField] public float speed;
        [SerializeField] public float spawnTimeDistance;
        
        private Object _spikePrefab;
        private float _timeSinceLastSpawn;
        private readonly List<Object> _spawnedObjects = new();
    
        public void Start()
        {
            this._spikePrefab = UnityEngine.Resources.Load("Spike");
        }
    
        public void Update()
        {
            var deltaTime = Time.deltaTime;
            this._timeSinceLastSpawn += deltaTime;
            
            foreach (var spawnedObject in this._spawnedObjects)
                spawnedObject.GameObject().transform.Translate(Vector3.left * (this.speed * deltaTime));

            if (this._timeSinceLastSpawn < this.spawnTimeDistance) return;
            
            this.SpawnRandomAmountOfSpikes();
            this._timeSinceLastSpawn = 0;
        }

        private void SpawnSingleSpike(Vector3 position)
        {
            if (this._spikePrefab is null)
                throw new System.Exception("Spike prefab is null!");
            
            this._spawnedObjects.Add(Instantiate(this._spikePrefab, position, Quaternion.identity));
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
