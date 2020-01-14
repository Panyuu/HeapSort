using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getValueForStatistic : MonoBehaviour
{
    public static getValueForStatistic GVFS;

    [SerializeField] Text labelComparisons, labelSwitches, labelTimeDelta;

    static int comparison = 0;
    static int switches = 0;
    static float startTime = 0;
    static float endTime = 0;

    public static int Comparison { get => comparison; set => comparison = value; }
    public static int Switches { get => switches; set => switches = value; }
    public static float StartTime { get => startTime; set => startTime = value; }
    public static float EndTime { get => endTime; set => endTime = value; }

    static float bestTime = 0;

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
        GVFS.labelComparisons.text = Comparison.ToString();
        GVFS.labelSwitches.text = Switches.ToString();
        GVFS.labelTimeDelta.text = (StartTime - EndTime).ToString();
    }








}
