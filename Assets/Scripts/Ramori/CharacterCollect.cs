
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCollect : MonoBehaviour
{
    static AudioSource redBot;
    public AudioClip collectSound;

    int collected = 0;
    public Text CollectedText;

    Text chicken;
    Text fish;
    Text final;

    private void Start()
    {
        var ui = GameObject.Find("UI");
        if (ui == null) return;
        var start = ui.GetComponent<ShowPanels>();
        start.ShowHighscorePanel();

        fish=GameObject.Find("FishPoints").GetComponent<Text>();
        final= GameObject.Find("FinalScore").GetComponent<Text>();
        chicken= GameObject.Find("ChickenPoints").GetComponent<Text>();
        start.HideHighscorePanel();
        if (CollectedText != null)
            CollectedText.text = "0";

        if ( redBot == null )
            redBot = GameObject.Find( "RedBot" ).GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CollectableComponent>() == null)
            return;
        var collectable = other.gameObject.GetComponent<CollectableComponent>();
        collected++;
        try
        {
            if (SceneManager.GetActiveScene().name.Contains("Wojtek"))
            {
                chicken.text = collected + "";
                final.text = ((collected + int.Parse(fish.text)) + Random.Range(-5, 5)) + "";
            }
            else if (SceneManager.GetActiveScene().name.Contains("Ramori"))
            {
                fish.text = collected + "";
                final.text = ((collected + int.Parse(chicken.text)) + Random.Range(-5, 5)) + "";
            }
        }
        catch { }
        CollectedText.text = collected + "";
        ExplosionManager.Collect(collectable.gameObject);
        Destroy(collectable.gameObject);

        redBot.pitch = 1f;
        redBot.PlayOneShot( collectSound );
    }
}

