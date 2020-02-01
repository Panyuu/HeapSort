using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public GameObject firstTutorial;

    public void pressPlay() {
        SceneManager.LoadScene(1);
    }

    public void pressExit() {
        Application.Quit();
    }

    public void startTutorial() {
        firstTutorial.SetActive(true);
    }

    public void nextCanvas(GameObject next) {
        next.SetActive(true);
        //now.SetActive(false);
    }

    public void closeCanvas(GameObject now) {
        now.SetActive(false);
    }

}
