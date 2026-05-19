using UnityEngine;
using TMPro;

public class ThoughtSpawner : MonoBehaviour
{
    public GameObject thoughtPrefab;
    public Transform thoughtParent;
    public StressManager stressManager;

    public float thoughtLifeTime = 4f;

    private float timer = 0f;

    public string[] thoughts =
    {
        "ÒÈ ÍÅ ÂÏÎÐAªØÑß",
        "ÍÅ ÐÈÇÈÊÓÉ",
        "ÓÑ² ÄÈÂËßÒÜÑß ÍÀ ÒÅÁÅ",
        "ÊÐÀÙÅ ÇÄÀÉÑß",
        "Í²×ÎÃÎ ÍÅ ÂÈÉÄÅ",
        "ÒÈ ÇÍÎÂÓ ÏÎÌÈËÈØÑß",
        "ÒÈ ÇÀÏ²ÇÍÈËÀÑß",
        "ÓÑÅ ÌÀÐÍÎ",
        "ÍÅ ÏÎ×ÈÍÀÉ",
        "ÖÅ ÍÅ ÄËß ÒÅÁÅ",
        "ÒÈ ÍÅÄÎÑÒÀÒÍß",
        "ÒÅÁÅ ÇÀÑÓÄßÒÜ",
        "ÒÈ ÂÑÅ Ç²ÏÑÓªØ",
        "ÊÐÀÙÅ ÌÎÂ×È",
        "ÂÆÅ Ï²ÇÍÎ",
        "ÒÈ ÍÅ ÃÎÒÎÂÀ",
        "ÓÑ² ÊÐÀÙ² ÇÀ ÒÅÁÅ",
        "ÍÅ ÐÎÁÈ ÖÜÎÃÎ",
        "ÒÈ ÍÅ ÂÑÒÈÃÍÅØ",
        "ÇÓÏÈÍÈÑß",
        "ÒÈ ÑËÀÁÊÀ",
        "ÂÈÕÎÄÓ ÍÅÌÀª",
        "ÓÑÅ ÏÎÂÒÎÐÞªÒÜÑß",
        "ÒÈ ÇÀËÈØÈËÀÑÜ ÑÀÌÀ",
        "ÖÅ ÒÂÎß ÏÐÎÂÈÍÀ"
    };

    void Update()
    {
        float stressPercent = stressManager.stress / stressManager.maxStress;

        float spawnInterval = Mathf.Lerp(2.5f, 0.35f, stressPercent);

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnThought(stressPercent);
            timer = 0f;
        }
    }

    void SpawnThought(float stressPercent)
    {
        GameObject newThought = Instantiate(thoughtPrefab, thoughtParent);
        newThought.SetActive(true);

        TMP_Text text = newThought.GetComponent<TMP_Text>();
        text.text = thoughts[Random.Range(0, thoughts.Length)];

        text.color = new Color(1f, 0f, 0f, Mathf.Lerp(0.35f, 1f, stressPercent));
        text.fontSize = Mathf.Lerp(26f, 48f, stressPercent);

        RectTransform rect = newThought.GetComponent<RectTransform>();

        rect.anchoredPosition = new Vector2(
            Random.Range(-400, 400),
            Random.Range(-150, 150)
        );

        rect.rotation = Quaternion.Euler(
            0,
            0,
            Random.Range(-18f, 18f)
        );

        Destroy(newThought, thoughtLifeTime);
    }
}