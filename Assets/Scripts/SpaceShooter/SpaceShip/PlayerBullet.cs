using UnityEngine;

public class PlayerBullet : MonoBehaviour {
    
    public float speed = 5f;
    public float deactivate_timer = 3f;
        
    // Start is called before the first frame update
    void Start(){
        Invoke("DeactivateGameObject",deactivate_timer);
    }

    // Update is called once per frame
    void Update() {
        Move();
    }

    void Move() {
        Vector3 temp_position = transform.position;
        temp_position.x += speed * Time.deltaTime;
        transform.position = temp_position;
        //transform.eulerAngles = new Vector3(0,0,90f);
    }

    void DeactivateGameObject() {
        gameObject.SetActive(false);
    }
}
