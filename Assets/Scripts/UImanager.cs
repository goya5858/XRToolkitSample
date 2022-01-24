using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public GameObject picher;

    public GameObject GameStart;
    public GameObject Level;
    public GameObject PicherHeightSlider;

    // GameStartButton関連
    GameObject GameStartButtonText;
    Color      startButtonColor;
       
    // default ball speed and angle
    float   ballSpeed       = 53f;
    Vector3 projectionAngle = new Vector3(315f, 90f, 0);

    // picherHeight関係
    float sliderValue = 0.5f;
    Vector3 picherPosition = new Vector3(-12.49f, 0.46f, 0);



    // Start is called before the first frame update
    void Start()
    {
        picher     = GameObject.Find("Picher");
        GameStartButtonText = GameStart.transform.GetChild(0).GetChild(0).gameObject;
        // GameStart/Button/Textオブジェクトを取得する

        if (picher.GetComponent<Picher>().isThrow)
        {
            GameStart.GetComponentInChildren<Image>().color = Color.blue;
            GameStartButtonText.GetComponent<Text>().text = "Stop Game";
        }
        else
        {
            ColorUtility.TryParseHtmlString("#ffad00", out startButtonColor);
            GameStart.GetComponentInChildren<Image>().color = startButtonColor;
            GameStartButtonText.GetComponent<Text>().text = "Start Game";
        }
    }

    // GameStartのボタンを押した時の処理
    public void OnClickGameStart()
    {
        if (picher.GetComponent<Picher>().isThrow)
        {
            picher.GetComponent<Picher>().isThrow = false;
            GameStartButtonText.GetComponent<Text>().text = "Start Game";

            ColorUtility.TryParseHtmlString("#ffad00", out startButtonColor);
            GameStart.GetComponentInChildren<Image>().color = startButtonColor;
        }
        else
        {
            picher.GetComponent<Picher>().isThrow = true;
            picher.GetComponent<Picher>().deltaTime = 2.0f;
            GameStartButtonText.GetComponent<Text>().text = "Stop Game";

            GameStart.GetComponentInChildren<Image>().color = Color.blue;
        }
    }

    // Levelのボタンを押した時の処理×3
    public void OnClickEasy()
    {
        picher.GetComponent<Picher>().deltaTime = 0f;
        ballSpeed = 53f;
        projectionAngle.x = 315;

        picher.GetComponent<Picher>().projectionPower = ballSpeed;
        picher.transform.GetChild(0).GetComponent<Transform>().localEulerAngles = projectionAngle;

        Level.transform.GetChild(0).GetComponent<Image>().color = Color.green;
        Level.transform.GetChild(1).GetComponent<Image>().color = Color.white;
        Level.transform.GetChild(2).GetComponent<Image>().color = Color.white;
        Level.transform.GetChild(2).GetChild(0).GetComponent<Text>().color = Color.black;
    }

    public void OnClickNormal()
    {
        picher.GetComponent<Picher>().deltaTime = 0f;
        ballSpeed = 100f;
        projectionAngle.x = 354;

        picher.GetComponent<Picher>().projectionPower = ballSpeed;
        picher.transform.GetChild(0).GetComponent<Transform>().localEulerAngles = projectionAngle;

        Level.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        Level.transform.GetChild(1).GetComponent<Image>().color = Color.yellow;
        Level.transform.GetChild(2).GetComponent<Image>().color = Color.white;
        Level.transform.GetChild(2).GetChild(0).GetComponent<Text>().color = Color.black;
    }

    public void OnClickHard()
    {
        picher.GetComponent<Picher>().deltaTime = 0f;
        ballSpeed = 140f;
        projectionAngle.x = 358;

        picher.GetComponent<Picher>().projectionPower = ballSpeed;
        picher.transform.GetChild(0).GetComponent<Transform>().localEulerAngles = projectionAngle;

        Level.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        Level.transform.GetChild(1).GetComponent<Image>().color = Color.white;
        Level.transform.GetChild(2).GetComponent<Image>().color = Color.red;
        Level.transform.GetChild(2).GetChild(0).GetComponent<Text>().color = Color.white;
    }

    // PicherHeightSlider関連
    public void onHeightSliderChange()
    {
        sliderValue = PicherHeightSlider.GetComponentInChildren<Slider>().value; //0～1の値をとる
        picherPosition.y = 0.46f + (sliderValue - 0.5f);
        picher.transform.position = picherPosition;
    }
}
