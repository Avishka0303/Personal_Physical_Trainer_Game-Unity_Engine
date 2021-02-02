using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour {
    
    private int MainMenuScene = 2;

    public void saveAndExit(){
        SceneManager.LoadScene(2);
    }
}
