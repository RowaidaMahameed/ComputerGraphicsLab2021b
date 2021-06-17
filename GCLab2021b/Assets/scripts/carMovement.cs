using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MyStuff : MonoBehaviour
{
    public int type;
    public int price;
    public int weight;
    public MyStuff(int newtype, int newprice, int newweight)
    {
        type = newtype;
        price = newprice;
        weight = newweight;
    }

}

public class carMovement : MonoBehaviour
{
    const int stuffsNumber = 3;
    public GameObject[] allStuff = new GameObject[stuffsNumber];
    public MyStuff stuff1 = new MyStuff(0,0,0);
    public MyStuff stuff2 = new MyStuff(1,0, 0);
    public MyStuff stuff3 = new MyStuff(2,0, 0);
    const int roadsNumber = 10;
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
