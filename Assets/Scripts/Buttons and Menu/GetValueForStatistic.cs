using System.Collections.Generic;
using TMPro;
using UnityEngine;

// author: Leon Portius

public class GetValueForStatistic : MonoBehaviour {
    // singleton
    public static GetValueForStatistic GVFS;

    // serialize fields for easy access of statistics labels
    public TMP_Text labelComparisons, labelSwitches, labelTimeDelta, labelBestCaseJudge;
    public GameObject[] arrBoxes = new GameObject[7];

    // testing out generated getter and setters by Visual Studio
    static int comparison = 0;
    static int switches = 0;
    static float startTime = 0;
    static float endTime = 0;

    public static int Comparison { get => comparison; set => comparison = value; }
    public static int Switches { get => switches; set => switches = value; }
    public static float StartTime { get => startTime; set => startTime = value; }
    public static float EndTime { get => endTime; set => endTime = value; }
    public static List<int> OwnArr { get => ownArr; set => ownArr = value; }

    //static float bestTime = 0;
    static List<int> ownArr = new List<int>();

    // instantiate singleton
    private void Awake() {
        GVFS = this;
    }

    // work with run time start, finish, delta 
    public static void makeStatisticsTexts() {
        // write into labels
        GVFS.labelComparisons.text = Comparison.ToString();
        GVFS.labelSwitches.text = Switches.ToString();
        GVFS.labelTimeDelta.text = (EndTime - StartTime).ToString() + " ms";

        // enable boxes and set their dynamic index label
        for (byte i = 0; i < OwnArr.Count; i++) {
            GVFS.arrBoxes[i].SetActive(true);
            GVFS.arrBoxes[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = OwnArr[i].ToString();

        }

        // evaluate the given case
        if (Switches <= 1) {
            GVFS.labelBestCaseJudge.text = "BEST CASE!!!";
        }
        else if (Switches >= 7) {
            GVFS.labelBestCaseJudge.text = "WORST CASE!!!";
        }
        else {
            GVFS.labelBestCaseJudge.text = "Durchschnitt...";
        }
    }

    // clear values
    public static void setBackEverything() {

        ownArr.Clear();
        Comparison = 0;
        Switches = 0;
        StartTime = 0;
        EndTime = 0;
    }
}
