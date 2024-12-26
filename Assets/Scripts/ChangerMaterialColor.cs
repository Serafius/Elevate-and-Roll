using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerMaterialColor : MonoBehaviour
{
    [SerializeField] private Renderer myObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myObject.material.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myObject.material.color = Color.white;
        }
    }
}
