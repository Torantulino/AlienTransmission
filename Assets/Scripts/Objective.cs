using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    public string WinningText = "MISSION COMPLETE";

    public Text MissionCompleteText;
    public RawImage ActionPanel;
    public TurnManager turnManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SoldierInfo>() != null)
        {
            StartCoroutine(UpdateText());
            ActionPanel.color = Color.HSVToRGB(0f, 0f, 0.39f);

            turnManager.StopAllActions();

            //Make soldier report!
        }
    }

    private IEnumerator UpdateText()
    {
        MissionCompleteText.text = WinningText + " ";
        MissionCompleteText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        MissionCompleteText.text = WinningText + "?";

        yield return new WaitForSeconds(1f);
        GarbleText();
        var canBeGarbled = MissionCompleteText.text.Any(x => char.IsLetter(x));
        while (canBeGarbled)
        {
            GarbleText();
            yield return new WaitForSeconds(0.75f);
            canBeGarbled = MissionCompleteText.text.Any(x => char.IsLetter(x));
        }
    }

    void GarbleText()
    {
        var newText = Garble(MissionCompleteText.text);
        MissionCompleteText.GetComponent<Text>().text = newText;
    }

    string Garble(string original)
    {
        int nlen = 0;
        int pos;

        string result = original;
        // Add randomised distortion to a string.
        string[] sn1 = { "&", "$", "?", "#", "!", "@" };
        string[] sn3 = { "???", "#$1", "***" };
        string[] sn5 = { "<static>", "*HISS*", "<crackle>" };
        string noise = "";
        int ierr;
        ierr = Random.Range(-1, result.Length * 2 / 3);
        while (ierr > 0)
        {
            ierr -= 1;
            nlen = 1;
            noise = sn1[Random.Range(0, 6)];
            pos = Random.Range(0, result.Length - nlen);
            result = result.Substring(0, pos) + noise + result.Substring(pos + nlen, result.Length - pos - nlen);

        }
        ierr = Random.Range(-1, result.Length / 12);
        while (ierr > 0)
        {
            ierr -= 1;
            nlen = 3;
            noise = sn3[Random.Range(0, 3)];
            pos = Random.Range(0, result.Length - nlen);
            result = result.Substring(0, pos) + noise + result.Substring(pos + nlen, result.Length - pos - nlen);

        }
        ierr = Random.Range(-1, result.Length / 20);
        while (ierr > 0)
        {
            if (Random.value < 0.5) break;
            ierr -= 1;
            nlen = 6;
            noise = sn5[Random.Range(0, 3)];
            pos = Random.Range(0, result.Length - nlen);
            result = result.Substring(0, pos) + noise + result.Substring(pos + nlen, result.Length - pos - nlen);
        }

        return result;
    }
}

