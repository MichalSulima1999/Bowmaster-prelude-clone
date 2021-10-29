using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUIHolder : MonoBehaviour, IDropHandler
{
    [SerializeField] private KeyCode key = KeyCode.Alpha0;

    private Button button;

    public void OnDrop(PointerEventData eventData) {
        if(eventData.pointerDrag != null && button == null) {
            eventData.pointerDrag.transform.parent = gameObject.transform;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        } 
    }

    private void Start() {
        InvokeRepeating("FindButton", 0f, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        if (button != null) {
            if (Input.GetKeyDown(key)) {
                button.onClick.Invoke();
                Debug.Log("Change arrow");
            }
        }
    }

    private void FindButton() {
        foreach (Transform child in transform) {
            if (child.tag == "ItemUI") {
                button = child.GetComponent<Button>();
                break;
            }
            button = null;
        }
    }
}
