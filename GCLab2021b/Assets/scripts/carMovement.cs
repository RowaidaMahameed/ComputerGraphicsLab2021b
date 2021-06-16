using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMovement : MonoBehaviour
{
    public GameObject[] Roads = new GameObject[4];
    public int roadIndex = 0;
    public GameObject car;
    public GameObject mycamera;
    public float distance = 0;
    public float speed = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        car.gameObject.transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
        mycamera.gameObject.transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
        distance += speed * Time.deltaTime;
        if(distance >= 30)
        {
            distance = 0;
            Roads[roadIndex].gameObject.transform.position += new Vector3(0, 0, 120);
            roadIndex = (roadIndex + 1) % 4;

        }
    }
}
