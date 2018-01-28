using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIIOMan : MonoBehaviour
{
    public GameObject alphasp;
    public GameObject bravosp;
    public GameObject charliesp;
    public GameObject deltasp;
    public Behaviour halo;

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
    public GameObject terminalInput;
    public CommandController CmdController;
    public TurnManager TurnManager;

    private AudioSource Source;
    public AudioClip fourKeys;
    private int soundtick = 0;
    private int TurnTime = 0;
    public const int numOrder = 3;
    private string FixedText = "";
    private string DOrder = "";
    private int ticker = 0;
    private int chargeTransmit = 0;

    private GameObject[,] radioArray;
    private string[,] reportArray;
    private string[,] cmdArray; // array of commands for each soldier
    private string CurrentOrder;
    private int state = 0;
    private string Cverb = "";
    private string Csubject = "";
    private string Cobject = "";
    private string Lsubject = "ALPHA";
    private int Cman = 0;
    private int Lslot = 0;
    private int xt = -1;
    private int yt = -1;
    private GameObject gridcam;
    private GridScript gridscript;
    // Use this for initialization
    void Awake ()
    {
        Source = GetComponent<AudioSource>();

        radioArray = new GameObject[3,4];

        //Populate Radio Array
        radioArray[0,0] = alphaRad1;
	    radioArray[1,0] = alphaRad2;
	    radioArray[2,0] = alphaRad3;
	    radioArray[0,1] = bravoRad1;
	    radioArray[1,1] = bravoRad2;
	    radioArray[2,1] = bravoRad3;
	    radioArray[0,2] = charlieRad1;
	    radioArray[1,2] = charlieRad2;
	    radioArray[2,2] = charlieRad3;
	    radioArray[0,3] = deltaRad1;
	    radioArray[1,3] = deltaRad2;
	    radioArray[2,3] = deltaRad3;
    
        

        cmdArray = new string[4, numOrder];
        for (int i = 0; i < 4; i++)
        {
            for (int j =0; j < numOrder; j++)
            {
                cmdArray[i,j] = "";
            }
        }
        reportArray = new string[4, numOrder];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < numOrder; j++)
            {
                reportArray[i, j] = ">";
            }
        }

    }

    public void ExecuteCmds() {
        reportArray = new string[4, numOrder];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < numOrder; j++)
            {
                reportArray[i, j] = ">";
            }
        }

        for (int j = 0; j < 4; j++)
        {
                for (int i = 0; i < 3; i++)
                {
                    radioArray[i, j].GetComponent<Text>().text = cmdArray[j, i];
                }
        }

        state = 11;
        chargeTransmit = 0;
        TurnTime = 0;
        Cobject = "";
        Cverb = "";
        Csubject = "";
        DOrder = "";
        CmdController.UpdateCommands(cmdArray);
        TurnManager.EnterTurn(Turn.PlayerMoving);
    }
 /*   private void FixedUpdate()
    {
        if (state == 11) TurnTime += 1;
    }*/
    // Update is called once per frame
    void Update() {
        int[] emptyslot;
        emptyslot = new int[4];
        GameObject gridcam = GameObject.Find("AOCamera");
        GridScript gridscript = gridcam.GetComponent<GridScript>();
        //Recieve Intel ##Extract to relevet method##
        //string intel = "";
        //radioArray[0][0].GetComponent<InputField>().enabled = false;   //Disable InputField to allow text display.
        //radioArray[0][0].GetComponent<Text>().text = intel;            //Display text.
        gridscript.mainColor = new Color(0f, 1f, 0f, 0.1f);

        // clear empty orders from the array
        for (int i = 0; i < 4; i++)
        {
            emptyslot[i] = numOrder-1;
            for (int j = 0; j < numOrder - 1; j++)
            {
                if (cmdArray[i, j] == "")
                {
                    emptyslot[i] = Math.Min(emptyslot[i], j);
                    cmdArray[i, j] = cmdArray[i, j + 1];
                    cmdArray[i, j + 1] = "";
                }
            }
        }
        if (state == 0)
        {
            FixedText = "[A/B/C/D]<i>, or </i>[HOLD SPACE]<i> to transmit</i>";
            FixedText = "<color=grey>" + FixedText + "</color>";

            if (Input.GetKeyDown("a")) {
                state = 1;
                Csubject = "ALPHA";
                Cman = 0;
                //            alphasp.GetComponent("Halo").size =5;
                halo = (Behaviour)alphasp.GetComponent("Halo");
                halo.enabled = true;
            }
            if (Input.GetKeyDown("b"))
            {
                state = 1;
                Csubject = "BRAVO";
                Cman = 1;
                halo = (Behaviour)bravosp.GetComponent("Halo");
                halo.enabled = true;
            }
            if (Input.GetKeyDown("c"))
            {
                state = 1;
                Csubject = "CHARLIE";
                Cman = 2;
                halo = (Behaviour)charliesp.GetComponent("Halo");
                halo.enabled = true;
            }
            if (Input.GetKeyDown("d"))
            {
                state = 1;
                Csubject = "DELTA";
                Cman = 3;
                halo = (Behaviour)deltasp.GetComponent("Halo");
                halo.enabled = true;
            }
            if (Input.GetKeyDown("backspace"))
            {
                cmdArray[Cman, Lslot] = "";
                if (Lslot > 0) Lslot = Lslot - 1;
            }
            if (Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.Return))
            {
                state = 10;
                chargeTransmit = 0;
            }
         /*   if (Input.GetKeyDown("a") || Input.GetKeyDown("b") || Input.GetKeyDown("c") || Input.GetKeyDown("d"))
            {
                Source.PlayOneShot(fourKeys);
            }*/
        }
        if (state == 10)
        {
            chargeTransmit += 1;
            FixedText = "<color=yellow>----------------------------------------\nCHARGING TRANSMISSION\n";
            for (int i = 0; i < (chargeTransmit / 5); i++) FixedText += "##";
            FixedText += "</color>";
            if (chargeTransmit == 80)
            {
                ExecuteCmds();
                state = 11;
                chargeTransmit = 0;
                TurnTime = 0;
            }
            if (Input.GetKeyUp("space") || Input.GetKeyUp(KeyCode.Return))
            {
                state = 0;
                DOrder = "TRANSMISSION CANCELLED";
            }
        } else if (state == 11){
            TurnTime += 1;
            FixedText = "<color=red>RECIEVING TRANSMISSION, PLEASE WAIT\n";
            int limitT = 300;
            for (int i = 0; i < (TurnTime / 5); i++) FixedText += "#";
            FixedText += "</color><color=grey>";
            for (int i = 0; i < ((limitT - TurnTime) / 5); i++) FixedText += ".";
            FixedText += "</color>\n<color=red>";
            for (int i = 0; i < (TurnTime / 5); i++) FixedText += "#";
            FixedText += "</color><color=grey>";
            for (int i = 0; i < ((limitT - TurnTime) / 5); i++) FixedText += ".";
            FixedText += "</color>";
            if (TurnTime > limitT) state = 0;
        }
        else if (state < 2)
        {
            if (state == 1)
            {
                FixedText = "[M] <i>move</i> [F] <i>face</i> [E] <i>engage</i> [H] <i>help or</i> [Bksp]";
                FixedText = "<color=grey>" + FixedText + "</color>";
            }

            if (Input.GetKeyDown("backspace"))
            {
                state = 0;
                Csubject = "";
                if (Cman == 0) halo = (Behaviour)alphasp.GetComponent("Halo");
                if (Cman == 1) halo = (Behaviour)bravosp.GetComponent("Halo");
                if (Cman == 2) halo = (Behaviour)charliesp.GetComponent("Halo");
                if (Cman == 3) halo = (Behaviour)deltasp.GetComponent("Halo");
                halo.enabled = false;
                soundtick = 0;
            }

            if (Input.GetKeyUp("m"))
            {
                state = 2;
                soundtick = 0;
                Cverb = "MOVE";
                if ((Csubject == "") && (Lsubject != "")) Csubject = Lsubject;
            }
            if (Input.GetKeyUp("f"))
            {
                state = 2;
                soundtick = 0;
                Cverb = "FACE";
                if ((Csubject == "") && (Lsubject != "")) Csubject = Lsubject;
            }
            if (Input.GetKeyUp("e"))
            {
                state = 2;
                soundtick = 0;
                Cverb = "ENGAGE";
                if ((Csubject == "") && (Lsubject != "")) Csubject = Lsubject;
            }
           /* if (Input.GetKeyUp("u"))
            {
                state = 2;
                Cverb = "USE";
                if ((Csubject == "") && (Lsubject != "")) Csubject = Lsubject;
            } */
            if (Input.GetKeyUp("h"))
            {
                state = 3;
                soundtick = 0;
                Cverb = "HELP";
                if ((Csubject == "") && (Lsubject != "")) Csubject = Lsubject;
            }
        } else if (state == 3)
        {
        
            FixedText = "[A/B/C/D]<i> to select a soldier to assist</i>";
            FixedText = "<color=grey>" + FixedText + "</color>";

            if (Cman == 0) halo = (Behaviour)alphasp.GetComponent("Halo");
            if (Cman == 1) halo = (Behaviour)bravosp.GetComponent("Halo");
            if (Cman == 2) halo = (Behaviour)charliesp.GetComponent("Halo");
            if (Cman == 3) halo = (Behaviour)deltasp.GetComponent("Halo");
            halo.enabled = true;
            if (Input.GetKeyDown("a") && (Cman != 0))
            {
                state = 4;
                Cobject = "ALPHA";
                //            alphasp.GetComponent("Halo").size =5;
                halo = (Behaviour)alphasp.GetComponent("Halo");
                halo.enabled = true;
            }
            if (Input.GetKeyDown("b") && (Cman != 1))
            {
                state = 4;
                Cobject = "BRAVO";
                //            alphasp.GetComponent("Halo").size =5;
                halo = (Behaviour)bravosp.GetComponent("Halo");
                halo.enabled = true;
            }
            if (Input.GetKeyDown("c") && (Cman != 2))
            {
                state = 4;
                Cobject = "CHARLIE";
                //            alphasp.GetComponent("Halo").size =5;
                halo = (Behaviour)charliesp.GetComponent("Halo");
                halo.enabled = true;
            }
            if (Input.GetKeyDown("d") && (Cman != 3))
            {
                state = 4;
                Cobject = "DELTA";
                //            alphasp.GetComponent("Halo").size =5;
                halo = (Behaviour)deltasp.GetComponent("Halo");
                halo.enabled = true;
            }
            if (Input.GetKeyDown("backspace"))
            {
                state = 1;
                Cverb = "";
            }
        } else if (state == 4)
        {
  
                FixedText = "[ENTER]<i> to finish order</i>";
            FixedText = "<color=grey>" + FixedText + "</color>";

            if (Input.GetKeyDown("backspace"))
            {
                halo.enabled = false;
                state = 3;
                Cobject = "";
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                halo.enabled = false;
                state = 99;
            }
        }
        else if (state == 2)
        {
            if (Cverb == "ENGAGE")
            {
                FixedText = "[ENTER]<i> to finish order</i>";
            }
            else
            {
                FixedText = "<i>Type coordinates then press</i> [ENTER]";
            }
            FixedText = "<color=grey>" + FixedText + "</color>";

            if (Cman == 0) halo = (Behaviour)alphasp.GetComponent("Halo");
            if (Cman == 1) halo = (Behaviour)bravosp.GetComponent("Halo");
            if (Cman == 2) halo = (Behaviour)charliesp.GetComponent("Halo");
            if (Cman == 3) halo = (Behaviour)deltasp.GetComponent("Halo");
            halo.enabled = true;
            if (Cverb != "ENGAGE") gridscript.mainColor = new Color(0f, 1f, 0f, 0.7f);
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
                } else if (((c == '\n') || (c == '\r')) && ((Cverb == "ENGAGE")|| (Cobject.Length > 1)))  // enter/return
                {
                    state = 99;
                }
                else if ((Cverb != "ENGAGE") && (((Cobject == "") && (c - 'a' > -1) && (c - 'z' < 1)) || ((Cobject!= "") && (c - '0' > -1) && (c - '9' < 1)) ))
                {
                    soundtick = 0;
                    Cobject = (Cobject +c).ToUpper();
                }
            }
            
            if (Cobject.Length > 0)
            {
               xt = Cobject.ToUpper().ToCharArray()[0] - 'A';
                //int y = int.Parse(gridCoord.Substring(1)) - 1;
                //if (gridscript.xSelected < xt) gridscript.xSelected += 1;
            } else
            {
                xt = -1;
            }
            if (Cobject.Length > 1)
            {
              yt = int.Parse(Cobject.Substring(1)) - 1;
                //int y = int.Parse(gridCoord.Substring(1)) - 1;
                //if (gridscript.ySelected < yt) gridscript.ySelected += 1;
            } else
            {
                yt = -1;
            }
            

        } else if (state == 99)
        {
            // done?
            Lsubject = Csubject;
            Lslot = emptyslot[Cman];
            cmdArray[Cman, emptyslot[Cman]] = CurrentOrder;
            Csubject = "";
            Cverb = "";
            Cobject = "";
            DOrder = "[DONE----]";
            state = 0;
            xt = -1;
            yt = -1;
            gridscript.xSelected = -1;
            gridscript.ySelected = -1;
            if (Cman == 0) halo = (Behaviour)alphasp.GetComponent("Halo");
            if (Cman == 1) halo = (Behaviour)bravosp.GetComponent("Halo");
            if (Cman == 2) halo = (Behaviour)charliesp.GetComponent("Halo");
            if (Cman == 3) halo = (Behaviour)deltasp.GetComponent("Halo");
            halo.enabled = false;
        }
        if (Cobject != "")
        {
            CurrentOrder = Csubject + " " + Cverb + " " + Cobject;
        }
        else if (Cverb != "")
        {
            CurrentOrder = Csubject + " " + Cverb;
        }
        else if (Csubject != "")
        {
            CurrentOrder = Csubject;
        }
        else CurrentOrder = "";


        //Update UI
        ticker += 1;
        if (ticker == 4)
        {
            ticker = 0;
            if (gridscript.xSelected != xt) gridscript.xSelected += Math.Sign(xt - gridscript.xSelected);
            if (Math.Abs(gridscript.ySelected - yt) > 5) gridscript.ySelected += 5* Math.Sign(yt - gridscript.ySelected);
            if (gridscript.ySelected != yt) gridscript.ySelected += Math.Sign(yt - gridscript.ySelected);
            if (Math.Abs(gridscript.xSelected - xt) > 5) gridscript.xSelected += 5 * Math.Sign(xt - gridscript.xSelected);
            //    gridscript.ySelected = yt;
            if (DOrder != CurrentOrder + "_")
            {
                if ((DOrder.Length - 1) > CurrentOrder.Length)
                {
                    DOrder = DOrder.Substring(0, DOrder.Length - 2) + "_";
                    if (soundtick == 0)
                    {
                        Source.PlayOneShot(fourKeys);
                    }
                    soundtick += 1;
                    if (soundtick == 3) soundtick = 0;
                }
                else
                {
                    DOrder = CurrentOrder.Substring(0, DOrder.Length) + "_";
                    if (soundtick == 0)
                    {
                        Source.PlayOneShot(fourKeys);
                    }
                    soundtick += 1;
                    if (soundtick == 3) soundtick = 0;
                }

            }
        }
        //Update Input
        terminalInput.GetComponent<Text>().text =FixedText+"\n"+ DOrder;

		if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey((KeyCode.Backspace))) {
            //Update Output
            for (int j = 0; j < 4; j++)
            {
                if ((cmdArray[j, 0]) == "")
                {
                    for (int i = 0; i < 3; i++)
                    {



                        radioArray[i, j].GetComponent<Text>().text = reportArray[j, i];
                        //    if (!string.IsNullOrEmpty(cmdArray[j, i]))
                        //    {
                        //        var str = cmdArray[j, i].Split();
                        //        var message = str[0] + "<color=yellow> " + str[1] + "</color> " + str[2];


                        //        //radioArray[i,j].GetComponent<Text>().text = cmdArray[j,i];
                        //        radioArray[i, j].GetComponent<Text>().text = message;
                        //    }
                        //    else
                        //    {
                        //        radioArray[i, j].GetComponent<Text>().text = cmdArray[j, i];
                        //    }
                        //}
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {



                        radioArray[i, j].GetComponent<Text>().text = "> " + cmdArray[j, i];
                        //    if (!string.IsNullOrEmpty(cmdArray[j, i]))
                        //    {
                        //        var str = cmdArray[j, i].Split();
                        //        var message = str[0] + "<color=yellow> " + str[1] + "</color> " + str[2];


                        //        //radioArray[i,j].GetComponent<Text>().text = cmdArray[j,i];
                        //        radioArray[i, j].GetComponent<Text>().text = message;
                        //    }
                        //    else
                        //    {
                        //        radioArray[i, j].GetComponent<Text>().text = cmdArray[j, i];
                        //    }
                        //}
                    }
                }
            }
		}
	}

	public void Report(List<string> reports, string soldierName) {
		int soldierId = 0;
        string col = "";
		if (soldierName.ToUpper() == "ALPHA") {
			soldierId = 0;
            col = "cyan";
		} else if (soldierName.ToUpper() == "BRAVO") {
			soldierId = 1;
            col = "red";
		} else if (soldierName.ToUpper() == "CHARLIE") {
			soldierId = 2;
            col = "green";
		} else if (soldierName.ToUpper() == "DELTA") {
			soldierId = 3;
            col = "yellow";
		}

		int i = 0;
		foreach (var report in reports) {
            reportArray[soldierId, i % radioArray.GetLength(0)] = "<color=" + col + ">|" + report + "|</color>";

            radioArray[i % radioArray.GetLength(0), soldierId].GetComponent<Text>().text ="<color="+col+">|"+ report+"|</color>";
			i++;
		}
	}

    public void ClearOrders()
    {
        foreach(var element in radioArray)
        {
            element.GetComponent<Text>().text = string.Empty;
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < numOrder; j++)
            {
                cmdArray[i, j] = "";
            }
        }
    }
}
