using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Character;
    public CameraAllignToCharacter Camera;

    void Awake()
    {
        Camera.CameraDetached += OnCameraDetached;
    }

    private void OnCameraDetached()
    {

    }
	
}
