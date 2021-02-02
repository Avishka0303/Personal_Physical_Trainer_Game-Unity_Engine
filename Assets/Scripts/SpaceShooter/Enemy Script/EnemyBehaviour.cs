using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{

    public float speed = 5f;

    public bool can_shoot;
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
        if(can_shoot)
            Invoke("StartShooting",Random.Range(1f,3f));
    }

    // Update is called once per frame
    void Update() {
        Move();
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

    void StartShooting() {
        GameObject bullet = Instantiate(bullet_prefab, attack_point.position, Quaternion.identity);
        bullet.GetComponent<PlayerBullet>().is_EnemyBullet = true;
        
        if(can_shoot)
            Invoke("StartShooting",Random.Range(1f,3f));
    }

    private void OnTriggerEnter2D(Collider2D target) {
        if (target.tag == "Bullet")
        {
            can_move = false;
            if (can_shoot) {
                can_shoot = false;
                CancelInvoke("StartShooting");
            }
            Invoke("TurnOffGameObject" , 1.5f);
            anim.Play("Destroy");
            explosion_sound.Play();
            EnemySpawner.destroyedEnemyCounter++;
        }
    }

    void TurnOffGameObject() {
        gameObject.SetActive(false);
    }
}
