using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeMenu : MonoBehaviour
{
    // Start is called before the first frame update
    
    private int gameChooserScene = 3;
    private int ProgressShooter = 4;

    public GameObject panel;

    public void ResumeGame() {
        InitializeBehaviour.canPlay = true;
        panel.gameObject.SetActive(false);
        ScoreScript.start = DateTime.Now;
    }

    public void saveAndExit() {
        ScoreScript.end = DateTime.Now;
        ScoreScript.time = ScoreScript.time+(long)ScoreScript.end.Subtract(ScoreScript.start).TotalSeconds;
        SceneManager.LoadScene(ProgressShooter);
    }
}
