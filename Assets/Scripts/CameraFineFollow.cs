using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFineFollow : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    private Vector3 _originVector3 = new Vector3(0, 0, -10);

    void LateUpdate()
    {
        transform.rotation = new Quaternion(0, 0, _playerTransform.rotation.z, _playerTransform.rotation.w);
        float a = Mathf.Abs(transform.rotation.z);
        if (a >= 0.5f)
            a = 0.5f;

        Vector3 modified = new Vector3(_playerTransform.position.x, _playerTransform.position.y, -10);
        transform.position = Vector3.Slerp(_originVector3, modified, a);
    }
}
