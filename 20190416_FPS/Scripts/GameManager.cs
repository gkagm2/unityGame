
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public GameObject gameMenu;
    public bool gameMenuflag = false;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape)) //누르면
        {
            TurnOnOffMenu(); //메뉴 작동

        }
    }
    public void TurnOnOffMenu()
    {
        gameMenuflag = gameMenuflag ? false : true; //플레그가 반대로 바뀐다.

        if (gameMenuflag) //게임 메뉴를 키면
        {
            gameMenu.SetActive(true);  //메뉴를 보여주고
            PauseScript.SetPause(true); //멈춘다.
        }
        else // 게임 메뉴를 끄면
        {
            gameMenu.SetActive(false);  //메뉴를 끄고
            PauseScript.SetPause(false); //Pause를 해제한다.
        }
    }
}
