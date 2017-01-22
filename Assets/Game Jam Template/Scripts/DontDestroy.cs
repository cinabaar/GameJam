using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

	void Start()
	{
        //Causes UI object not to be destroyed when loading a new scene. If you want it to be destroyed, destroy it manually via script.
        var uiCount = GameObject.FindObjectsOfType<DontDestroy>().Length;
        if (uiCount == 1)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
	}

	

}
