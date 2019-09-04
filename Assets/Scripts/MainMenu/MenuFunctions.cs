using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour {
    
    Dictionary<string,int> scene_dict=new Dictionary<string, int>();
    
    void Start() {
        scene_dict.Add("game_chooser",3);
        scene_dict.Add("past_progress",4);
        scene_dict.Add("options",5);
    }

    public void PlayGame() {
        SceneManager.LoadScene(scene_dict["game_chooser"]);
    }

    public void PastProgress() {
        SceneManager.LoadScene(scene_dict["past_progress"]);
    }

    public void Options() {
        SceneManager.LoadScene(scene_dict["options"]);
    }

    public void Exit() {
        Application.Quit();
    }
}
