using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform firepoinTransform;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Instantiate(prefab, firepoinTransform.position, firepoinTransform.rotation);
        }
    }
}
