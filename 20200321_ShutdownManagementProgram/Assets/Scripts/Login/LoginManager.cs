using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoginManager : MonoBehaviour
{
    public InputField passwordInputField;
    public GameObject alarmPopup;
    public GameObject newPasswordRegistPopup;

    public string sceneName;


    [Header("Alarm Description")]
    [SerializeField]    private string wrongPassword = null;
    [SerializeField]    private string emptyPassword = null;

    private void Start()
    {
        LoadInfo();
    }

    /// <summary>
    /// Load information
    /// </summary>
    private void LoadInfo()
    {
        if (!PlayerPrefs.HasKey("password"))
        {
            newPasswordRegistPopup.SetActive(true);
        }
        else
        {
            newPasswordRegistPopup.SetActive(false);
        }
    }

    /// <summary>
    /// When login button clicked
    /// </summary>
    public void OnClick_LoginBtn()
    {
        if(passwordInputField.text == "")
        {
            alarmPopup.SetActive(true);
            alarmPopup.GetComponent<AlarmPopup>().SetDescription(emptyPassword);

        }
        else if (!IsCorrectedPassword(passwordInputField.text))
        {
            alarmPopup.SetActive(true);
            alarmPopup.GetComponent<AlarmPopup>().SetDescription(wrongPassword);
        }
        else // correct
        {
            SceneManager.LoadScene(sceneName);
        }
        passwordInputField.text = "";
    }

    /// <summary>
    /// Confirm correct password
    /// </summary>
    /// <param name="_password">Returns true if the password is correct</param>
    /// <returns></returns>
    private bool IsCorrectedPassword(string _password)
    {
        string password = PlayerPrefs.GetString("password");
        if (password == _password)
        {
            return true;
        }
        return false;
    }
}
