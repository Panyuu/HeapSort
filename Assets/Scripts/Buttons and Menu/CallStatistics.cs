using UnityEngine;

public class CallStatistics : MonoBehaviour
{
    static CallStatistics CS;

    public GameObject statisticCanvas;

    private void Awake()
    {
        CS = this;
    }

    public static void callStatisticAfterVisualization()
    {
        CS.statisticCanvas.SetActive(true);
    }

    public static void closeStatisticAfterVisualization()
    {
        CS.statisticCanvas.SetActive(false);
    }

}
