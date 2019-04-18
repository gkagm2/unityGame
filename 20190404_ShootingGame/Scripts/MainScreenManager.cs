using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScreenManager : MonoBehaviour {
    
    public void OnClickStartBtn()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
