using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherArrow : MonoBehaviour {

    [Range(20.0f, 75.0f)] [SerializeField] private float LaunchAngle;

    public bool enemyArrow { get; set; }
    public int damage { get; set; }

    public Transform target { get; set; }

    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Launch();
    }

    private void Update() {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        transform.right = rigid.velocity * 5 - position;
    }

    void Launch() {
        float R = Mathf.Abs(transform.position.x - target.position.x);
        float G = Physics2D.gravity.y;
        float tanAlpha = Mathf.Tan(LaunchAngle * Mathf.Deg2Rad);
        float H = target.position.y - transform.position.y;

        float Vx = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
        float Vy = tanAlpha * Vx;

        Vector2 localVelocity = new Vector2(Vx, Vy);
        Vector2 globalVelocity = transform.TransformDirection(localVelocity);
        rigid.velocity = globalVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(enemyArrow && collision.collider.CompareTag("FriendlyUnit")) {
            collision.collider.GetComponent<Unit>().TakeDamage(damage);
        }

        if (!enemyArrow && collision.collider.CompareTag("EnemyUnit")) {
            collision.collider.GetComponent<Unit>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
