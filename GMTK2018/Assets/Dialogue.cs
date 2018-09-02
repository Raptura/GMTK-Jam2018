using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

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

    void removeButtonListeners()
    {

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

    void clear(bool continueDialogue = false)
    {
        currentEvidence = null;
        inDialogue = continueDialogue;
        removeButtonListeners();
        option1.gameObject.SetActive(false);
        option2.gameObject.SetActive(false);
        option3.gameObject.SetActive(false);
        cancel.gameObject.SetActive(false);
        dialougeBox.gameObject.SetActive(false);

        GameManager.gm.paused = continueDialogue;
    }

    public void setUpEvidence(Evidence e)
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

    public void phoneScene()
    {
        if (!inDialogue)
        {
            option1.gameObject.SetActive(true);
            option1.GetComponentInChildren<TextMeshProUGUI>().text = "Return to the scene";
            option1.onClick.AddListener(delegate { clear(); });

            option2.gameObject.SetActive(true);
            option2.GetComponentInChildren<TextMeshProUGUI>().text = "Report in with what we have collected";
            option2.onClick.AddListener(delegate { reportIn(); });

            dialougeBox.gameObject.SetActive(true);
            dialougeBox.GetComponentInChildren<TextMeshProUGUI>().text = "Should we report in to the detective agency?";

            inDialogue = true;
            GameManager.gm.paused = true;
        }
    }

    void reportIn()
    {
        clear(true);

        option1.gameObject.SetActive(true);
        option1.GetComponentInChildren<TextMeshProUGUI>().text = "I would like to report in!";
        dialougeBox.gameObject.SetActive(true);
        dialougeBox.GetComponentInChildren<TextMeshProUGUI>().text = "Hello? Do you have any evidence you would like to report in?";


        string[] case_details = GameManager.gm.buildEvidenceLog().ToArray();

        option1.onClick.AddListener(delegate
        {
            showEvidence(case_details, 0);
        });

    }

    void showEvidence(string[] evidence, int index)
    {
        if (evidence.Length > index)
        {
            clear(true);

            option1.gameObject.SetActive(true);
            option1.GetComponentInChildren<TextMeshProUGUI>().text = "That is correct...";

            dialougeBox.gameObject.SetActive(true);
            dialougeBox.GetComponentInChildren<TextMeshProUGUI>().text = evidence[index];

            option1.onClick.AddListener(delegate { showEvidence(evidence, index + 1); });
        }
        else {
            clear(true);

            dialougeBox.gameObject.SetActive(true);
            dialougeBox.GetComponentInChildren<TextMeshProUGUI>().text = "Your score is: " + GameManager.gm.getScore() + " \n";

            option1.gameObject.SetActive(true);
            option1.onClick.AddListener(delegate
            {
                SceneManager.LoadScene(0); //Reset Game
            });

            if (GameManager.gm.isCorrectEvidence())
            {
                option1.GetComponentInChildren<TextMeshProUGUI>().text = "Thanks";
                dialougeBox.GetComponentInChildren<TextMeshProUGUI>().text += "Your theory seems plausable \n Congratulations! You passed! \n";
            }
            else
            {

                option1.GetComponentInChildren<TextMeshProUGUI>().text = "Dang...";
                dialougeBox.GetComponentInChildren<TextMeshProUGUI>().text += "Go work at another detective agency! You're fired! \n";
            }

            dialougeBox.GetComponentInChildren<TextMeshProUGUI>().text += "(Press the button to reset the game)";
        }
    }
}
