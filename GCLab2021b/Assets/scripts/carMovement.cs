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
    Rigidbody rb;
    public static readonly int[] winners_value = { 1000,3000,1600};
    public GameObject sumweights;
    public GameObject sumPrice;
    public GameObject finalScore;
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
    public float dist_posi = 0;
    public float textdist = 0;
    int lev = 0;
    public int level1_end = 0;
    public float speed = 15;
    public float roundDist = 60;
    public MyStuff[] myStuffs = new MyStuff[30];
    public GameObject[] Weights = new GameObject[30];
    public GameObject[] Prices = new GameObject[30];
    public int mytotal = 0;
    public int spaceleft = 1000;
    public int CarPos = 0;
    public bool MouseStarted = false;
    // touch vectors 
    Vector2 firstTouch = new Vector2(-1, -1);
    Vector2 CurTouch = new Vector2(-1,-1);
    int moveDir = 0;

    // Start is called before the first frame update
    TMPro.TextMeshProUGUI text1;
    TMPro.TextMeshProUGUI text2;
    TMPro.TextMeshProUGUI text3;
    public bool isMoving = false;
    public GameObject WinnerWindow;
    void Start()
    {
        finalScore.SetActive(false);
        WinnerWindow.SetActive(false);
        text1 = sumweights.GetComponent<TMPro.TextMeshProUGUI>();
        text2 = sumPrice.GetComponent<TMPro.TextMeshProUGUI>();
        text3 = finalScore.GetComponent<TMPro.TextMeshProUGUI>();
        All[0] = stuff1;
        All[1] = stuff2;
        All[2] = stuff3;
        rb = car.GetComponent<Rigidbody>();
        int[] array1 = { 100, 200, 100, 100, 50, 150, 200, 120, 90, 250, 200, 200, 120, 150, 100, 200, 150, 180, 50, 100, 300, 150, 120, 100, 80, 50, 50, 50, 80, 80 };
        int[] array2 = { 100, 50, 80, 90, 10, 100, 50, 40, 30, 20, 70, 140, 140, 200, 120, 500, 400, 200, 300, 140, 120, 100, 60, 110, 70, 210, 10, 230, 10, 50 };
        for(int i=0; i<30; i++)
        {
            int j = UnityEngine.Random.Range(0, 9);
            myStuffs[i] = new MyStuff(j, array1[i], array2[i]);
        }

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

    void Update()
    {
        if (lev == 10)
        {
            Debug.Log("Winner");
            if (textdist <= 610)
            {
                text1.text = "space left : " + spaceleft;
                text2.text = "total : " + mytotal;
                car.gameObject.transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
                mycamera.gameObject.transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
                distance += speed * Time.deltaTime;
                dist_posi += speed * Time.deltaTime;
                textdist += speed * Time.deltaTime;
            }
            if (mytotal >= winners_value[0])
            {
                Debug.Log("Winner");
                mytotal -= winners_value[0];
                spaceleft = 0;
                WinnerWindow.SetActive(true);
                text3.text = "finalScore : " + mytotal;
                finalScore.SetActive(true);
                sumweights.SetActive(false);
                sumPrice.SetActive(false);
            }
            else
            {
                Debug.Log("Loser");
            }
        }
        else
        {

            text1.text = "space left : " + spaceleft;
            text2.text = "total : " + mytotal;
            car.gameObject.transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
            mycamera.gameObject.transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
            distance += speed * Time.deltaTime;
            dist_posi += speed * Time.deltaTime;
            textdist += speed * Time.deltaTime;
            if (dist_posi >= 60)
            {
                Debug.Log("CarPos " + CarPos);
                MyStuff temp = myStuffs[lev * 3 + CarPos + 1];
                if (spaceleft - temp.getWeight() >= 0)
                {
                    mytotal = mytotal + temp.getPrice();
                    spaceleft = spaceleft - temp.getWeight();
                }
                lev++;
                dist_posi -= 60;
            }
            if (distance >= 30)
            {
                //Debug.Log("must move");
                distance = 0;
                //Debug.Log(Roads[roadIndex].gameObject.transform.position);
                Roads[roadIndex].gameObject.transform.position += new Vector3(0, 0, 30 * roadsNumber);
                //Debug.Log(Roads[roadIndex].gameObject.transform.position);
                roadIndex = (roadIndex + 1) % roadsNumber;

            }


            if (Input.touchCount > 0)
            {
                Debug.Log(Input.GetTouch(0).position);

                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    firstTouch = Input.GetTouch(0).position;
                }
                if (!isMoving && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    CurTouch = Input.GetTouch(0).position;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    isMoving = false;
                    firstTouch = new Vector2(-1, -1);
                    CurTouch = new Vector2(-1, -1);
                }
            }

            if (MouseStarted)
            {
                CurTouch = Input.mousePosition;
                Debug.Log(Input.mousePosition.x);
            }

            if (!isMoving && Input.GetMouseButtonDown(0))
            {
                Debug.Log("First" + Input.mousePosition.x);
                firstTouch = Input.mousePosition;
                MouseStarted = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                firstTouch = new Vector2(-1, -1);
                CurTouch = new Vector2(-1, -1);

                MouseStarted = false;
                isMoving = false;
            }



            //for both mouse and touch
            if (firstTouch.x > -1 && CurTouch.x > -1)
            {
                if (!isMoving && CurTouch.x - firstTouch.x > 200)
                {
                    Debug.Log("Riiight");
                    moveDir = 1;
                    isMoving = true;
                    rb.velocity = (Vector3.right * 10);
                    firstTouch = new Vector2(-1, -1);
                    CurTouch = new Vector2(-1, -1);
                }
                else if (!isMoving && CurTouch.x - firstTouch.x < -200)
                {
                    moveDir = -1;
                    isMoving = true;
                    rb.velocity = (Vector3.left * 10);
                    firstTouch = new Vector2(-1, -1);
                    CurTouch = new Vector2(-1, -1);
                }
            }
            if (moveDir == 1)
            {
                if (CarPos == 0)
                {
                    if (car.gameObject.transform.position.x >= 7)
                    {
                        car.gameObject.transform.position = new Vector3(7, car.gameObject.transform.position.y, car.gameObject.transform.position.z);
                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                        CarPos = 1;
                        moveDir = 0;
                        isMoving = false;
                    }
                }
                else if (CarPos == -1)
                {
                    if (car.gameObject.transform.position.x >= 0)
                    {
                        car.gameObject.transform.position = new Vector3(0, car.gameObject.transform.position.y, car.gameObject.transform.position.z);
                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                        CarPos = 0;
                        moveDir = 0;
                        isMoving = false;
                    }
                }
                else if (CarPos == 1)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    moveDir = 0;
                    isMoving = false;
                }
            }

            if (moveDir == -1)
            {
                if (CarPos == 0)
                {
                    if (car.gameObject.transform.position.x <= -7)
                    {
                        car.gameObject.transform.position = new Vector3(-7, car.gameObject.transform.position.y, car.gameObject.transform.position.z);
                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                        CarPos = -1;
                        moveDir = 0;
                        isMoving = false;
                    }
                }
                else if (CarPos == 1)
                {
                    if (car.gameObject.transform.position.x <= 0)
                    {
                        car.gameObject.transform.position = new Vector3(0, car.gameObject.transform.position.y, car.gameObject.transform.position.z);
                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                        CarPos = 0;
                        moveDir = 0;
                        isMoving = false;

                    }
                }
                else if (CarPos == -1)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    moveDir = 0;
                    isMoving = false;
                }
            }

        }
    }
    
}
