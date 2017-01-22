using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CameraAllignToCharacter))]
[RequireComponent(typeof(ScrollingBehaviour))]
public class CameraEndBehaviour : MonoBehaviour {

    CameraAllignToCharacter CameraAlign;
    ScrollingBehaviour CameraScroll;

    bool isEnding = false;

    public Image BlackoutRenderer;
    public float BlackoutDelay = 2f;
    public float BlackoutDuration = 1f;
    public string SuccessSceneName = "";
    public string FailSceneName = "";

    void Start()
    {
        CameraAlign = GetComponent<CameraAllignToCharacter>();
        CameraScroll = GetComponent<ScrollingBehaviour>();
        StartCoroutine(AnimateStart());
    }

    public void End()
    {
        if (isEnding)
            return;

        isEnding = true;
        CameraAlign.enabled = false;
        CameraScroll.enabled = true;
        StartCoroutine(AnimateEnd(true));
    }

    public void Restart()
    {
        if (isEnding)
            return;

        isEnding = true;
        CameraAlign.enabled = false;
        CameraScroll.enabled = true;
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
        var sceneName = Success ? SuccessSceneName : FailSceneName;
        if (!string.IsNullOrEmpty(sceneName))
            SceneManager.LoadScene(sceneName);
    }
}
