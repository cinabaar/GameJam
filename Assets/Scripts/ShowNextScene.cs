using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowNextScene : MonoBehaviour {
    public string nextScene = "Scenes/Wojtek";
    // Use this for initialization

    private StartOptions start;
    private bool loading = false;

    public Text BottomText;

    void Start ()
    {
        var ui = GameObject.Find("UI");
        if (ui == null) return;
        start = ui.GetComponent<StartOptions>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown && start!= null && !loading)
        {
            if (BottomText != null)
                BottomText.text = "LOADING . . .";
            loading = true;
            start.StartButtonClicked(nextScene);
        }
	}
}
