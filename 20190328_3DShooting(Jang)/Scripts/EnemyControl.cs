using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
    public float Speed = 4.0f;
    public GameObject explosion;
	
	// Update is called once per frame
	void Update () {
        float moveAmt = Speed * Time.deltaTime;
        transform.Translate(Vector3.down * moveAmt, Space.World); // Space.World는 월드좌표, Space.self는 로컬좌표


        if (transform.position.y < -4.0f)
        {
            Debug.Log("Main.Lives : " + Main.Lives);
            if (Main.Lives < 0)
            {
                //Game Over (Exit)
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false; // play모드를 false로.
                #elif UNITY_WEBPLAYER
                    Application.OpenURL("http://google.com");
                #else
                    Application.Quit();
                #endif
            }
            else
                Lives();
        }
        // 원래위치로 
        if(transform.position.y < -7.0f)
        {
            InitPosition();
        }

	}
    IEnumerator Lives()
    {
        while (true)
        {
            Debug.Log("IEnumerator Lives : ");
            yield return --Main.Lives;
        }
        
    }



    void InitPosition()
    {
        //랜덤으로 받아오깅 : Random.Range(min, max)
        transform.position = new Vector3(Random.Range(-5.0f, 5.0f),7.0f, 0.0f);

    }
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("TriggerEnter start");
        if (other.gameObject.tag == "bullet")
        {
            //get score
            Main.Score += 100;

            //audio
            GetComponent<AudioSource>().Play();

            //position
            Instantiate(explosion, transform.position, transform.rotation);
            InitPosition();
        }
        
    }
    
}
