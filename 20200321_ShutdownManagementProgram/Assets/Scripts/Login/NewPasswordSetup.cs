using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPasswordSetup : MonoBehaviour
{
    public InputField passwordInputField;
    public InputField passwordConfirmInputField;
    public GameObject alarmPopup;

    [Header("Alarm Description")]
    [SerializeField]    private string notSamePassword = null;
    [SerializeField]    private string emptyPassword = null;
    [SerializeField]    private string completeGeneratePassword = null;

    private string m_strPassword;
    private string m_strPasswordConfirm;

    public void OnClick_GeneratePasswordBtn()
    {
        m_strPassword = passwordInputField.text;
        m_strPasswordConfirm = passwordConfirmInputField.text;


        if(m_strPassword == "" || m_strPasswordConfirm == "")
        {
            alarmPopup.SetActive(true);
            alarmPopup.GetComponent<AlarmPopup>().SetDescription(emptyPassword);
        }
        else if(m_strPassword != m_strPasswordConfirm)
        {
            alarmPopup.SetActive(true);
            alarmPopup.GetComponent<AlarmPopup>().SetDescription(notSamePassword);
        }
        else // correct
        {
            PlayerPrefs.SetString("password", m_strPassword);
            alarmPopup.SetActive(true);
            alarmPopup.GetComponent<AlarmPopup>().SetDescription(completeGeneratePassword);
            gameObject.SetActive(false);
        }
    }
}
