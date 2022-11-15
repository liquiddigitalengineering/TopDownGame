using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform bulletTRANS;

    private void Update()
    {
        Vector3 targ = transform.position;
        targ.z = 0f;

        Vector3 objectPos = Input.mousePosition;
        targ.x -= objectPos.x;
        targ.y -= objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetMouseButtonDown(0)) {
            Instantiate(prefab, bulletTRANS.position, Quaternion.identity);
        }
    }
}
