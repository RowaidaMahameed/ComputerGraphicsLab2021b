using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class carMovement : MonoBehaviour
{
    const int roadsNumber = 6;
    public GameObject[] Roads = new GameObject[roadsNumber];
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
            Roads[roadIndex].gameObject.transform.position += new Vector3(0, 0, 30* roadsNumber);
            roadIndex = (roadIndex + 1) % roadsNumber;

        }
    }
}
