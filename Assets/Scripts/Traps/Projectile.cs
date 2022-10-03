using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private MenuManager _menuManager;
    private Vector2 _direction = Vector2.zero;
    private float _speed = 0;

    void Update()
    {
        if (_speed != 0)
            Move();
    }

    private void Move()
    {
        transform.position += (Vector3)(_direction * _speed * Time.deltaTime);
    }

    public void ReceiveData(Vector2 direction, float speed, MenuManager menuManager)
    {
        _direction = direction;
        _speed = speed;
        _menuManager = menuManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _menuManager.GoToGameOverMenu("You were shot down.");
    }
}
