using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System.ComponentModel;

public class ShutdownManager : MonoBehaviour
{
    public Text timerDisplayText;
    public InputField hourInputField;
    public InputField minuteInputField;
    public Button startBtn;
    public Button stopBtn;
    public GameObject alarmPopup;

    [SerializeField]
    private bool isTimerOn;
    private int m_iHour;
    private int m_iMinute;

    [Header("Alarm Description")]
    [SerializeField] private string inputTime = null;

    /// <summary>
    /// timer start
    /// </summary>
    public void OnClick_StartBtn()
    {
        if (isTimerOn == true)
        {
            OnClick_StopBtn();
        }

        SetTimer();
        if(m_iHour == 0 && m_iMinute == 0)
        {
            alarmPopup.SetActive(true);
            alarmPopup.GetComponent<AlarmPopup>().SetDescription(inputTime);
            return;
        }

        isTimerOn = true;
        int second = m_iHour * 3600 + m_iMinute * 60;
        var psi = new ProcessStartInfo("shutdown", "/s /f /t " + second);
        psi.CreateNoWindow = true;
        psi.UseShellExecute = false;
        Process.Start(psi);

        timerDisplayText.text = m_iHour + "시간" + m_iMinute + "분 후 종료됩니다.";
    }

    /// <summary>
    /// timer stop
    /// </summary>
    public void OnClick_StopBtn()
    {
        isTimerOn = false;

        var psi = new ProcessStartInfo("shutdown", "/a");
        psi.CreateNoWindow = true;
        psi.UseShellExecute = false;
        Process.Start(psi);

        timerDisplayText.text = "타이머를 취소하였습니다.";
    }

    /// <summary>
    /// Timer setting
    /// </summary>
    private void SetTimer()
    {
        if (hourInputField.text == "")
        {
            m_iHour = 0;
        }
        else
        {
            m_iHour = int.Parse(hourInputField.text);
        }
        if (minuteInputField.text == "")
        {
            m_iMinute = 0;
        }
        else
        {
            m_iMinute = int.Parse(minuteInputField.text);
        }
    }
}