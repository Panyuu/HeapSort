using UnityEngine;

// author: Leon Portius

// necessary to activate the disabled object from another object
public class CallStatistics : MonoBehaviour {
    static CallStatistics CS;

    public GameObject statisticCanvas;

    private void Awake() {
        CS = this;
    }

    public static void callStatisticAfterVisualization() {
        CS.statisticCanvas.SetActive(true);
    }

    public static void closeStatisticAfterVisualization() {
        CS.statisticCanvas.SetActive(false);
    }

}
