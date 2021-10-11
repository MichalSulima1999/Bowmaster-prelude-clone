using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class Arrow : MonoBehaviour
{
    public ArrowSO arrowSO;

    public float speed { get; set; }

    [Header("Arrow")]
    private bool active = true;

    private Rigidbody2D rbody;

    private void Start() {
        rbody = GetComponent<Rigidbody2D>();
        Fly();

        Destroy(gameObject, 6f);
    }

    private void LateUpdate() {
        if (active) {
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            transform.right = rbody.velocity * arrowSO.speedMultiplier - position;
        }
    }

    void Fly() {
        rbody.velocity = transform.right * speed * arrowSO.speedMultiplier;
    }

    public void HitGround() {
        rbody.velocity = Vector2.zero;
        rbody.constraints = RigidbodyConstraints2D.FreezeAll;
        rbody.isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        active = false;

        Destroy(gameObject, 3f);
    }
}
