using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUISelectionGrid : MonoBehaviour {

    private int selectionGridInt = 0;
    private string[] selectionStrings = { "Grid 1", "Grid 2", " Grid 3" };

    private void OnGUI()
    {
        selectionGridInt = GUI.SelectionGrid(new Rect(25, 25, 300, 60), selectionGridInt, selectionStrings,72);
        print(selectionGridInt);

    }



}
