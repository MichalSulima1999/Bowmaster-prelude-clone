using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BowManager : MonoBehaviour
{
    [SerializeField] private GameObject arrowModelPrefab;
    [SerializeField] private GameObject arrowAnchorPrefab;
    [SerializeField] private GameObject startingArrow;

    [SerializeField] private GameObject bowSFX;

    private GameObject arrowModel;
    private GameObject arrowAnchor;

    private GameObject activeArrow;
    private Arrow arrowScript;

    // Start is called before the first frame update
    void Start()
    {
        activeArrow = startingArrow;
        arrowScript = startingArrow.GetComponent<Arrow>();
    }

    public void ChangeArrow(Arrow arrow) {
        activeArrow = arrow.gameObject;
        arrowScript = arrow;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        if (Input.GetMouseButtonDown(0) && arrowModel == null ) {
            arrowAnchor = Instantiate(arrowAnchorPrefab, transform.position, Quaternion.identity);

            arrowModel = Instantiate(arrowModelPrefab, transform.position, Quaternion.identity);
            arrowModel.GetComponent<ArrowModel>().arrowAnchor = arrowAnchor.transform;
        }

        if (Input.GetMouseButtonUp(0) && arrowModel != null) {
            if (arrowScript.arrowSO.reloadCounter <= 0) {
                GameObject arrow = Instantiate(activeArrow, arrowModel.transform.position, arrowModel.transform.rotation);
                arrow.GetComponent<Arrow>().speed = Vector3.Distance(transform.position, arrowModel.transform.position);

                GameObject sfx = Instantiate(bowSFX, transform.position, Quaternion.identity);
                Destroy(sfx, 2f);

                arrowScript.arrowSO.reloadCounter = arrowScript.arrowSO.reloadTime;
            }

            Destroy(arrowModel);
            Destroy(arrowAnchor);
        }
    }
}
