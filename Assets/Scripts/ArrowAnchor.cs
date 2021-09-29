using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnchor : MonoBehaviour
{
    private Vector3 startMousePosition;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

        startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startMousePosition.z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;

        transform.position = startPosition + (pz - startMousePosition);
    }
}
