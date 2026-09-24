using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HistoriaController : MonoBehaviour
{
    [Header("Botão")]
    public Button botaoContinuar;

    [Header("Cena do jogo")]
    public string nomeDaCenaDoJogo = "Game";

    private void Start()
    {
        botaoContinuar.onClick.AddListener(IrParaOJogo);
    }

    private void IrParaOJogo()
    {
        SceneManager.LoadScene(nomeDaCenaDoJogo);
    }
}