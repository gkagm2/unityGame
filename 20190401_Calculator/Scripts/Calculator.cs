using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour {
    public int First_Num;
    public int Second_Num;
    public int Result;
	void Start () {
        Result = Calc_Sum(First_Num, Second_Num);
	}
    int Calc_Sum(int FirstNumber, int SecondNumber)
    {
        return FirstNumber + SecondNumber;
    }
	
}
