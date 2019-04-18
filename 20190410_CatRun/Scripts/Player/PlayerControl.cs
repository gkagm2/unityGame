using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    

    // info
    short beforePlayerLife;

    // Jump
    public float jumpTime;
    public float []coolTime;

    byte jumpCount = 0;
    public byte jumpMaxCount = 2;
    public float jumpSpeed = 0.5f;
    public float fallSpeed = 0.5f;

    public bool isJump = false;
    Transform beforeTransform;

    // Animation
    public Animation playerAnimation;

    // explosion
    public GameObject explosion;
    public GameObject itemExplosion;
    public PlayerInfoScript playerinfo;

    // shield
    public ShieldScript shield;
    public float shieldTime = 1.0f;
    public bool shieldOn = false;

    // game manager.
    GameManager gameManager;

    void Start()
    {
        shield = transform.Find("Shield").GetComponent<ShieldScript>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        beforePlayerLife = playerinfo.life;
        coolTime[0] = jumpTime;
        coolTime[1] = 0;
        beforeTransform = transform;
        playerAnimation = transform.Find("kitten").GetComponent<Animation>();
        playerAnimation["Run"].speed = 0.7f;

    }

    void Update()
    {
        if (!gameManager.gameOver)
        {
        }



    }



    //플레이어 사이즈 변경
    void PlayerSizeChange(float size = 0.5f)
    {
        //사이즈 변경
        transform.localScale += new Vector3(size, size, size);
    }
    //점프
    public void Jump(KeyCode jumpKeyCode)
    { /// TODO : 땅이 움직이는 작동을 이 스크립트에서 하고 있음 Ground 스크립트에서 움직일 수 있도록 바꿔라
        if (!isJump) // 점프가 아니면
        {
            if (transform.position.y > 0) //공중에 뛰어져 있는 상태면 
                transform.Translate(Vector3.down * fallSpeed * Time.deltaTime); //떨어진다. 
            else
            {
                playerAnimation.Play("Run"); //뛰는 애니메이션으로 바꿈
                jumpCount = 0; // 점프 횟수를 0으로 바꿈
                if(playerinfo.life < beforePlayerLife) 
                {//땅 크기를 바꿈.
                    beforePlayerLife = playerinfo.life;
                    
                }
            }
        }
        else // 점프면
        {
            if (jumpCount < jumpMaxCount) //점프가 jumpMaxCount를 넘기지 않았으면
            { }
            coolTime[0] -= Time.deltaTime; //초동안 위로 올라감
            if (coolTime[0] > 0) //coolTime이 0이 되기전까지  
                transform.Translate(Vector3.up * jumpSpeed * Time.deltaTime); //위로 이동
            else //끝나면
                isJump = false; //점프 false로 바꾸고

            if (jumpCount > jumpMaxCount) 
                isJump = false;
        }

        if (Input.GetKeyDown(jumpKeyCode)) //스페이스 바를 누르면
        {
            if (jumpCount < jumpMaxCount) //점프가 점프 Max보다 적으면
            {
                playerAnimation.Play("Jump"); //점프 애니메이션으로 바꿈
                coolTime[0] = jumpTime; //쿨타임을 초기화 함
            }
            ++jumpCount; //점프가 1 증가한다. 

            //Debug.Log("jumpCount : " + jumpCount);
            beforeTransform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            isJump = true; //점프한다는 상태로 바꾸고
        }
    }

    // 방패들기
    public void PickShield(KeyCode shieldKeyCode)
    {
        if (Input.GetKeyDown(shieldKeyCode)) { 
            shieldOn = true;
        }

        if (shieldOn)
        {
            coolTime[1] += Time.deltaTime;

            //방패가 앞에 놓여짐
            shield.PickShield(transform);


            //시간이 지나면
            if (coolTime[1] > shieldTime)
            {
                shield.transform.position = new Vector3(0, 0, -30.0f);
                shieldOn = false;
                coolTime[1] = 0;
            }
        }
    }
    public void StopAnimationPlayer(string playerName)
    {
        //Debug.Log(playerName + "의 애니메이션이 ");
        //PlayerControl player = GameObject.Find(playerName).GetComponent<PlayerControl>();
        //if (player.playerAnimation.Play("IdleSit"))
        //{
        //    Debug.Log("애니메이션이 잘 바뀜");
        //}
        //else
        //{
        //    Debug.Log("애니메이션이 잘 안바뀜");
        //}  
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (playerinfo.life <= 0)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                gameManager.GameOver();
                Debug.Log("this gameObject Name : " + gameObject.name);
                if(gameObject.name == "Player1") //Player1가 맞은거면
                {
                    StopAnimationPlayer("Player2"); //Player2 애니메이션 행동
                }
                else // Player2가 맞은거면
                {
                    StopAnimationPlayer("Player1"); //Player1 애니메이션 행동함
                }
                Destroy(gameObject);
            }
            //gameManager.EnemySlowlyGenerate();
            --playerinfo.life; //목숨 깎임
            Debug.Log("Player2에서 닿음 : " + playerinfo.life);
        }
        if (collision.gameObject.tag == "Item")
        {
            if(playerinfo.life < playerinfo.maxLife)
            {
                Instantiate(itemExplosion, transform.position, transform.rotation);
                ++playerinfo.life; // 목숨 증가
                
                //gameManager.EnemySlowlyGenerate(-0.3f);
            }
        }
    }

}
