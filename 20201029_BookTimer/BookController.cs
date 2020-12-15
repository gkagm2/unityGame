using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BookController : MonoBehaviour
{
    [SerializeField] private Button playBtn = null;
    [SerializeField] private Button stopBtn = null;
    [SerializeField] private Button waitBtn = null;

    [SerializeField] private Text startTimeText = null;
    [SerializeField] private Text totalTimeText = null;

    [SerializeField] private InputField timerInp = null;
    [SerializeField] private Text timerText = null;
    [SerializeField] private Toggle soundMuteToggle = null;
    public AudioSource beepAudio = null;

    private bool isStart = false;
    private bool isTs = false;
    private float timer;
    public float defaultTimer = 30f;
    public float maxTimer;

    private DateTime startTime;
    private DateTime endTime;
    private TimeSpan totalTimeSpan;
    private void Start()
    {
        isStart = false;
        isTs = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isStart == false)
            {
                OnClick_Play();
            }
            else
            {
                OnClick_Wait();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            OnClick_Stop();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (isStart)
        {
            if(isTs == false)
                timer -= Time.deltaTime;

            if(timer < 0)
            {
                if(!soundMuteToggle.isOn)
                    beepAudio.Play();
                timer = maxTimer;
            }
        }

        if (isTs == false && isStart)
            timerText.text = Mathf.Ceil(timer).ToString();
    }

    public void OnClick_Play()
    {
        if (string.IsNullOrEmpty(timerInp.text))
            timer = defaultTimer;
        else
        {
            maxTimer = int.Parse(timerInp.text);
            timer = maxTimer;
        }
        isStart = true;
        waitBtn.gameObject.SetActive(true);
        startTime = DateTime.Now;
        startTimeText.text = startTime.ToString();
    }

    public void OnClick_Stop()
    {
        timer = defaultTimer;
        isStart = false;
        isTs = false;
        Text waitBtnText = waitBtn.GetComponentInChildren<Text>();
        waitBtnText.text = "일시정지(Enter)";
        timerInp.text = "";
        timerText.text = defaultTimer.ToString();
        waitBtn.gameObject.SetActive(false);
        startTimeText.text = "";
        totalTimeText.text = "";
    }

    public void OnClick_Wait()
    {
        Text waitBtnText = waitBtn.GetComponentInChildren<Text>();
        if (isTs == true)
        {
            isTs = false;
            waitBtnText.text = "일시정지(Enter)";
            startTime = DateTime.Now;
        }
        else if (isTs == false)
        {
            isTs = true;
            waitBtnText.text = "시작(Enter)";
            endTime = DateTime.Now;
            TimeSpan timeSpan = endTime - startTime;
            totalTimeSpan += timeSpan;
            startTime = DateTime.Now;
            totalTimeText.text = totalTimeSpan.ToString();
        }
    }
}
