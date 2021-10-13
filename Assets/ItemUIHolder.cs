using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIHolder : MonoBehaviour
{
    [SerializeField] private KeyCode key = KeyCode.Alpha0;

    [SerializeField] private Button button;

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
}
