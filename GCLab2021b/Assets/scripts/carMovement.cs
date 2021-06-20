using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;


public class MyStuff
{
    private int type;
    private int price;
    private int weight;
    public MyStuff(int newtype, int newprice, int newweight)
    {
        type = newtype;
        price = newprice;
        weight = newweight;
    }

    public int getType()
    {
        return type;
    }
    public int getPrice()
    {
        return price;
    }
    public int getWeight()
    {
        return weight;
    }
}

public class carMovement : MonoBehaviour
{
    public GameObject[] prefabs = new GameObject[10];
    public GameObject[] texts = new GameObject[2];
    const int stuffsNumber = 30;
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
    public float roundDist = 60;
    public MyStuff[] myStuffs = new MyStuff[30];
    public GameObject[] Weights = new GameObject[30];
    public GameObject[] Prices = new GameObject[30];
    // Start is called before the first frame update

    void Start()
    {
        All[0] = stuff1;
        All[1] = stuff2;
        All[2] = stuff3;

        myStuffs[0] = new MyStuff(0, 100, 100);
        myStuffs[1] = new MyStuff(1, 200, 150);
        myStuffs[2] = new MyStuff(2, 100, 200);//s1
        myStuffs[3] = new MyStuff(3, 100, 120);
        myStuffs[4] = new MyStuff(4, 50, 50);
        myStuffs[5] = new MyStuff(5, 150, 200);// s2
        myStuffs[6] = new MyStuff(6, 200, 150);
        myStuffs[7] = new MyStuff(7, 120, 200);
        myStuffs[8] = new MyStuff(8, 90, 90);//s3
        myStuffs[9] = new MyStuff(9, 250, 150);
        myStuffs[10] = new MyStuff(1, 200, 200);
        myStuffs[11] = new MyStuff(4, 200, 300);//s4
        myStuffs[12] = new MyStuff(7, 120, 120);
        myStuffs[13] = new MyStuff(2, 150, 150);
        myStuffs[14] = new MyStuff(6, 100, 150);//s5
        myStuffs[15] = new MyStuff(5, 200, 100);
        myStuffs[16] = new MyStuff(9, 150, 200);
        myStuffs[17] = new MyStuff(4, 150, 150);//s6
        myStuffs[18] = new MyStuff(2, 100, 50);
        myStuffs[19] = new MyStuff(6, 50, 50);
        myStuffs[20] = new MyStuff(7, 200, 150);//s7
        myStuffs[21] = new MyStuff(1, 200, 150);
        myStuffs[22] = new MyStuff(9, 120, 150);
        myStuffs[23] = new MyStuff(3, 100, 100);//s8
        myStuffs[24] = new MyStuff(7, 150, 80);
        myStuffs[25] = new MyStuff(4, 100, 70);
        myStuffs[26] = new MyStuff(0, 130, 150);//s9
        myStuffs[27] = new MyStuff(8, 80, 100);
        myStuffs[28] = new MyStuff(4, 90, 90);
        myStuffs[29] = new MyStuff(2, 120, 110);//s10

        for (int i = 0; i < myStuffs.Length / 3; i++)
        {
            allStuff[i * 3] = Instantiate(prefabs[myStuffs[i * 3].getType()], new Vector3(-6.4f, 1.4f + prefabs[myStuffs[i * 3].getType()].transform.lossyScale.y / 2, roundDist * (i + 1)), Quaternion.identity);
            allStuff[i * 3 + 1] = Instantiate(prefabs[myStuffs[i * 3 + 1].getType()], new Vector3(0, 1.4f + prefabs[myStuffs[i * 3 + 1].getType()].transform.lossyScale.y / 2, roundDist * (i + 1)), Quaternion.identity);
            allStuff[i * 3 + 2] = Instantiate(prefabs[myStuffs[i * 3 + 2].getType()], new Vector3(6.4f, 1.4f + prefabs[myStuffs[i * 3 + 2].getType()].transform.lossyScale.y / 2, roundDist * (i + 1)), Quaternion.identity);

            Weights[i * 3] = Instantiate(texts[0], new Vector3(-6.4f, 1.4f + prefabs[myStuffs[i * 3].getType()].transform.lossyScale.y / 2 + 4, roundDist * (i + 1)), Quaternion.identity);
            Weights[i * 3 + 1] = Instantiate(texts[0], new Vector3(0, 1.4f + prefabs[myStuffs[i * 3 + 1].getType()].transform.lossyScale.y / 2 + 4, roundDist * (i + 1)), Quaternion.identity);
            Weights[i * 3 + 2] = Instantiate(texts[0], new Vector3(6.4f, 1.4f + prefabs[myStuffs[i * 3 + 2].getType()].transform.lossyScale.y / 2 + 4, roundDist * (i + 1)), Quaternion.identity);
            TMPro.TextMeshPro t1 = Weights[i * 3].GetComponent<TMPro.TextMeshPro>();
            TMPro.TextMeshPro t2 = Weights[i * 3 + 1].GetComponent<TMPro.TextMeshPro>();
            TMPro.TextMeshPro t3 = Weights[i * 3 + 2].GetComponent<TMPro.TextMeshPro>();

            t1.text = "" + myStuffs[i * 3].getWeight();
            t2.text = "" + myStuffs[i * 3 + 1].getWeight();
            t3.text = "" + myStuffs[i * 3 + 2].getWeight();

            Prices[i * 3] = Instantiate(texts[1], new Vector3(-6.4f, 1.4f + prefabs[myStuffs[i * 3].getType()].transform.lossyScale.y / 2 + 6, roundDist * (i + 1)), Quaternion.identity);
            Prices[i * 3 + 1] = Instantiate(texts[1], new Vector3(0, 1.4f + prefabs[myStuffs[i * 3 + 1].getType()].transform.lossyScale.y / 2 + 6, roundDist * (i + 1)), Quaternion.identity);
            Prices[i * 3 + 2] = Instantiate(texts[1], new Vector3(6.4f, 1.4f + prefabs[myStuffs[i * 3 + 2].getType()].transform.lossyScale.y / 2 + 6, roundDist * (i + 1)), Quaternion.identity);

            TMPro.TextMeshPro t4 = Prices[i * 3].GetComponent<TMPro.TextMeshPro>();
            TMPro.TextMeshPro t5 = Prices[i * 3 + 1].GetComponent<TMPro.TextMeshPro>();
            TMPro.TextMeshPro t6 = Prices[i * 3 + 2].GetComponent<TMPro.TextMeshPro>();

            t4.text = "" + myStuffs[i * 3].getPrice();
            t5.text = "" + myStuffs[i * 3 + 1].getPrice();
            t6.text = "" + myStuffs[i * 3 + 2].getPrice();


        }
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
        

    }
    
}
