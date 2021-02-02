using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressFiller : MonoBehaviour
{

    public Slider progressSlider;
    public Slider totalProgress;
    public TMP_Text progressValue;
    public TMP_Text t_progressValue;
    public TMP_Text killed_count;
    public TMP_Text TotalCount;
    public TMP_Text remainingCount;
    public GameObject panel;
    private float enemyDestroyedCount = 0f;
    private float enemySpawnCount = 0f;
    private float totalEnemyGenerated;
    
    
    void Start() {
        totalEnemyGenerated = EnemySpawner.total_enemy_count;
        TotalCount.text = totalEnemyGenerated.ToString() ;
        panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        
        if (InitializeBehaviour.canPlay) {
            
            enemySpawnCount = EnemySpawner.spawnEnemyCounter;
            enemyDestroyedCount = EnemySpawner.destroyedEnemyCounter;

             float slider_val= (enemyDestroyedCount / enemySpawnCount) * 100;
             if (!Double.IsNaN(slider_val)) {
                progressSlider.value = slider_val;
                progressValue.text = (progressSlider.value).ToString("0.00") + "%";
             }
             
             float total_val = (enemyDestroyedCount / totalEnemyGenerated ) * 100;
             if (!Double.IsNaN(total_val)) {
                 totalProgress.value = total_val;
                 t_progressValue.text = totalProgress.value.ToString("0.00") +"%";
             }
            
            killed_count.text = enemyDestroyedCount.ToString();

            remainingCount.text = (totalEnemyGenerated - enemySpawnCount).ToString();

        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (InitializeBehaviour.canPlay) {
                ScoreScript.end = DateTime.Now;
                ScoreScript.time = ScoreScript.time+(long)ScoreScript.end.Subtract(ScoreScript.start).TotalSeconds;
                InitializeBehaviour.canPlay = false;
                panel.gameObject.SetActive(true);
            }else{
                ScoreScript.start = DateTime.Now;
                InitializeBehaviour.canPlay = true;
                panel.gameObject.SetActive(false);
            }
        }
    }
}
