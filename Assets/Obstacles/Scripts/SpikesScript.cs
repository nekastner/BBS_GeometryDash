using TMPro;
using UnityEngine;

namespace Obstacles.Scripts
{
    public class SpikesScript : MonoBehaviour
    {
        public uint spikesAmount = 1;
        private Object _spikePrefab;
    
        public void Start()
        {
            // load prefabs
            _spikePrefab = UnityEngine.Resources.Load("Spike");
            
            // spawn spikes
            var spawnPosition = transform.position;
            for (var i = 0; i < spikesAmount; i++)
            {
                SpawnSpike(spawnPosition);
                spawnPosition.x++;
            }
        }
    
        public void Update()
        {
            // TODO: move spikes
        }

        private void SpawnSpike(Vector3 position)
        {
            if (_spikePrefab is null) return;
            Instantiate(_spikePrefab, position, Quaternion.identity);
        }
    }
}
