using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayGravField : MonoBehaviour
{


    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.transform.rotation = Quaternion.Euler(180, 0, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

}
