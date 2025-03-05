using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaIgnore : MonoBehaviour
{
    public GameObject ScreenCenter;

    void Start()
    {   
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return null;
        transform.position = ScreenCenter.transform.position;
    }
}
