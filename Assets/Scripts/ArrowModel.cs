using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowModel : MonoBehaviour
{
    private Transform bow;
    [SerializeField] private float slingRadius = 1f;

    public Transform arrowAnchor { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        bow = GameObject.FindGameObjectWithTag("Bow").transform;
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        gameObject.transform.position = pz;*/

        transform.position = arrowAnchor.position;

        Vector3 offset = transform.position - bow.position;
        transform.position = bow.position + Vector3.ClampMagnitude(offset, slingRadius);

        transform.right = bow.position - transform.position;
        
    }
}
