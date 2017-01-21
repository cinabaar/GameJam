using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    private static UIManager _instance;

    public GameObject HealthPrefab;
    public Transform HealthHolder;
    List<Animator> Health;

    void Awake () {
        _instance = this;
    }

    public static void InitHealth(int maxHealth)
    {
        if (_instance == null)
            return;

        _instance.Health = new List<Animator>();

        for (int i = 0; i < maxHealth; i++) {
            GameObject go = Instantiate(_instance.HealthPrefab);
            go.transform.SetParent(_instance.HealthHolder);
            _instance.Health.Add(go.GetComponent<Animator>());
        }
    }

    public static void UpdateHealth(int health)
    {
        if (_instance == null)
            return;

        for (int i = _instance.Health.Count - 1; i >= health; i--) {
            _instance.Health[i].SetTrigger("LoseHealth");
        }
    }
}
