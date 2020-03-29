using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordChange : MonoBehaviour
{
    public Text questionText;
    public InputField answerInputField;

    public InputField passwordInputField;
    public InputField passwordConfirmInputField;
    public GameObject alarmPopup;

    [Header("Alarm Description")]
    [SerializeField]    private string wrongAnswer = null;
    [SerializeField]    private string notSamePassword = null;
    [SerializeField]    private string emptyPassword = null;
    [SerializeField]    private string completeChangePassword = null;

    private int m_iAnswer;

    private void OnEnable()
    {
        GenerateQuestion();
    }

    public void OnClick_ChangePasswordBtn()
    {
        string m_strPassword = passwordInputField.text;
        string m_strPasswordConfirm = passwordConfirmInputField.text;

        if (!IsCorrectAnswer())
        {
            alarmPopup.SetActive(true);
            alarmPopup.GetComponent<AlarmPopup>().SetDescription(wrongAnswer);
        }
        else if (m_strPassword == "" || m_strPasswordConfirm == "")
        {
            alarmPopup.SetActive(true);
            alarmPopup.GetComponent<AlarmPopup>().SetDescription(emptyPassword);
        }
        else if (m_strPassword != m_strPasswordConfirm)
        {
            alarmPopup.SetActive(true);
            alarmPopup.GetComponent<AlarmPopup>().SetDescription(notSamePassword);
        }
        else // correct
        {
            alarmPopup.SetActive(true);
            alarmPopup.GetComponent<AlarmPopup>().SetDescription(completeChangePassword);
            PlayerPrefs.SetString("password", m_strPassword);
            gameObject.SetActive(false);
        }
        GenerateQuestion();
        InitInputField();
    }

    private bool IsCorrectAnswer()
    {
        int iAnswer;
        if (answerInputField.text == "")
        {
            iAnswer = 0;
        }
        else
        {
            iAnswer = int.Parse(answerInputField.text);
        }

        if(iAnswer == m_iAnswer)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Generate question
    /// </summary>
    private void GenerateQuestion()
    {
        int iA = UnityEngine.Random.Range(11, 99);
        int iB = UnityEngine.Random.Range(2, 9);
        int iC = UnityEngine.Random.Range(11, 99);

        questionText.text = iA.ToString() + " * " + iB.ToString() + " + " + iC.ToString();
        m_iAnswer = iA * iB + iC;
    }


    private void InitInputField()
    {
        passwordInputField.text = "";
        passwordConfirmInputField.text = "";
        answerInputField.text = "";
    }
}
