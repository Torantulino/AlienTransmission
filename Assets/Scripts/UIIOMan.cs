using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class script : MonoBehaviour
{
    public GameObject alphaRad1;
    public GameObject alphaRad2;
    public GameObject alphaRad3;
    public GameObject bravoRad1;
    public GameObject bravoRad2;
    public GameObject bravoRad3;
    public GameObject charlieRad1;
    public GameObject charlieRad2;
    public GameObject charlieRad3;
    public GameObject deltaRad1;
    public GameObject deltaRad2;
    public GameObject deltaRad3;

    public const int numOrder = 3;

    private GameObject[][] radioArray;
    private string[,] cmdArray; // array of commands for each soldier
    private string CurrentOrder;
    private int state = 0;
    private string Cverb = "";
    private string Csubject = "";
    private string Cobject = "";
    private string Lsubject = "ALPHA";
    private int Cman = 0;
    private int Lslot = 0;
    // Use this for initialization
    void Start () {
        //Use this to access text: alphaRad1.InputField.text
        //Use this to set text (feedback from Agents): SEE BELOW

        //Populate Radio Array
        radioArray[0][0] = alphaRad1;
	    radioArray[1][0] = alphaRad2;
	    radioArray[2][0] = alphaRad3;
	    radioArray[0][1] = bravoRad1;
	    radioArray[1][1] = bravoRad2;
	    radioArray[2][1] = bravoRad3;
	    radioArray[0][2] = charlieRad1;
	    radioArray[1][2] = charlieRad2;
	    radioArray[2][2] = charlieRad3;
	    radioArray[0][3] = deltaRad1;
	    radioArray[1][3] = deltaRad2;
	    radioArray[2][3] = deltaRad3;
        cmdArray = new string[4, numOrder];
        for (int i = 0; i < 4; i++)
        {
            for (int j =0; j < numOrder; j++)
            {
                cmdArray[i,j] = "";
            }
        }
       

	}
	
	// Update is called once per frame
	void Update () {
        int[] emptyslot;

        //Recieve Intel ##Extract to relevet method##
	    string intel = "";
	    radioArray[0][0].GetComponent<InputField>().enabled = false;   //Disable InputField to allow text display.
	    radioArray[0][0].GetComponent<Text>().text = intel;            //Display text.


        // clear empty orders from the array
        for (int i = 0; i < 4; i++)
        {
            emptyslot[i] = 99;
            for (int j = 0; j < numOrder - 1; j++)
            {
                if (cmdArray[i,j] == "")
                {
                    emptyslot[i] = Math.Min(emptyslot[i], j);
                    cmdArray[i,j] = cmdArray[i,j + 1];
                    cmdArray[i,j + 1] = "";
                }
            }
        }
        if (state == 0)
        {
            if (Input.GetKeyDown("a")) {
                state = 1;
                Csubject = "ALPHA";
                Cman = 0;
            }
            if (Input.GetKeyDown("b"))
            {
                state = 1;
                Csubject = "BETA";
                Cman = 1;
            }
            if (Input.GetKeyDown("c"))
            {
                state = 1;
                Csubject = "CHARLIE";
                Cman = 2;
            }
            if (Input.GetKeyDown("d"))
            {
                state = 1;
                Csubject = "DELTA";
                Cman = 3;
            }
            if (Input.GetKeyDown("backspace"))
            {
                cmdArray[Cman, Lslot] = "";
            }

        }
        if (state < 2)
        {
            if (Input.GetKeyDown("backspace"))
            {
                state = 0;
                Csubject = "";
            }

            if (Input.GetKeyUp("m"))
            {
                state = 2;
                Cverb = "MOVE";
                if ((Csubject == "") && (Lsubject != "")) Csubject = Lsubject;
            }
            if (Input.GetKeyUp("f"))
            {
                state = 2;
                Cverb = "FACE";
                if ((Csubject == "") && (Lsubject != "")) Csubject = Lsubject;
            }
            if (Input.GetKeyUp("e"))
            {
                state = 2;
                Cverb = "ENGAGE";
                if ((Csubject == "") && (Lsubject != "")) Csubject = Lsubject;
            }
            if (Input.GetKeyUp("u"))
            {
                state = 2;
                Cverb = "USE";
                if ((Csubject == "") && (Lsubject != "")) Csubject = Lsubject;
            }
            if (Input.GetKeyUp("h"))
            {
                state = 2;
                Cverb = "HELP";
                if ((Csubject == "") && (Lsubject != "")) Csubject = Lsubject;
            }
        } else if (state == 2)
        {
            foreach (char c in Input.inputString)
            {
                if (c == '\b') // has backspace/delete been pressed?
                {
                    if (Cobject.Length != 0)
                    {
                        Cobject = Cobject.Substring(0, Cobject.Length - 1);
                    } else
                    {
                        state = 1;
                        Cverb = "";
                    }
                } else if ((c == '\n') || (c == '\r')) // enter/return
                {
                    // done?
                    Lsubject = Csubject;
                    Lslot = emptyslot[Cman];
                    cmdArray[Cman, emptyslot[Cman]] = CurrentOrder;
                    Csubject = "";
                    Cverb = "";
                    Cobject = "";
                    state = 0;
                } else
                {
                    Cobject += c;
                }
            }
        }

        CurrentOrder = Csubject + " " + Cverb + " " + Cobject;

    }
}
