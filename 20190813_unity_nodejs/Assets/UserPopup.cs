using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UserPopup : MonoBehaviour {
    

    public InputField nameInputField;

    public void OnClick_OKBtn()
    {
        if (nameInputField.text == "")
            return;

        UserManager.userName = nameInputField.text;
        UserManager.Save();
        
        Debug.Log(UserManager.userName);
        NetworkManager.instance.CheckUserExisted(nameInputField.text);
        //UIManager.instance.userNameText.text = nameInputField.text;
        gameObject.SetActive(false);
    }
}
