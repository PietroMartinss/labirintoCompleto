using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Botões")]
    public Button botaoJogar;
    public Button botaoSair;

    [Header("Cenas")]
    public string nomeDaCenaHistoria = "Historia";
    public string nomeDaCenaDoJogo = "Game";

    private void Start()
    {
        botaoJogar.onClick.AddListener(Jogar);
        botaoSair.onClick.AddListener(Sair);
    }

    private void Jogar()
    {
        SceneManager.LoadScene(nomeDaCenaHistoria);
    }

    private void Sair()
    {
        Application.Quit();
    }
}