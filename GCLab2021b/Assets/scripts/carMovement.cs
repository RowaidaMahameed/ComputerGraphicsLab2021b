using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;


public class MyStuff
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
    public GameObject[] weights = new GameObject[3] ;
    public GameObject[] prices = new GameObject[3];

    const int stuffsNumber = 3;
    public GameObject[] allStuff = new GameObject[stuffsNumber];
    public MyStuff stuff1 = new MyStuff(0,10,10);
    public MyStuff stuff2 = new MyStuff(1,60, 1000);
    public MyStuff stuff3 = new MyStuff(2,30, 200);
    public MyStuff[] All = new MyStuff[3];
    const int roadsNumber = 10;
    public GameObject[] Roads = new GameObject[roadsNumber];
    public int roadIndex = 0;
    public GameObject car;
    public GameObject mycamera;
    public float distance = 0;
    public float textdist = 0;
    public float speed = 15;
    // Start is called before the first frame update

    void Start()
    {
        All[0] = stuff1;
        All[1] = stuff2;
        All[2] = stuff3;
    }

    // Update is called once per frame
    void Update()
    {
        car.gameObject.transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
        mycamera.gameObject.transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
        distance += speed * Time.deltaTime;
        textdist += speed * Time.deltaTime;
        if (distance >= 30)
        {
            distance = 0;
            Roads[roadIndex].gameObject.transform.position += new Vector3(0, 0, 30 * roadsNumber);
            roadIndex = (roadIndex + 1) % roadsNumber;

        }
        if (textdist >= 50)
        {
            weights[0].GetComponent<Text>().text = "10";
            weights[1].GetComponent<Text>().text = "60";
            weights[2].GetComponent<Text>().text = "30";
            prices[0].GetComponent<Text>().text = "10";
            prices[1].GetComponent<Text>().text = "1000";
            prices[2].GetComponent<Text>().text = "200";

        }
    }
    
}
