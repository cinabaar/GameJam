using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNextScene : MonoBehaviour {
    public string nextScene = "Scenes/Wojtek";
    // Use this for initialization

    private StartOptions start;
    private bool loading = false;

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
            loading = true;
            start.StartButtonClicked(nextScene);
        }
	}
}
