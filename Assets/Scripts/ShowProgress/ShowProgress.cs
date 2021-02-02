using System;
using System.Collections.Generic;
using System.Net;
using MongoDB.Bson;
using Skytanet.SimpleDatabase;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowProgress : MonoBehaviour
{
    private int mainMenuScene = 2;
    private SaveFile _saveFile;
    public TMP_Text PlayerName;
    public TMP_Text injury;
    public TMP_Text movementNo;
    public TMP_Text time;
    public TMP_Text treatment;
    public TMP_Text game;
    public GameObject sampleBtn;
    public GameObject sampleGraphBar;
    public Transform contentPanel;
    public Transform barContentPanel;

    private ObjectId currentUserId;
    private ObjectId TreatmentId;
    
    // Start is called before the first frame update
    void Start() {
        
        _saveFile = GameLogin.current_user;
        currentUserId = DBConnect.currentUserID;
        
        //take the data from reprogress collections and the injury collection
        ReProgress reProgress = DBConnect.LoadRecordByIdAndColumn<ReProgress>("reprogress",currentUserId,"PatientID");
        List<AchProgress> arProgress = DBConnect.LoadRecordListByIdAndColumn<AchProgress>("arprogress",currentUserId,"PatientID");
        TreatmentId = reProgress.TreatmentID;
        Injury injuryObject = DBConnect.LoadRecordByIdAndColumn<Injury>("injury", TreatmentId, "Id");
        
        //display the data on scene.
        PlayerName.text = _saveFile.Get<string>("FName")+" "+_saveFile.Get<string>("LName");
        injury.text =_saveFile.Get<string>("Injury");
        movementNo.text = reProgress.ReqProgress[0].Movements.ToString();
        time.text = reProgress.ReqProgress[0].Time+" min";
        treatment.text =injuryObject.TreatmentName;
        game.text =injuryObject.VideoGames[0];
        PopulatePastProgressList(arProgress);
        DrawGraphs(arProgress);
        Debug.Log(arProgress.ToString());
    }

    private void DrawGraphs(List<AchProgress> arProgress) {
        foreach (var progDetail in arProgress) {
            GameObject probarPrototype = Instantiate(sampleGraphBar);
            GraphGenerator generatorScript = probarPrototype.GetComponent<GraphGenerator>();
            ReqProObject rpo = progDetail.Progress[0];
            double movements = rpo.Movements;
            string atime = rpo.Time;
            double reqMovement = Double.Parse(movementNo.text);
            generatorScript.achievedProgressSlider.value =(float)(movements / reqMovement) * 100;
            //generatorScript.requiredProgressSlider.value = (float.Parse(atime) / float.Parse(time.text)) * 100;
            string date_t = progDetail.Date.ToString();
            string[] date_p = date_t.Split('-');
            string[] date_d = date_p[2].Split('T');
            generatorScript.xValueLabel.text = "Day " + date_p[1] +"-"+ date_d[0];
            probarPrototype.transform.SetParent(barContentPanel);
        }

        if (ScoreScript.time!=0) {
            GameObject probarPrototype = Instantiate(sampleGraphBar);
            GraphGenerator generatorScript = probarPrototype.GetComponent<GraphGenerator>();
            double movements = ScoreScript.movements;
            long atime =(ScoreScript.time/60);
            double reqMovement = Double.Parse(movementNo.text);
            generatorScript.achievedProgressSlider.value =(float)(movements / reqMovement) * 100;
            //generatorScript.requiredProgressSlider.value = (atime / float.Parse(time.text)) * 100;
            string date_t = new BsonDateTime(DateTime.Now).ToString();
            string[] date_p = date_t.Split('-');
            string[] date_d = date_p[2].Split('T');
            generatorScript.xValueLabel.text = "Day " + date_p[1] +"-"+ date_d[0];
            probarPrototype.transform.SetParent(barContentPanel);
        }
    }

    private void PopulatePastProgressList(List<AchProgress> list) {
        foreach (var progDetail in list) {
            GameObject newButton = Instantiate(sampleBtn) as GameObject;
            SampleButtonScript buttonScript = newButton.GetComponent<SampleButtonScript>();
            buttonScript.name.text = "Day : "+progDetail.Date;
            ReqProObject rpo = progDetail.Progress[0];
            buttonScript.progress.text ="Movements : "+ rpo.Movements + " Time duration: " + rpo.Time;
            newButton.transform.SetParent(contentPanel);
        }
        
    }
    

    public void backToMainMenu() {
        if (IsOnline() && !ScoreScript.isAdded) {
            //submit the results to the database.
            AchProgress newRecord = new AchProgress();
            newRecord.PatientID = currentUserId;
            newRecord.TreatmentID = TreatmentId;
            newRecord.Date = new BsonDateTime(DateTime.Now);
            Debug.Log(newRecord.Date);
            newRecord.Time = 2;
            newRecord.Progress =new ReqProObject[1];
            ReqProObject toInsert = new ReqProObject();
            toInsert.Movements = ScoreScript.movements;
            Debug.Log("--------"+toInsert.Movements+" "+ScoreScript.movements);
            toInsert.Time = (ScoreScript.time/60).ToString();
            newRecord.Progress[0] = toInsert;
            DBConnect.InsertRecord("arprogress",newRecord);
            Debug.Log("--------------2---------"+newRecord.Progress[0].Movements);
            ScoreScript.isAdded = true;
        }
        SceneManager.LoadScene(mainMenuScene);
    }

    public bool IsOnline() {
        try {
            using (var client = new WebClient()) {
                using (client.OpenRead("http://google.com/generate_204")) {
                    return true;
                }
            }
        }
        catch (Exception e)
        {
            return false;
        }
    }
}
