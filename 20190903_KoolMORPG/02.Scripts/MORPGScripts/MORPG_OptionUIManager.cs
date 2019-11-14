using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Option UI Manager
/// </summary>
public class MORPG_OptionUIManager : MonoBehaviour
{
    /// <summary>
    /// Main 화면으로 이동한다.
    /// </summary>
    public void OnClick_BackToMainBtn()
    {
        SceneMoveManager.LoadScene("MainScene");
    }
}