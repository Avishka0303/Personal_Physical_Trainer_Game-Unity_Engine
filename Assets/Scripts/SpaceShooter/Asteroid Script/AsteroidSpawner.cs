using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

    public float min_Y = -4.3f, max_Y = 4.3f;
    public GameObject[] AsteroidPrefab;
    public float asteroid_generate_timer = 4f;
    
    
    // Start is called before the first frame update
    void Start() {
        Invoke("SpawnAsteroids",asteroid_generate_timer);
    }

    void SpawnAsteroids()
    {
        if (InitializeBehaviour.canPlay)
        {
            float pos_Y = Random.Range(min_Y, max_Y);
            Vector3 current_position = transform.position;
            current_position.y = pos_Y;

            Instantiate(AsteroidPrefab[Random.Range(0, AsteroidPrefab.Length)]
                ,current_position
                ,Quaternion.identity);
        }
        
        Invoke("SpawnAsteroids",asteroid_generate_timer);
    }
}

