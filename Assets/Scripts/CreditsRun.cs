using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsRun : MonoBehaviour
{
    public Animator animator;
    public Transform Player;
	
    void Start()
    {
        animator.SetBool("Running", true);

        var ui = GameObject.Find("UI");
        if (ui == null) return;
        var start = ui.GetComponent<ShowPanels>();
        start.ShowHighscorePanel();
        
    }
	// Update is called once per frame
	void Update () {
        Player.position = Player.position + new Vector3(3, 0, 0) * Time.deltaTime;
        if(Player.position.x > 10)
        {
            Player.position = new Vector3(-10, Player.position.y, Player.position.z);
        }
	}

    void OnDestroy()
    {
        var ui = GameObject.Find("UI");
        if (ui == null) return;
        var start = ui.GetComponent<ShowPanels>();
        start.HideHighscorePanel();
    }
}
