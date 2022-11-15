using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    public static bool IsNotMoving = true;

    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform bulletTRANS;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Instantiate(prefab, bulletTRANS.position, Quaternion.identity);
        }
    }
}
