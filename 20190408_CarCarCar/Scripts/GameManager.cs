using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public short gameFlow = 0;
    
    //float coolTime = 0;
    //float delayTime = 3;


    public GameObject[] groundScrolling;
    public float scrollSpeed = 13.0f; //이게 어쩌다보니 전체 스피드를 관리하는 주요 변수가 됨.. 지금 바꾸기에는 늦었다 좃망;
    public float beforeScrollSpeed;
    public int playerGas;
    public int playerFever;

    public bool gameOver = false;

    public GameObject UIControl;
	// Use this for initialization
	void Start () {
        //GroundScroll를 받아온다.
        
        groundScrolling = GameObject.FindGameObjectsWithTag("Ground");
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameOver)//게임이 안끝났으면
        {
            if(gameFlow % 2 == 0) //게임 시작부분일 때의 속도
            {
                WhenStartGameAccelerate(); //점점 가속하기

            }else if(gameFlow % 2 == 1) //게임이 시작하고 있을 때의 속도
            {
                foreach (GameObject ground in groundScrolling)
                {//scrollSpeed를 그라운드에게 넣어 줌
                    ground.GetComponent<GroundScrolling>().scrollSpeed = scrollSpeed;
                }
            }
        }
        else //게임이 끝나면
        {
            GameInputControl();
        }
        //Debug.Log("gameFlow: " + gameFlow);
    }

    void WhenStartGameAccelerate()
    {

        float speed = 0.3f * Time.deltaTime;
        
        foreach (GameObject ground in groundScrolling) //모든 ground에게
        {
            //scrollSpeed를 넘지 않을때까지 스피드를 증가시킨다.
            if(ground.GetComponent<GroundScrolling>().scrollSpeed <= scrollSpeed)
            {
                ground.GetComponent<GroundScrolling>().scrollSpeed += speed;
            }
            else //scrollSpeed를 넘거나 같으면
            {
                beforeScrollSpeed = scrollSpeed;
                ++gameFlow; //gameFlow 1증가
                Debug.Log("gameFlow가 1증가 : " + gameFlow);
            }
           
        }
    }

    void GameInputControl()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
