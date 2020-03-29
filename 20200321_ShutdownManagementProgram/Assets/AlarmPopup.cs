using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmPopup : MonoBehaviour
{
    public Text descriptionText;

    /// <summary>
    /// Setting alarm of description text
    /// </summary>
    /// <param name="_description">description text</param>
    public void SetDescription(string _description)
    {
        descriptionText.text = _description;
    }
}