using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class FinishAreaTrigger : MonoBehaviour
{
    private MenuManager menuManager;
    [SerializeField] UnityEvent _finishTrigger;

    private void Awake()
    {
        menuManager = FindObjectOfType<MenuManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _finishTrigger.Invoke();
    }
}
