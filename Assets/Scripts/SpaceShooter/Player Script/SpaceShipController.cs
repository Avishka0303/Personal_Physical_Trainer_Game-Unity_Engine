using System;
using System.IO;
using UnityEngine;
using System.IO.Ports;
using TMPro;

public class SpaceShipController : MonoBehaviour {
    
    
    public float max_angle;
    public float min_angle;
    public float player_speed;
    public AudioClip shoot;
    public bool auto_attack;
    public bool autoSerial;
    public TMP_Text warning_text;

    private float Y_max = 4.3f;
    private float Y_min = -4.3f;
    public float minYaw;
    public float maxYaw;
    private string serialData="";
    
    private static SerialPort serial_port;

    [SerializeField]
    private GameObject player_bullet;
    
    [SerializeField]
    private Transform attack_point;
    
    //manage the attack timer.
    public float attack_timer = 0.35f;
    private float current_attack_timer;
    private bool can_attack;

    private AudioSource laser_audio;

    private void Awake() {
        laser_audio = GetComponent<AudioSource>();
    }
    
    // Start is called before the first frame update
    void Start() {
        
        current_attack_timer = attack_timer;
        ScoreScript.start = DateTime.Now;
    }

    // Update is called once per frame
    void Update() {
        if(autoSerial && InitializeBehaviour.canPlay) {
            MoveOnAutoSerial();
        }else if(InitializeBehaviour.canPlay){
            MoveOnKey(); 
        }
        if (InitializeBehaviour.canPlay) {
            Attack();
        }
    }
    
    void MoveOnKey() {
        if (Input.GetAxisRaw("Vertical") > 0f) {
            Vector3 temp = transform.position;
            temp.y += player_speed * Time.deltaTime;
            if(temp.y < Y_max)
                transform.position = temp;
        }else if (Input.GetAxisRaw("Vertical") < 0f) {
            Vector3 temp = transform.position;
            temp.y -= player_speed * Time.deltaTime;
            if(temp.y>Y_min)
                transform.position = temp;
        }
    }

    float YPosition_Threshold(float angle) {
        float y_position = (Y_max-Y_min)/(max_angle-min_angle)*(angle-min_angle)-Y_max;
        return y_position;
    }

    private void OnApplicationQuit() {
        
    }

    void Attack() {
        attack_timer += Time.deltaTime;
        if (attack_timer > current_attack_timer) {
            can_attack = true;
        }

        if (Input.GetKeyDown(KeyCode.K) || auto_attack) {
            if (can_attack) {
                can_attack = false;
                attack_timer = 0f;
                Instantiate(player_bullet, attack_point.position, Quaternion.identity);
                laser_audio.clip = shoot;
                laser_audio.Play();
            }
        }
    }

    void MoveOnAutoSerial() {

        try {
            string[] splitted_msg = serialData.Split(',');
            if (splitted_msg[0].Equals("001")) {
                float y_position = YPosition_Threshold(float.Parse(splitted_msg[2]));
                transform.position = new Vector3(-7.5f, y_position);
                
                float yaw = float.Parse(splitted_msg[3]);
            
                if (yaw>maxYaw || yaw <minYaw) {
                    warning_text.text = "Wrong movement X ";
                }else {
                    warning_text.text = "Correct Movement.Good";
                }
                
            }
        }
        catch (Exception e) {
            Debug.Log("Error when splitting.");
            Debug.Log(e.Message);
        }
    }

    void OnMessageArrived(string msg) {
        Debug.Log(msg);
        serialData = msg;
     }
    
     void OnConnectionEvent(bool success) {
            Debug.Log(success);
     }
}
