using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    public float speed = 10;
    public bool rotate = false;
    
    private void Update()
    {
        if(rotate)
        {
            transform.Rotate(new Vector3(0,0,1) * Time.deltaTime * speed);
        }
    }
}
