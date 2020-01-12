using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject heapHolder;



    public GameObject pauseButton;
    public GameObject startButton;
    public GameObject protoButton;
    public GameObject Proto;

    private bool protoShown;

    [SerializeField] GameObject ships;

    public void animationMaxStop()
    {
        bool playing = MaxHeap.getPlayAnimation();
        
            Debug.Log(playing);
            MaxHeap.setPlayAnimation(false);
            Debug.Log(playing);



        startButton.SetActive(true);
        protoButton.SetActive(true);
        pauseButton.SetActive(false);
    }
    public void animationMaxStart()
    {
        bool playing = MaxHeap.getPlayAnimation();

        Debug.Log(playing);
        MaxHeap.setPlayAnimation(true);
        StartCoroutine(MaxHeap.startAnimation());
        Debug.Log(playing);

        pauseButton.SetActive(true);
        protoButton.SetActive(false);
        startButton.SetActive(false);
        
    }

    public void protocolShow()
    {
        if(protoShown)
        {
            Proto.SetActive(false);
            protoShown = false;
        }

        else
        {
            Proto.SetActive(true);
            protoShown = true;
        }

    }

}
