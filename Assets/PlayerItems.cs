using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [SerializeField] private Transform[] itemHolders;

    public Transform GetFreeItemHolder() {
        foreach(Transform itemHolder in itemHolders) {
            if(itemHolder.childCount <= 1) {
                return itemHolder;
            }
        }

        return null;
    }
}
