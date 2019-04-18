using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameUIManager : MonoBehaviour {
    //Game Scene
    public Text gameOverText;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.isGameOver)
        {

        }
        else
        {
            ShowGameOverText();
        }
    }
    public void ShowGameOverText()
    {
        if (gameOverText.transform.position.y > 150)
        {

            gameOverText.transform.position -= new Vector3(0, 50.0f * Time.deltaTime, 0);
        }
    }
}
