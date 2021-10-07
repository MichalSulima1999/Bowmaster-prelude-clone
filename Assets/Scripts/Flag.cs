using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FlagType {
    friendlyFlag,
    enemyFlag
}

public class Flag : MonoBehaviour {
    [SerializeField] private FlagType type;

    [SerializeField] private Transform followSpot;

    private GameObject carrier;
    private Unit carrierUnit;
    private Vector3 startingPoint;

    // Start is called before the first frame update
    void Start() {
        startingPoint = transform.position;
    }

    private void Update() {
        if (carrier) {
            transform.position = carrier.transform.position;
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, carrier.transform.rotation.eulerAngles.z));
        }
    }

    private void SetCarrier(Collider2D collision) {
        carrier = collision.gameObject;
        carrierUnit = carrier.GetComponent<Unit>();
        carrierUnit.hasFlag = true;
        carrierUnit.TakeFlag();
    }

    private void RestoreFlag() {
        transform.position = startingPoint;
        carrier = null;
        carrierUnit.hasFlag = false;
        carrierUnit.PutFlag();
    }

    public bool HasCarrier() {
        if (carrier)
            return true;

        return false;
    }

    public Transform GetFollowSpot() {
        return followSpot;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!carrier) {
            if (collision.CompareTag("FriendlyUnit") && !collision.GetComponent<Unit>().hasFlag) {
                if (type == FlagType.friendlyFlag && transform.position != startingPoint) {
                    SetCarrier(collision);
                    carrier.transform.rotation = Quaternion.Euler(new Vector3(carrier.transform.rotation.eulerAngles.x, 180, carrier.transform.rotation.eulerAngles.z));
                } else if (type == FlagType.enemyFlag) {
                    SetCarrier(collision);
                    carrier.transform.rotation = Quaternion.Euler(new Vector3(carrier.transform.rotation.eulerAngles.x, 180, carrier.transform.rotation.eulerAngles.z));
                }
            }

            if (collision.CompareTag("EnemyUnit") && !collision.GetComponent<Unit>().hasFlag) {
                if (type == FlagType.enemyFlag && transform.position != startingPoint) {
                    SetCarrier(collision);
                    carrier.transform.rotation = Quaternion.Euler(new Vector3(carrier.transform.rotation.eulerAngles.x, 0, carrier.transform.rotation.eulerAngles.z));
                } else if (type == FlagType.friendlyFlag) {
                    SetCarrier(collision);
                    carrier.transform.rotation = Quaternion.Euler(new Vector3(carrier.transform.rotation.eulerAngles.x, 0, carrier.transform.rotation.eulerAngles.z));
                }
            }
        } else {
            if (type == FlagType.enemyFlag && collision.CompareTag("FriendlyFlagSpot")) {
                Debug.Log("You win!");
                Destroy(gameObject);
            } else if (type == FlagType.friendlyFlag && collision.CompareTag("EnemyFlagSpot")) {
                Debug.Log("You lose!");
            } else if (type == FlagType.friendlyFlag && collision.CompareTag("FriendlyFlagSpot") && carrier.CompareTag("FriendlyUnit")) {
                // Friendly flag restored
                RestoreFlag();
            } else if (type == FlagType.enemyFlag && collision.CompareTag("EnemyFlagSpot") && carrier.CompareTag("EnemyUnit")) {
                // Enemy flag restored
                RestoreFlag();
            }
        }
    }
}
