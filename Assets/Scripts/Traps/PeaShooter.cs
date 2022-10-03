using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : MonoBehaviour
{
    private MenuManager menuManager;
    [SerializeField] private GameObject _projectilePrefab;
    [Space]
    [SerializeField] private float _projectileLifetime = 5f;
    [SerializeField] private float _projectileSpeed = 5f;
    [SerializeField] private float _fireRateTimer = 2f;
    public float _timer = 0;

    private void Awake()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _timer = _fireRateTimer;
            ShootProjectile();
        }
    }

    private void ShootProjectile()
    {
        GameObject projectileObject = Instantiate(_projectilePrefab, transform.position, transform.rotation);
        Projectile projectileScript = projectileObject.GetComponent<Projectile>();
        Vector2 goodVector = new Vector2(transform.forward.x, transform.forward.z);
        projectileScript.ReceiveData(transform.right, _projectileSpeed, menuManager);
        Destroy(projectileObject, _projectileLifetime);
    }

}
