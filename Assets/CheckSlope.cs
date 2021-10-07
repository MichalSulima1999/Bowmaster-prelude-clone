using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSlope : MonoBehaviour
{
    private Vector2 slopeNormal;

    private Transform parent;
    private Unit unit;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        unit = parent.GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnTriggerEnter2D(Collider2D collision) {
        unit.onSlope = true;

        Debug.Log("trigger");
        ContactPoint2D[] contacts = new ContactPoint2D[1];
        collision.GetContacts(contacts);
        slopeNormal = contacts[0].normal;

        parent.rotation = Quaternion.FromToRotation(parent.up, contacts[0].normal) * parent.rotation;

        transform.rotation = Quaternion.FromToRotation(transform.up, contacts[0].normal) * transform.rotation;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        unit.onSlope = false;
    }*/
}
