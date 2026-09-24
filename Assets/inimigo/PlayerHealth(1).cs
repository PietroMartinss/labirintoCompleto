using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vidas")]
    public int maxLives = 3;
    private int currentLives;

    [Header("Texto de vidas")]
    public TMP_Text textoVidas;

    [Header("UI (opcional)")]
    public Image[] heartIcons;
    public Sprite heartFull;
    public Sprite heartEmpty;

    [Header("Invulnerabilidade após tomar dano")]
    public float invulnerabilityTime = 1f;
    private bool isInvulnerable = false;

    [Header("Cenas")]
    public string creditsSceneName = "Credits";

    [Header("Tempo antes dos créditos")]
    public float deathDelay = 1f;

    private bool isDead = false;

    void Start()
    {
        currentLives = maxLives;
        UpdateUI();
    }

    public void TakeDamage(int amount = 1)
    {
        if (isInvulnerable || isDead) return;

        currentLives -= amount;
        currentLives = Mathf.Max(currentLives, 0);

        UpdateUI();

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(InvulnerabilityWindow());
        }
    }

    IEnumerator InvulnerabilityWindow()
    {
        isInvulnerable = true;

        yield return new WaitForSeconds(invulnerabilityTime);

        isInvulnerable = false;
    }

    void UpdateUI()
    {
        if (textoVidas != null)
        {
            textoVidas.text = "Vidas: " + currentLives;
        }

        if (heartIcons == null) return;

        for (int i = 0; i < heartIcons.Length; i++)
        {
            if (heartIcons[i] == null) continue;

            heartIcons[i].sprite = i < currentLives ? heartFull : heartEmpty;
        }
    }

    void GameOver()
    {
        if (isDead) return;

        isDead = true;

        StartCoroutine(IrParaCreditos());
    }

    IEnumerator IrParaCreditos()
    {
        yield return new WaitForSeconds(deathDelay);

        SceneManager.LoadScene(creditsSceneName);
    }

    public void ResetLives()
    {
        currentLives = maxLives;
        isInvulnerable = false;
        isDead = false;

        UpdateUI();
    }
}