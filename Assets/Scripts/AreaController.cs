using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    public static AreaController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public float speed = 10;
    public bool reverse = false;
    public bool rotate = false;
    
    private void Update()
    {
        if(rotate)
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
