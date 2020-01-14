using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Scene play;

    public void pressPlay()
    {
        SceneManager.LoadScene(1);
    }
}
