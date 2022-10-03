using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    private MenuManager menuManager;
    [SerializeField] private string _deathText = "You were pierced by a spike.";

    private void Awake()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        menuManager.GoToGameOverMenu(_deathText);
    }
}
