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
    bool firedOnce = false;

    public Image BlackoutRenderer;
    public float BlackoutDelay = 2f;
    public float BlackoutDuration = 1f;
    public string SuccessSceneName = "";
    public string FailSceneName = "";
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
        if ( !AudioSource.isPlaying && !firedOnce )
        {
            firedOnce = true;
            AudioSource.PlayOneShot( EndClip, 1.0f );
        }
    }

    public void End()
    {
        if (isEnding)
            return;

        isEnding = true;
        CameraAlign.enabled = false;
        CameraScroll.enabled = true;
        AudioSource.loop = false;
        if (AudioSource.clip.length > 5f)
        {
            AudioSource.Stop();
        }
        StartCoroutine(AnimateEnd(true));
    }

    public void Restart()
    {
        if (isEnding)
            return;

        isEnding = true;
        CameraAlign.enabled = false;
        CameraScroll.enabled = true;
        AudioSource.loop = false;
        if(AudioSource.clip.length > 5f)
        {
            AudioSource.Stop();
        }
        FailSceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(AnimateEnd(false));
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

    IEnumerator AnimateEnd(bool Success)
    {
        yield return new WaitForSeconds(BlackoutDelay);

        BlackoutRenderer.gameObject.SetActive(true);
        for (float t = 0; t < BlackoutDuration; t += Time.deltaTime) {
            BlackoutRenderer.color = new Color(0f, 0f, 0f, t / BlackoutDuration);
            yield return new WaitForEndOfFrame();
        }

        while( AudioSource.isPlaying )
            yield return new WaitForEndOfFrame();

        var sceneName = Success ? SuccessSceneName : FailSceneName;
        if (!string.IsNullOrEmpty(sceneName))
            SceneManager.LoadScene(sceneName);
    }
}
