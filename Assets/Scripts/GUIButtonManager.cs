using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIButtonManager : MonoBehaviour
{
    public GameObject heapHolder;

    public GameObject protoText;
    public GameObject textHolder;
    public GameObject pauseButton;
    public GameObject startButton;
    public GameObject protoButton;
    public GameObject Proto;

    private bool protoShown;
    private int protoCount = 0;
    

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

    public void protofill(string content)
    {
        Debug.Log("FILL IT UP BOBBY!!");
        GameObject help = Instantiate(GameObject.Find("Proto_Prefab"), new Vector3(protoText.transform.localPosition.x, protoText.transform.localPosition.y + 50 * protoCount, protoText.transform.localPosition.z), Quaternion.identity);
        help.transform.SetParent(GameObject.Find("Textholder").transform, false);
        help.name = "Schritt" + protoCount;
        help.GetComponent<Text>().text = content;
        
    }

}
