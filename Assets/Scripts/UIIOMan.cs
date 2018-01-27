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

    private GameObject[][] radioArray;


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
	}
	
	// Update is called once per frame
	void Update () {

        //Recieve Intel ##Extract to relevet method##
	    string intel = "";
	    radioArray[0][0].GetComponent<InputField>().enabled = false;   //Disable InputField to allow text display.
	    radioArray[0][0].GetComponent<Text>().text = intel;            //Display text.
	}
}
