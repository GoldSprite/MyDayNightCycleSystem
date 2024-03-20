using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    [Range(0, 10)]
    public float Rate = 5;
    public GameObject cube;
    public GameObject lightObj;
    private float tick;


    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        cube.transform.rotation = Quaternion.Euler(45, tick+=Time.deltaTime*Rate * 10, 45);
        lightObj.transform.rotation = Quaternion.Euler(-tick, 45, 0);
    }
}
