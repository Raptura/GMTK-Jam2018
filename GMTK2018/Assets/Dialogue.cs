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
    public string choice_text1, choice_text2, choice_text3;

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
            choice_text1 = currentEvidence.dialouge_1;
            choice_text2 = currentEvidence.dialouge_2;
            choice_text3 = currentEvidence.dialouge_3;

            option1.GetComponentInChildren<TextMeshProUGUI>().text = choice_text1;
            option2.GetComponentInChildren<TextMeshProUGUI>().text = choice_text2;
            option3.GetComponentInChildren<TextMeshProUGUI>().text = choice_text3;
            dialougeBox.GetComponentInChildren<TextMeshProUGUI>().text = "How do we interpret this piece of evidence?";

            option1.gameObject.SetActive(choice_text1 != null);
            option2.gameObject.SetActive(choice_text2 != null);
            option3.gameObject.SetActive(choice_text3 != null);

            if (option1.IsActive())
            {
                option1.onClick.RemoveAllListeners();
                option1.onClick.AddListener(delegate { RegisterOption(1); });
            }
            if (option2.IsActive())
            {
                option2.onClick.RemoveAllListeners();
                option2.onClick.AddListener(delegate { RegisterOption(2); });
            }
            if (option3.IsActive())
            {
                option3.onClick.RemoveAllListeners();
                option3.onClick.AddListener(delegate { RegisterOption(3); });
            }

            cancel.onClick.RemoveAllListeners();
            cancel.onClick.AddListener(delegate { clear(); });

            dialougeBox.gameObject.SetActive(true);
            cancel.gameObject.SetActive(true);
        }
        else
        {
            choice_text1 = null;
            choice_text2 = null;
            choice_text3 = null;

            option1.gameObject.SetActive(false);
            option2.gameObject.SetActive(false);
            option3.gameObject.SetActive(false);
            cancel.gameObject.SetActive(false);
            dialougeBox.gameObject.SetActive(false);
        }
    }

    void alreadySeenText()
    {
        option1.gameObject.SetActive(true);
        option1.GetComponentInChildren<TextMeshProUGUI>().text = "Move on";
        option1.onClick.RemoveAllListeners();
        option1.onClick.AddListener(delegate { clear(); });


        dialougeBox.gameObject.SetActive(true);
        dialougeBox.GetComponentInChildren<TextMeshProUGUI>().text = "You have already looked at and judged this piece of evidence. There is no reason to look at it again.";



    }

    void RegisterOption(int choice)
    {
        GameManager.gm.registerEvidence(currentEvidence, choice);
        clear();
    }

    void clear()
    {
        currentEvidence = null;
        examineEvidence();
        GameManager.gm.paused = false;
        inDialogue = false;
    }

    public void setupDialogue(Evidence e)
    {
        if (!inDialogue)
        {
            if (!GameManager.gm.evidenceFound.ContainsKey(e))
            {
                currentEvidence = e;
                examineEvidence();
                GameManager.gm.paused = true;
            }
            else
            {
                //Already found evidence
                alreadySeenText();
            }
            inDialogue = true;
        }
    }

}
