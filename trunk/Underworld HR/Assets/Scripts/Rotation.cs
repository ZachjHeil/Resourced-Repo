using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    void Update()
    {
    transform.Rotate(new Vector3(0f, 75f, 0f)* Time.deltaTime);   
    }
}
