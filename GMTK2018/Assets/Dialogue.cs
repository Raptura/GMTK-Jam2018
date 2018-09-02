using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject dialougeBox;
    public Button option1, option2, option3, cancel;

    [HideInInspector]
    public Evidence currentEvidence;

    private bool inDialogue;

    // Use this for initialization
    void Awake()
    {
        clear();
    }

    void examineEvidence()
    {
        if (currentEvidence != null)
        {
            option1.GetComponentInChildren<TextMeshProUGUI>().text = currentEvidence.dialouge_1;
            option2.GetComponentInChildren<TextMeshProUGUI>().text = currentEvidence.dialouge_2;
            option3.GetComponentInChildren<TextMeshProUGUI>().text = currentEvidence.dialouge_3;

            dialougeBox.GetComponentInChildren<TextMeshProUGUI>().text = "How do we interpret this piece of evidence?";

            option1.gameObject.SetActive(true);
            option2.gameObject.SetActive(true);
            option3.gameObject.SetActive(true);
            dialougeBox.gameObject.SetActive(true);
            cancel.gameObject.SetActive(true);


            option1.onClick.AddListener(delegate { RegisterOption(1); });
            option2.onClick.AddListener(delegate { RegisterOption(2); });
            option3.onClick.AddListener(delegate { RegisterOption(3); });
            cancel.onClick.AddListener(delegate { clear(); });

        }
    }

    void alreadySeenText()
    {
        option1.gameObject.SetActive(true);
        option1.GetComponentInChildren<TextMeshProUGUI>().text = "Move on";
        option1.onClick.AddListener(delegate { clear(); });

        dialougeBox.gameObject.SetActive(true);
        dialougeBox.GetComponentInChildren<TextMeshProUGUI>().text = "You have already looked at and judged this piece of evidence. There is no reason to look at it again.";
    }

    void removeButtonListeners() {

        option1.onClick.RemoveAllListeners();
        option2.onClick.RemoveAllListeners();
        option3.onClick.RemoveAllListeners();
        cancel.onClick.RemoveAllListeners();
    }

    void RegisterOption(int choice)
    {
        GameManager.gm.registerEvidence(currentEvidence, choice);
        Debug.Log("Added Evidence: " + currentEvidence.ToString());
        Debug.Log("Choice: " + choice);
        clear();
    }

    void clear()
    {
        currentEvidence = null;
        inDialogue = false;
        removeButtonListeners();
        option1.gameObject.SetActive(false);
        option2.gameObject.SetActive(false);
        option3.gameObject.SetActive(false);
        cancel.gameObject.SetActive(false);
        dialougeBox.gameObject.SetActive(false);

        GameManager.gm.paused = false;
    }

    public void setupDialogue(Evidence e)
    {
        if (!inDialogue)
        {
            if (!GameManager.gm.evidenceFound.ContainsKey(e))
            {
                currentEvidence = e;
                examineEvidence();
            }
            else
            {
                //Already found evidence
                alreadySeenText();
            }
            inDialogue = true;
            GameManager.gm.paused = true;
        }
    }

}
