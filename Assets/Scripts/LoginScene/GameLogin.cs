using System.Threading;
using MongoDB.Driver;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Skytanet.SimpleDatabase;

public class GameLogin : MonoBehaviour {

    public TMP_InputField username;
    public TMP_InputField password;
    public Toggle rememeberMe;
    public Button loginBtn;
    public TMP_Text wrongAttempt;
    public Slider slider;

    private string usernameTxt;
    private string passwordTxt;
    private bool isRemember;

    private int mainMenuSceneIndex;
    public static SaveFile current_user { get; set; }

    // Start is called before the first frame update
    void Start() {
        mainMenuSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        slider.gameObject.SetActive(false);
    }

    public void LoginToMainMenu() {
        
        usernameTxt = username.text;
        passwordTxt = password.text;
        isRemember = rememeberMe.isOn;
        
        string[] saveFiles = SaveFile.GetSaveFileList();
        DeleteAllSavedFiles(saveFiles);

        if (IsAuthorized()) {
            SceneManager.LoadScene(mainMenuSceneIndex);
        }else {
            wrongAttempt.text = "Username or Password is incorrect.Retry again.";
            username.text = "";
            password.text = "";
        }
    }

    public bool IsAuthorized()
    {
        slider.gameObject.SetActive(true);
        //set slider progress to 10;
        slider.value = 10;
        
        //check the login details in the local files.
        string[] saveFiles = SaveFile.GetSaveFileList();
        slider.value = 20;
        Thread.Sleep(200);
        
        if (saveFiles.Length != 0) {
            foreach (string file_name in saveFiles) {
                SaveFile saveFile = new SaveFile(file_name);
                string name = saveFile.Get<string>("Username");
                string password = saveFile.Get<string>("Password");
                if (name.Equals(usernameTxt) && password.Equals(passwordTxt)) {
                    slider.value = 100;
                    current_user = saveFile;
                    return true;
                }
            }
        }
        
        DBConnect.createConnection();
        slider.value = 50;
        Debug.Log(usernameTxt + "      "+passwordTxt );
        if (DBConnect.IsAuthorized(usernameTxt, passwordTxt)) {   
            slider.value = 70;
            SaveFile saveFile= new SaveFile("pdata"+saveFiles.Length);
            saveFile.Set("Username",usernameTxt);
            saveFile.Set("Password",passwordTxt);
            saveFile.Set("UserId", DBConnect.currentUserID);
            saveFile.Set("Injury",DBConnect.current_patient.Injury);
            saveFile.Set("FName",DBConnect.current_patient.Fname);
            saveFile.Set("LName",DBConnect.current_patient.Lname);
            current_user = saveFile;
            return true;
        }
        slider.value = 100;
        slider.gameObject.SetActive(false);
        return false;
    }

    public void DeleteAllSavedFiles(string[] filenames)
    {
        foreach (var filename in filenames) {
            SaveFile.DeleteSaveFile(filename);
        }
    }
}
