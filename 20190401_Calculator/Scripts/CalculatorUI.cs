using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CalculatorUI : MonoBehaviour {
    int stackNumber;
    string numbers;
    string oper;
    public Text resultScreenText;
    // Use this for initialization
    void Start () {
        stackNumber = 0;
        oper = "";
        numbers = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClickBtn1()
    {
        numbers += "1";
        Debug.Log("stackNumber : " + stackNumber + " numbers : " + numbers);
        resultScreenText.text = numbers;
    }
    public void OnClickBtn2()
    {
        numbers += "2";
        Debug.Log("stackNumber : " + stackNumber + " numbers : " + numbers);
        resultScreenText.text = numbers;

    }
    public void OnClickBtn3()
    {
        numbers += "3";
        Debug.Log("stackNumber : " + stackNumber + " numbers : " + numbers);
        resultScreenText.text = numbers;

    }
    public void OnClickBtn4()
    {
        numbers += "4";
        Debug.Log("stackNumber : " + stackNumber + " numbers : " + numbers);
        resultScreenText.text = numbers;

    }
    public void OnClickBtn5()
    {
        numbers += "5";
        Debug.Log("stackNumber : " + stackNumber + " numbers : " + numbers);
        resultScreenText.text = numbers;

    }
    public void OnClickBtn6()
    {
        numbers += "6";
        Debug.Log("stackNumber : " + stackNumber + " numbers : " + numbers);
        resultScreenText.text = numbers;

    }
    public void OnClickBtn7()
    {
        numbers += "7";
        Debug.Log("stackNumber : " + stackNumber + " numbers : " + numbers);
        resultScreenText.text = numbers;

    }
    public void OnClickBtn8()
    {
        numbers += "8";
        Debug.Log("stackNumber : " + stackNumber + " numbers : " + numbers);
        resultScreenText.text = numbers;

    }
    public void OnClickBtn9()
    {
        numbers += "9";
        Debug.Log("stackNumber : " + stackNumber + " numbers : " + numbers);
        resultScreenText.text = numbers;

    }
    public void OnClickBtn0()
    {
        numbers += "0";
        Debug.Log("stackNumber : " + stackNumber + " numbers : " + numbers);
        resultScreenText.text = numbers;

    }
    public void OnClickPlus()
    {
        Debug.Log("stackNumber : " + stackNumber + " numbers : " + numbers);
        if (stackNumber == 0)
        {
            stackNumber = Convert.ToInt32(numbers);
            numbers = "";
        }
        oper = "+";
        if (numbers != "")
        {
            stackNumber = CalcNum(stackNumber, Convert.ToInt32(numbers), oper);
            numbers = "";
        }   
        

    }
    public void OnClickMinus()
    {
        Debug.Log("stackNumber : " + stackNumber + " numbers : " + numbers);
        if (stackNumber == 0)
        {
            stackNumber = Convert.ToInt32(numbers);
            numbers = "";
        }
        oper = "-";
        if (numbers != "")
        {
            stackNumber = CalcNum(stackNumber, Convert.ToInt32(numbers), oper);
            numbers = "";
        }
        

    }
    public void OnClickDivide()
    {
        Debug.Log("stackNumber : " + stackNumber + " numbers : " + numbers);
        if (stackNumber == 0)
        {
            stackNumber = Convert.ToInt32(numbers);
            numbers = "";
        }
        oper = "/";
        if (numbers != "")
        {
            stackNumber = CalcNum(stackNumber, Convert.ToInt32(numbers), oper);
            numbers = "";
        }
    }
    public void OnClickMultiply()
    {
        Debug.Log("stackNumber : " + stackNumber + " numbers : " + numbers);
        if (stackNumber == 0)
        {
            stackNumber = Convert.ToInt32(numbers);
            numbers = "";
        }
        oper = "*";
        if (numbers != "")
        {
            stackNumber = CalcNum(stackNumber, Convert.ToInt32(numbers), oper);
            numbers = "";
        }
    }
    public void OnClickResult()
    {
        
        stackNumber = CalcNum(stackNumber, Convert.ToInt32(numbers), oper);
        
        Debug.Log("stackNumber : " + stackNumber + " numbers : " + numbers);
        numbers = "";
        resultScreenText.text = stackNumber.ToString();
        print(stackNumber);
    }
    public void OnClickInit()
    {
        numbers = "";
        stackNumber = 0;
        oper = "";
        Debug.Log("stackNumber : " + stackNumber + " numbers : " + numbers);
        resultScreenText.text = stackNumber.ToString();

    }
    public int CalcNum(int a, int b, string oper)
    {
        if (oper == "+")
            return a + b;
        else if (oper == "-")
            return a - b;
        else if (oper == "*")
            return a * b;
        else if (oper == "/")
            return a / b;
        return 0;

    }
}
