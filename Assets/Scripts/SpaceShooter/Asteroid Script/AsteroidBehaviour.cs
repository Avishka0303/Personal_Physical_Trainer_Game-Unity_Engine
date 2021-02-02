using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidBehaviour : MonoBehaviour {

    public float speed = 5f;
    public float rotate_speed = 5f;

    public bool can_rotate;

    private bool can_move = true;

    public Transform attack_point;
    public GameObject bullet_prefab;

    public float bound_x = -11f;

    private Animator anim;
    private AudioSource explosion_sound;

    private void Awake() {
        anim = GetComponent<Animator>();
        explosion_sound = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start() {
        if (can_rotate) {
            if ( Random.Range(0f,2f) > 0 ) {
                rotate_speed = Random.Range(rotate_speed, rotate_speed + 20f);
                rotate_speed *= -1f;
            }
        }else {
            rotate_speed = Random.Range(rotate_speed, rotate_speed + 20f);
        }
    }

    // Update is called once per frame
    void Update() {
        Move();
        RotateEnemy();
    }

    void Move() {
        if (can_move) {
            Vector3 current_position = transform.position;
            current_position.x -= speed * Time.deltaTime;
            transform.position = current_position;

            if (current_position.x < bound_x){
                gameObject.SetActive(false);
            }
        }
    }

    void RotateEnemy() {
        if (can_rotate){
            transform.Rotate(new Vector3(0f,0f,rotate_speed* Time.deltaTime),Space.World);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D target) {
        if (target.tag == "Bullet") {
            can_move = false;
            Invoke("TurnOffGameObject" , 1.5f);
            anim.Play("Destroy");
            explosion_sound.Play();
        }
    }

    void TurnOffGameObject() {
        gameObject.SetActive(false);
    }
}