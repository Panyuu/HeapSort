using UnityEngine;
using TMPro;

// author: Leon

public class DisplayTextOnLowerBoard : MonoBehaviour {
    // singleton
    public static DisplayTextOnLowerBoard DTOLB;

    // compiler stops complaining when public instead of serailize field
    public TextMeshPro lowerText;
    public GameObject displayParent;

    // instantiate singleton
    private void Awake() {
        DTOLB = this;
    }

    // set text to empty
    private void Start() {
        lowerText.text = "";
    }

    // activate the lower display
    public static void activateDisplayParent() {
        DTOLB.displayParent.SetActive(true);
    }

    // set the text of the lower display
    public static void setLowerText(string s) {
        DTOLB.lowerText.text = s;
    }
}
