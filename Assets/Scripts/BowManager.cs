using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowManager : MonoBehaviour
{
    [SerializeField] private GameObject arrowModelPrefab;
    [SerializeField] private GameObject arrowAnchorPrefab;
    [SerializeField] private GameObject normalArrowPrefab;

    private GameObject arrowModel;
    private GameObject arrowAnchor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && arrowModel == null) {
            arrowAnchor = Instantiate(arrowAnchorPrefab, transform.position, Quaternion.identity);

            arrowModel = Instantiate(arrowModelPrefab, transform.position, Quaternion.identity);
            arrowModel.GetComponent<ArrowModel>().arrowAnchor = arrowAnchor.transform;
        }

        if (Input.GetMouseButtonUp(0) && arrowModel != null) {
            GameObject arrow = Instantiate(normalArrowPrefab, arrowModel.transform.position, arrowModel.transform.rotation);
            arrow.GetComponent<Arrow>().speed = Vector3.Distance(transform.position, arrowModel.transform.position);
            Destroy(arrowModel);
            Destroy(arrowAnchor);
        }
    }
}
