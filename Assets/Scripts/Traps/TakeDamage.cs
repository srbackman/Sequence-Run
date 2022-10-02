using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    private MenuManager menuManager;

    private void Awake()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        menuManager.GoToGameOverMenu();
    }
}
