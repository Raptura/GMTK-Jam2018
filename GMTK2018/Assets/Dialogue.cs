using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{

    public Button option1, option2, option3;

    [HideInInspector]
    public string choice_text1, choice_text2, choice_text3;

    public EvidenceBehaviour currentEvidence;

    // Use this for initialization
    void Start()
    {
        //currentEvidence = null;
        //clear();
        buttonSetup();
    }

    // Update is called once per frame
    void Update()
    {
        manageText();
    }

    void buttonSetup()
    {
        option1.onClick.AddListener(delegate { RegisterOption(1); });
        option2.onClick.AddListener(delegate { RegisterOption(2); });
        option3.onClick.AddListener(delegate { RegisterOption(3); });
    }

    void manageText()
    {
        if (currentEvidence != null)
        {
            choice_text1 = currentEvidence.evidenceData.dialouge_1;
            choice_text2 = currentEvidence.evidenceData.dialouge_2;
            choice_text3 = currentEvidence.evidenceData.dialouge_3;

            option1.GetComponentInChildren<TextMeshProUGUI>().text = choice_text1;
            option2.GetComponentInChildren<TextMeshProUGUI>().text = choice_text2;
            option3.GetComponentInChildren<TextMeshProUGUI>().text = choice_text3;
        }


        option1.gameObject.SetActive(choice_text1 != null);
        option2.gameObject.SetActive(choice_text2 != null);
        option3.gameObject.SetActive(choice_text3 != null);

    }

    void RegisterOption(int choice)
    {
        GameManager.gm.registerEvidence(currentEvidence.evidenceData, choice);
        clear();
    }

    void clear()
    {

        currentEvidence = null;
        choice_text1 = choice_text2 = choice_text3 = null;
    }

}
