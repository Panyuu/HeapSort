using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getValueForStatistic : MonoBehaviour
{
    public static getValueForStatistic GVFS;

    [SerializeField] Text labelComparisons, labelSwitches, labelTimeDelta, labelBestCaseJudge;
    // for prefab
    [SerializeField] GameObject original;
    static int comparison = 0;
    static int switches = 0;
    static float startTime = 0;
    static float endTime = 0;

    public static int Comparison { get => comparison; set => comparison = value; }
    public static int Switches { get => switches; set => switches = value; }
    public static float StartTime { get => startTime; set => startTime = value; }
    public static float EndTime { get => endTime; set => endTime = value; }
    public static List<int> OwnArr { get => ownArr; set => ownArr = value; }

    static float bestTime = 0;
    static List<int> ownArr = new List<int>();

    private void Awake()
    {
        GVFS = this;
    }

    // max heap / minheap
    // count how many comparisons have been made


    //count how many switches have been made

    // run time start, finish, delta 
    public static void makeStatisticsTexts()
    {
        // write into labels
        GVFS.labelComparisons.text = Comparison.ToString();
        GVFS.labelSwitches.text = Switches.ToString();
        GVFS.labelTimeDelta.text = (StartTime - EndTime).ToString() + " ms";

        for(byte i = 1; i<=OwnArr.Count; i++)
        {
            //generate pefab 
            // increment index
            // write in value
        }
        
        if(true)
        {
            // try out optimal arr & get amount of switches as comparison
        } else
        {
            // get worst case switch amount
        }
        
    }








}
