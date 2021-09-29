using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour {
    protected bool moving;
    protected bool attacking;
    protected bool dead;
    protected Animator animator;
    protected Rigidbody2D rbody;
    protected Collider2D unitCollider;

    protected int currentHp;
    private float attackTimeCounter;
    RaycastHit2D hitEnemy;

    private GameObject followedFlag;
    private Flag flagScript;
    private Transform followSpot;

    public bool hasFlag { get; set; } = false;

    [SerializeField] protected int maxHp = 100;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float timeBetweenAttacks = 1f;

    [SerializeField] protected float moveSpeed = 1f;
    [SerializeField] protected float attackRange = 5f;
    [SerializeField] protected float maxDistanceFromFlag = 0.1f;
    [SerializeField] protected bool enemy;
    [SerializeField] protected LayerMask friendlyLayer;
    [SerializeField] protected LayerMask enemyLayer;

    [Header("UI")]
    [SerializeField] private Image hpImage;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();

        unitCollider = GetComponent<Collider2D>();
        currentHp = maxHp;

        if (enemy) {
            followedFlag = GameObject.FindGameObjectWithTag("FriendlyFlag");
            flagScript = followedFlag.GetComponent<Flag>();
        } else {
            followedFlag = GameObject.FindGameObjectWithTag("EnemyFlag");
            flagScript = followedFlag.GetComponent<Flag>();
        }
    }

    // Update is called once per frame
    void Update() {
        SetFollowSpot();

        CheckForEnemy();
        PassVarToAnimator();

        attackTimeCounter -= Time.deltaTime;

        hpImage.fillAmount = (float)currentHp / (float)maxHp;

        LookAtFlag();
    }

    private void SetFollowSpot() {
        if (flagScript.HasCarrier()) {
            followSpot = flagScript.GetFollowSpot();
        } else {
            followSpot = followedFlag.transform;
        }
    }

    public void Move() {
        moving = true;
        attacking = false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right + transform.up) * 0.1f, -Vector2.up, 2f, ~friendlyLayer);
        Debug.DrawRay(transform.position + (transform.right + transform.up) * 0.1f, -Vector2.up, Color.white, 2f);
        if (hit.collider != null) {
            var slope = hit.normal;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        rbody.velocity = transform.right * moveSpeed;
    }

    public void Stay() {
        moving = false;
        attacking = false;
        rbody.velocity = Vector2.zero;
    }

    void CheckForEnemy() {
        hitEnemy = Physics2D.Raycast(transform.position + new Vector3(0, 0.2f, 0), transform.right, attackRange, enemyLayer);

        if (hitEnemy.collider != null) {
            Debug.DrawRay(transform.position + new Vector3(0, 0.2f, 0), transform.right, Color.red, attackRange);
            if (attackTimeCounter <= 0) {
                Attack();
                attackTimeCounter = timeBetweenAttacks;
            } else {
                Stay();
            }
        } else {
            if(Vector3.Distance(transform.position, followSpot.position) > maxDistanceFromFlag)
                Move();
            else
                rbody.velocity = Vector2.zero;
        }
    }

    public void Attack() {
        moving = false;
        attacking = true;

        rbody.velocity = Vector2.zero;
    }

    public void HitMoment() {
        AttackUnit(hitEnemy.collider.gameObject);
    }

    public abstract void AttackUnit(GameObject unit);

    public void Die() {
        moving = false;
        attacking = false;
        dead = true;

        hpImage.fillAmount = (float)currentHp / (float)maxHp;

        animator.Play("Death");

        Destroy(gameObject, 1f);
        gameObject.layer = 0;
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        rbody.velocity = Vector2.zero;
        rbody.isKinematic = true;
    }

    public void PassVarToAnimator() {
        animator.SetBool("Moving", moving);
        animator.SetBool("Dead", dead);
        animator.SetBool("Attacking", attacking);
    }

    public void TakeDamage(int amount) {
        currentHp -= amount;

        if (currentHp <= 0) {
            currentHp = 0;
            Die();
        }
    }

    public void TakeFlag() {
        moveSpeed /= 2;
    }

    public void LookAtFlag() {
        if (hasFlag)
            return;

        if (transform.position.x > followSpot.position.x) {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z));
        } else {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z));
        }

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
