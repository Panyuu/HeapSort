﻿using UnityEngine;
using TMPro;

public class DisplayTextOnLowerBoard : MonoBehaviour
{
    public static DisplayTextOnLowerBoard DTOLB;

    [SerializeField] TextMeshPro lowerText;
    [SerializeField] GameObject displayParent;

    private void Awake()
    {
        DTOLB = this;
    }

    private void Start()
    {
        lowerText.text = "";
    }

    public static void activateDisplayParent()
    {
        DTOLB.displayParent.SetActive(true);
    }

    public static void setLowerText(string s)
    {
        DTOLB.lowerText.text = s;
    }
}