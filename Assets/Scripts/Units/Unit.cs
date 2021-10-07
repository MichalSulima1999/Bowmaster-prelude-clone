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
    protected CapsuleCollider2D cc;

    protected int currentHp;
    protected float attackTimeCounter;

    private GameObject followedFlag;
    private Flag flagScript;
    protected Transform followSpot;

    public bool onSlope { get; set; }
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
    [SerializeField] protected LayerMask groundLayer;

    [Header("UI")]
    [SerializeField] private Image hpImage;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();

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

        if (!attacking) {
            if (!hasFlag)
                LookAtTarget(followSpot);

            if (hasFlag && enemy)
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z));

            if (hasFlag && !enemy)
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z));
        }
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

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 2f, groundLayer);
        Debug.DrawRay(transform.position, -Vector2.up, Color.white, 2f);


        if (hit.collider != null) {
            Debug.Log("hit");
            var slope = hit.normal;
            var targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 0.1f);
        }



        rbody.velocity = transform.right * moveSpeed;


    }

    public void Stay() {
        moving = false;
        rbody.velocity = Vector2.zero;
    }

    public abstract void CheckForEnemy();

    public void Attack() {
        moving = false;
        attacking = true;

        animator.Play("Attack");
        rbody.velocity = Vector2.zero;
    }

    public abstract void AttackUnit(GameObject unit);

    public abstract void AttackMoment();

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
        //animator.SetBool("Attacking", attacking);
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

    public void PutFlag() {
        moveSpeed *= 2;
    }

    public void LookAtTarget(Transform target) {
        if (transform.position.x > target.position.x) {
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
