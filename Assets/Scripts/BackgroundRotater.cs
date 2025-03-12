using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRotater : MonoBehaviour
{
    public float speed = -15;
    public bool reverse = false;
    public bool rotate = true;

    private void Update()
    {
        if (rotate)
        {
            if (reverse)
            {
                transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * -speed);
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
            }
        }
    }
}
