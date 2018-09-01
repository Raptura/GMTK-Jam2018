using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Evidence", order = 1)]
public class Evidence : ScriptableObject {

    [TextArea(1,1)]
    public string dialouge_1, dialouge_2, dialouge_3;

    [TextArea(3, 6)]
    public string case_option_1, case_option_2, case_option_3;


    public int score1, score2, score3;
}
