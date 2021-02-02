using System;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {
    
    public float speed = 5f;
    public float deactivate_timer = 3f;
    public bool is_EnemyBullet = false;
    
    // Start is called before the first frame update
    void Start(){
        if (is_EnemyBullet)
            speed *= -1f;
        
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
    }

    void DeactivateGameObject() {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy Bullet" || other.tag == "Enemy" || other.tag=="Bullet") {
            other.tag = "Destroyed";
            gameObject.SetActive(false);
        }
    }
}
