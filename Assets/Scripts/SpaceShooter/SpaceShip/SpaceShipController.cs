using System;
using System.IO;
using UnityEngine;
using System.IO.Ports;

public class SpaceShipController : MonoBehaviour {
    
    //inspector properties.
    public string com_port;
    public int baud_rate;
    public int read_timeout;
    public float max_angle;
    public float min_angle;
    public float player_speed;

    private float Y_max = 4.3f;
    private float Y_min = -4.3f;
    
    private static SerialPort serial_port;

    [SerializeField]
    private GameObject player_bullet;
    
    [SerializeField]
    private Transform attack_point;
    
    //manage the attack timer.
    public float attack_timer = 0.35f;
    private float current_attack_timer;
    private bool can_attack;
    
    
    // Start is called before the first frame update
    void Start() {

        /*try {
            serial_port=new SerialPort(com_port,baud_rate);
            if (!serial_port.IsOpen) {
                serial_port.Open();
            }else {
                Debug.Log("Your device is already use by another program.");   
            }
        }catch (IOException e) {
            Debug.Log(e.Message);
        }*/
        //initialize the current attack timer  to the attack timer
        current_attack_timer = attack_timer;
    }

    // Update is called once per frame
    void Update() {
        //MoveOnSerialReading();
        MoveOnKey();
        Attack();

    }

    void MoveOnSerialReading() {
        
        if (!serial_port.IsOpen) {
            serial_port.Open();
        }

        string message = serial_port.ReadLine();
        Debug.Log("get in to read.");
        Debug.Log(message);

        try {
            string[] splitted_msg = message.Split(',');
            
            if (splitted_msg[0].Equals("001")) {
                float y_position = YPosition_Threshold(float.Parse(splitted_msg[2]));
                transform.position = new Vector3(-7.5f, y_position);
            }
        }
        catch (Exception e) {
            Debug.Log("Error when splitting.");
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
        if (serial_port.IsOpen) {
            serial_port.Close();
        }
    }

    void Attack() {
        attack_timer += Time.deltaTime;
        if (attack_timer > current_attack_timer) {
            can_attack = true;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (can_attack) {
                can_attack = false;
                attack_timer = 0f;
                Instantiate(player_bullet, attack_point.position, Quaternion.identity);
            }
        }
    } 
}
