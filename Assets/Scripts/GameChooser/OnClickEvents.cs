using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickEvents : MonoBehaviour {
    
    private int spaceShooterScene = 6;
    private int honeyCollectorScene = 7;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void playSpaceShooter()
    {
        ScoreScript.movements = 0;
        ScoreScript.isAdded = false;
        ScoreScript.time = 0;
        
        SceneManager.LoadScene(spaceShooterScene);
    }

    public void playHoneyCollector()
    {
        SceneManager.LoadScene(honeyCollectorScene);
    }
}
