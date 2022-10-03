using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullWayGravField : MonoBehaviour
{
    [SerializeField] private bool _reverse = false;
    [SerializeField] private int _maxAngleManipulation;
    [SerializeField] private int _minAngleManipulation;
    private float _previousAngle;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector3 targetDirection;
        if (!_reverse)
        {
            targetDirection = collision.transform.position - transform.position;
        }
        else
        {
            targetDirection = transform.position - collision.transform.position;
        }

        float rotationZ = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        rotationZ -= 270;
        collision.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!gameManager._countdownOn)
        {
            if (collision.transform.position.y > transform.position.y)
            {
                collision.transform.rotation = Quaternion.Euler(0, 0, _maxAngleManipulation);
            }
            else
            {
                collision.transform.rotation = Quaternion.Euler(0, 0, _minAngleManipulation);
            }
        }
    }
}
