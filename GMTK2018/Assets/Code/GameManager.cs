using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm = null;

    public Dictionary<Evidence, int> evidenceFound;


    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        evidenceFound = new Dictionary<Evidence, int>();

        if(gm == null)
            gm = this;
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

    public bool isCorrectEvidence(int s)
    {
        //TODO: finish Evidence sequence
        //pick a random number, if it's less than or equal to the score, the case is plausible
        if (Random.Range(1, 101) <= s)
        {
            return true;
        }
        else
        {
            return false; //You lose
        }
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

        if (log.Count == 0)
            log.Add("There was no evidence found...");

        return log;
    }

}
