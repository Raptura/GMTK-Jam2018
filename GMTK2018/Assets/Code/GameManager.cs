using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm = null;

    public Dictionary<Evidence, int> evidenceFound;
    public bool paused = false;


    // Use this for initialization
    void Awake()
    {
        evidenceFound = new Dictionary<Evidence, int>();

        if (gm == null)
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

    public int getScore()
    {
        int score = 0;
        foreach (KeyValuePair<Evidence, int> entry in evidenceFound)
        {
            Evidence e = entry.Key;
            int choice = entry.Value;
            switch (choice)
            {
                case 1:
                    score += e.score1;
                    break;
                case 2:
                    score += e.score2;
                    break;
                case 3:
                    score += e.score3;
                    break;
                default:
                    score += 0;
                    break;
            }

        }

        return score;
    }

    public bool isCorrectEvidence()
    {
        //TODO: finish Evidence sequence
        //pick a random number, if it's less than or equal to the score, the case is plausible
        if (Random.Range(1, 101) <= getScore())
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
    public List<string> buildEvidenceLog()
    {
        List<string> log = new List<string>();

        foreach (KeyValuePair<Evidence, int> entry in evidenceFound)
        {
            Evidence e = entry.Key;
            int choice = entry.Value;

            switch (choice)
            {
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
