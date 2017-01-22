using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CameraAllignToCharacter))]
[RequireComponent(typeof(ScrollingBehaviour))]
[RequireComponent(typeof(AudioSource))]
public class CameraEndBehaviour : MonoBehaviour {

    CameraAllignToCharacter CameraAlign;
    ScrollingBehaviour CameraScroll;
    AudioSource AudioSource;

    bool isEnding = false;

    public Image BlackoutRenderer;
    public float BlackoutDelay = 2f;
    public float BlackoutDuration = 1f;
    public string NextSceneName = "";
    public AudioClip EndClip;

    void Start()
    {
        CameraAlign = GetComponent<CameraAllignToCharacter>();
        CameraScroll = GetComponent<ScrollingBehaviour>();
        AudioSource = GetComponent<AudioSource>();
        StartCoroutine(AnimateStart());
    }

    void FixedUpdate()
    {
        if ( !AudioSource.isPlaying )
            AudioSource.PlayOneShot( EndClip, 1.0f );
    }

    public void End()
    {
        if (isEnding)
            return;

        isEnding = true;
        CameraAlign.enabled = false;
        CameraScroll.enabled = true;
        AudioSource.loop = false;
        StartCoroutine(AnimateEnd());
    }

    public void Restart()
    {
        if (isEnding)
            return;

        isEnding = true;
        CameraAlign.enabled = false;
        CameraScroll.enabled = true;
        AudioSource.loop = false;
        NextSceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(AnimateEnd());
    }

    IEnumerator AnimateStart()
    {
        BlackoutRenderer.gameObject.SetActive(true);
        for (float t = 0; t < BlackoutDuration; t += Time.deltaTime) {
            BlackoutRenderer.color = new Color(0f, 0f, 0f, 1f - t / BlackoutDuration);
            yield return new WaitForEndOfFrame();
        }

        BlackoutRenderer.gameObject.SetActive(false);
    }

    IEnumerator AnimateEnd()
    {
        yield return new WaitForSeconds(BlackoutDelay);

        BlackoutRenderer.gameObject.SetActive(true);
        for (float t = 0; t < BlackoutDuration; t += Time.deltaTime) {
            BlackoutRenderer.color = new Color(0f, 0f, 0f, t / BlackoutDuration);
            yield return new WaitForEndOfFrame();
        }

        while( AudioSource.isPlaying )
            yield return new WaitForEndOfFrame();

        if (!string.IsNullOrEmpty(NextSceneName))
            SceneManager.LoadScene(NextSceneName);
    }
}
