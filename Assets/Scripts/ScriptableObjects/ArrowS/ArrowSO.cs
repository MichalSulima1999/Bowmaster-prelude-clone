using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New arrow", menuName = "Arrow")]
public class ArrowSO : ScriptableObject
{
    public float speedMultiplier = 10f;

    public int damageToUnits = 25;
    public int damageToBuildings = 5;

    public int criticalMultiplier = 2;

    public float reloadTime = 1f;
    public float reloadCounter;

    public GameObject hitSFX;

    public void PlaySFX(Transform transform) {
        GameObject sfx = Instantiate(hitSFX, transform.position, Quaternion.identity);
        Destroy(sfx, 2f);
    }

}
