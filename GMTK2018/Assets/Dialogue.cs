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

    // Use this for initialization
    void Awake()
    {
        clear();
        buttonSetup();
        manageText();
    }

    void buttonSetup()
    {
        option1.onClick.AddListener(delegate { RegisterOption(1); });
        option2.onClick.AddListener(delegate { RegisterOption(2); });
        option3.onClick.AddListener(delegate { RegisterOption(3); });
        cancel.onClick.AddListener(delegate { clear(); });
    }


    void manageText()
    {
        if (currentEvidence != null)
        {
            choice_text1 = currentEvidence.dialouge_1;
            choice_text2 = currentEvidence.dialouge_2;
            choice_text3 = currentEvidence.dialouge_3;

            option1.GetComponentInChildren<TextMeshProUGUI>().text = choice_text1;
            option2.GetComponentInChildren<TextMeshProUGUI>().text = choice_text2;
            option3.GetComponentInChildren<TextMeshProUGUI>().text = choice_text3;

            option1.gameObject.SetActive(choice_text1 != null);
            option2.gameObject.SetActive(choice_text2 != null);
            option3.gameObject.SetActive(choice_text3 != null);
            dialougeBox.gameObject.SetActive(true);
            cancel.gameObject.SetActive(true);
        }
        else
        {
            option1.gameObject.SetActive(false);
            option2.gameObject.SetActive(false);
            option3.gameObject.SetActive(false);
            cancel.gameObject.SetActive(false);
            dialougeBox.gameObject.SetActive(false);
        }
    }

    void RegisterOption(int choice)
    {
        GameManager.gm.registerEvidence(currentEvidence, choice);
        clear();
    }

    void clear()
    {
        currentEvidence = null;
        choice_text1 = choice_text2 = choice_text3 = null;
        manageText();
        Time.timeScale = 1;
    }

    public void setupDialogue(Evidence e)
    {
        currentEvidence = e;
        manageText();
        Time.timeScale = 0;
    }

}
