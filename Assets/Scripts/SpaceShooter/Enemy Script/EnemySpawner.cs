using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour {

    public float min_Y = -4.3f, max_Y = 4.3f;
    public GameObject[] EnemyPrefab;
    public float enemy_generate_timer = 2f;

    public static float spawnEnemyCounter=0f;
    public static float total_enemy_count = 300;
    public static float destroyedEnemyCounter = 0f;

    public static double angle_degree = 0;
    
    
    // Start is called before the first frame update
    void Start() {
        Invoke("SpawnEnemies",enemy_generate_timer);
    }

    void SpawnEnemies()
    {
        if (InitializeBehaviour.canPlay)
        {

            double angle_radian = angle_degree * (Math.PI / 180);
            double yValue = Math.Sin(angle_radian)*4.3f;
            Vector3 current_position = transform.position;
            current_position.y = (float)yValue;
            angle_degree=angle_degree+60;
            if (angle_degree == 360) {
                ScoreScript.movements++;
                angle_degree = 60;
            }

            Instantiate(EnemyPrefab[Random.Range(0, EnemyPrefab.Length)]
                ,current_position
                ,Quaternion.identity);

            spawnEnemyCounter++;

        }
        Invoke("SpawnEnemies",enemy_generate_timer);
    }
}
