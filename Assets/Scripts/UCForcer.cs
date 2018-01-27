using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UCForcer : MonoBehaviour
{

    public InputField inputField;

    public void Start()
    {
        inputField.text = "ISSUE ORDER.";
       //Add Listener to call ValChanged
        inputField.onValueChange.AddListener(delegate {ValChanged(); });
    }

    public void ValChanged()
    {
        inputField.text = inputField.text.ToUpper();
        Debug.Log("test");
    }
}
