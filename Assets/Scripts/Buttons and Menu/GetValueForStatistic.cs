using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetValueForStatistic : MonoBehaviour
{
    public static GetValueForStatistic GVFS;

    public TMP_Text labelComparisons, labelSwitches, labelTimeDelta, labelBestCaseJudge;
    public GameObject[] arrBoxes = new GameObject[7];
    //static List<GameObject> prefabList = new List<GameObject>();
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

    private void Awake()
    {
        GVFS = this;
    }

    // run time start, finish, delta 
    public static void makeStatisticsTexts()
    {
        // write into labels
        GVFS.labelComparisons.text = Comparison.ToString();
        GVFS.labelSwitches.text = Switches.ToString();
        GVFS.labelTimeDelta.text = (EndTime - StartTime).ToString() + " ms";

        for(byte i = 0; i < OwnArr.Count; i++)
        {
            GVFS.arrBoxes[i].SetActive(true);
            GVFS.arrBoxes[i].GetComponentsInChildren<TextMeshProUGUI>()[0].text = OwnArr[i].ToString();

        }
        
        if(Switches <= 1)
        {
            GVFS.labelBestCaseJudge.text = "BEST CASE!!!";
        } else if (Switches >= 7)
        {
            GVFS.labelBestCaseJudge.text = "WORST CASE!!!";
        } else
        {
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
