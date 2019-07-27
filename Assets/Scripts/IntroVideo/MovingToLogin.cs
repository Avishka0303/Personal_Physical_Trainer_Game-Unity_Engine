using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MovingToLogin : MonoBehaviour {

    public VideoPlayer introVideoPlayer;
    private int nextSceneIndex;
    
    void Start() {
        introVideoPlayer.loopPointReached += CheckOver;
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp) {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
