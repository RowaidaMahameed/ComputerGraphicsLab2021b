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
    const int stuffsNumber = 90;
    const int RoadssNumber = 66;
    public GameObject[] allStuff = new GameObject[stuffsNumber];
    public GameObject[] allRoads = new GameObject[RoadssNumber];
    public MyStuff stuff1 = new MyStuff(0,10,10);
    public MyStuff stuff2 = new MyStuff(1,60, 1000);
    public MyStuff stuff3 = new MyStuff(2,30, 200);
    public MyStuff[] All = new MyStuff[3];
    const int roadsNumber = 30;
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
    public MyStuff[] stuffs1 = new MyStuff[90];
    public GameObject[] Weights = new GameObject[90];
    public GameObject[] Prices = new GameObject[90];
    public int mytotal = 0;
    public int spaceleft = 1000;
    public int CarPos = 0;
    public bool MouseStarted = false;
    public int currentLevel = 0;
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
    public GameObject LoserWindow;
    public GameObject play;
    public GameObject exit;
    public GameObject continue_;
    public AudioSource audioSource;
    public AudioSource audioCollect;
    public AudioSource audioStart;


    public void PlayGame()
    {
        audioStart.Stop();
        currentLevel++;
        audioSource.Play();
        play.SetActive(false);
        exit.SetActive(false);
    }

    public void continueGame()
    {
        currentLevel++;
        continue_.SetActive(false);
        WinnerWindow.SetActive(false);
        finalScore.SetActive(false);
        spaceleft = 1000;
        mytotal = 0;
        sumweights.SetActive(true);
        sumPrice.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void Awake()
    {


    }
    void Start()
    {
        audioStart.Play();
        continue_.SetActive(false);
        finalScore.SetActive(false);
        WinnerWindow.SetActive(false);
        LoserWindow.SetActive(false);
        text1 = sumweights.GetComponent<TMPro.TextMeshProUGUI>();
        text2 = sumPrice.GetComponent<TMPro.TextMeshProUGUI>();
        text3 = finalScore.GetComponent<TMPro.TextMeshProUGUI>();
        All[0] = stuff1;
        All[1] = stuff2;
        All[2] = stuff3;
        rb = car.GetComponent<Rigidbody>();

        int[] price = { 100, 200, 100, 100, 50, 150, 200, 120, 90, 250, 200, 200, 120, 150, 100, 200, 150, 180, 50, 100, 300, 150, 120, 100, 80, 50, 50, 50, 80, 80 , 100, 200, 100, 100, 50, 150, 200, 120, 90, 250, 200, 200, 120, 150, 100, 200, 150, 180, 50, 100, 300, 150, 120, 100, 80, 50, 50, 50, 80, 80, 100, 200, 100, 100, 50, 150, 200, 120, 90, 250, 200, 200, 120, 150, 100, 200, 150, 180, 50, 100, 300, 150, 120, 100, 80, 50, 50, 50, 80, 80 };
        int[] weights = { 100, 50, 80, 90, 10, 100, 50, 40, 30, 20, 70, 140, 140, 200, 120, 500, 400, 200, 300, 140, 120, 100, 60, 110, 70, 210, 10, 230, 10, 50, 100, 50, 80, 90, 10, 100, 50, 40, 30, 20, 70, 140, 140, 200, 120, 500, 400, 200, 300, 140, 120, 100, 60, 110, 70, 210, 10, 230, 10, 50, 100, 50, 80, 90, 10, 100, 50, 40, 30, 20, 70, 140, 140, 200, 120, 500, 400, 200, 300, 140, 120, 100, 60, 110, 70, 210, 10, 230, 10, 50 };

        for (int i=0; i<90; i++)
        {
            int j = UnityEngine.Random.Range(0, 9);
            stuffs1[i] = new MyStuff(j, price[i], weights[i]);
        }

        for (int i = 0; i < RoadssNumber; i++)
        {
            allRoads[i] = Instantiate(Roads[(10*(i/22)) + (i%10)],new Vector3(0, 0, 30 * i), Quaternion.identity);
        }

        Debug.Log(stuffs1.Length);
        Debug.Log(allStuff.Length);

        for (int i = 0; i < stuffs1.Length / 3; i++)
        {
         
            allStuff[i * 3] = Instantiate(prefabs[stuffs1[i * 3].getType()], new Vector3(-6.4f, 1.4f + prefabs[stuffs1[i * 3].getType()].transform.lossyScale.y / 2, roundDist * (i + 1)), Quaternion.identity);
            allStuff[i * 3 + 1] = Instantiate(prefabs[stuffs1[i * 3 + 1].getType()], new Vector3(0, 1.4f + prefabs[stuffs1[i * 3 + 1].getType()].transform.lossyScale.y / 2, roundDist * (i + 1)), Quaternion.identity);
            allStuff[i * 3 + 2] = Instantiate(prefabs[stuffs1[i * 3 + 2].getType()], new Vector3(6.4f, 1.4f + prefabs[stuffs1[i * 3 + 2].getType()].transform.lossyScale.y / 2, roundDist * (i + 1)), Quaternion.identity);

            Weights[i * 3] = Instantiate(texts[0], new Vector3(-6.4f, 1.4f + prefabs[stuffs1[i * 3].getType()].transform.lossyScale.y / 2 + 4, roundDist * (i + 1)), Quaternion.identity);
            Weights[i * 3 + 1] = Instantiate(texts[0], new Vector3(0, 1.4f + prefabs[stuffs1[i * 3 + 1].getType()].transform.lossyScale.y / 2 + 4, roundDist * (i + 1)), Quaternion.identity);
            Weights[i * 3 + 2] = Instantiate(texts[0], new Vector3(6.4f, 1.4f + prefabs[stuffs1[i * 3 + 2].getType()].transform.lossyScale.y / 2 + 4, roundDist * (i + 1)), Quaternion.identity);

            TMPro.TextMeshPro t1 = Weights[i * 3].GetComponent<TMPro.TextMeshPro>();
            TMPro.TextMeshPro t2 = Weights[i * 3 + 1].GetComponent<TMPro.TextMeshPro>();
            TMPro.TextMeshPro t3 = Weights[i * 3 + 2].GetComponent<TMPro.TextMeshPro>();

            t1.text = "" + stuffs1[i * 3].getWeight();
            t2.text = "" + stuffs1[i * 3 + 1].getWeight();
            t3.text = "" + stuffs1[i * 3 + 2].getWeight();

            Prices[i * 3] = Instantiate(texts[1], new Vector3(-6.4f, 1.4f + prefabs[stuffs1[i * 3].getType()].transform.lossyScale.y / 2 + 6, roundDist * (i + 1)), Quaternion.identity);
            Prices[i * 3 + 1] = Instantiate(texts[1], new Vector3(0, 1.4f + prefabs[stuffs1[i * 3 + 1].getType()].transform.lossyScale.y / 2 + 6, roundDist * (i + 1)), Quaternion.identity);
            Prices[i * 3 + 2] = Instantiate(texts[1], new Vector3(6.4f, 1.4f + prefabs[stuffs1[i * 3 + 2].getType()].transform.lossyScale.y / 2 + 6, roundDist * (i + 1)), Quaternion.identity);

            TMPro.TextMeshPro t4 = Prices[i * 3].GetComponent<TMPro.TextMeshPro>();
            TMPro.TextMeshPro t5 = Prices[i * 3 + 1].GetComponent<TMPro.TextMeshPro>();
            TMPro.TextMeshPro t6 = Prices[i * 3 + 2].GetComponent<TMPro.TextMeshPro>();

            t4.text = "" + stuffs1[i * 3].getPrice();
            t5.text = "" + stuffs1[i * 3 + 1].getPrice();
            t6.text = "" + stuffs1[i * 3 + 2].getPrice();
            

        }
    }

    void Update()
    {
        if(currentLevel %2 == 1)
        {
            if (lev == 10)
            {
                //Debug.Log("Winner");
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
                else {
                    if (mytotal >= winners_value[0])
                    {
                        //Debug.Log("Winner");
                        mytotal -= winners_value[0];
                        spaceleft = 0;
                        WinnerWindow.SetActive(true);
                        text3.text = "finalScore : " + mytotal;
                        finalScore.SetActive(true);
                        sumweights.SetActive(false);
                        sumPrice.SetActive(false);
                        currentLevel++;
                        continue_.SetActive(true);
                        lev = 0;
                    }
                    else
                    {
                        //Debug.Log("Loser");
                        LoserWindow.SetActive(true);
                        text3.text = "finalScore : " + mytotal;
                        finalScore.SetActive(true);
                        sumweights.SetActive(false);
                        sumPrice.SetActive(false);
                    }
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
                    allStuff[lev * 3 + CarPos + 1].SetActive(false);
                    Prices[lev * 3 + CarPos + 1].SetActive(false);
                    Weights[lev * 3 + CarPos + 1].SetActive(false);

                    MyStuff temp = stuffs1[lev * 3 + CarPos + 1];
                    if (spaceleft - temp.getWeight() >= 0)
                    {
                        mytotal = mytotal + temp.getPrice();
                        spaceleft = spaceleft - temp.getWeight();
                        audioCollect.Play();
                    }
                    lev++;
                    dist_posi -= 60;
                }
            }
        }

        /* Moving Code - Mouse and Touch */
        if (Input.touchCount > 0)
        {
            // Debug.Log(Input.GetTouch(0).position);

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
           // Debug.Log(Input.mousePosition.x);
        }

        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            //Debug.Log("First" + Input.mousePosition.x);
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
                // Debug.Log("Riiight");
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
