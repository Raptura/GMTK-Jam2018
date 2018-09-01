﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Dictionary<Evidence, int> evidenceFound = new Dictionary<Evidence, int>();


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e">The Evidence being registered</param>
    /// <param name="choice">the choice [1,3]  that is the interpretation </param>
    public void registerEvidence(Evidence e, int choice)
    {
        if (evidenceFound.ContainsKey(e) == false)
            evidenceFound.Add(e, choice);
    }


    public bool isCorrectEvidence()
    {
        //TODO: Create correct Evidence sequence
        return false; //You lose
    }

    /// <summary>
    /// Builds the evidence log to be used during the case
    /// </summary>
    /// <returns></returns>
    public List<string> buildEvidenceLog() {
        List<string> log = new List<string>();

        foreach (KeyValuePair<Evidence, int> entry in evidenceFound) {
            Evidence e = entry.Key;
            int choice = entry.Value;

            switch (choice) {
                case 1:
                    log.Add(e.case_option_1);
                    break;
                case 2:
                    log.Add(e.case_option_2);
                    break;
                case 3:
                    log.Add(e.case_option_3);
                    break;
                default:
                    log.Add(e.case_option_1);
                    break;
            }
        }

        return log;
    }

}
