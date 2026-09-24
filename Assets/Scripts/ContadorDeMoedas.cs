using UnityEngine;
using TMPro;

public class ContadorDeMoedas : MonoBehaviour
{
    public static ContadorDeMoedas Instance;

    [SerializeField] private TMP_Text textoMoedas;

    private int moedas = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AtualizarUI();
    }

    public void AdicionarMoedas(int quantidade)
    {
        moedas += quantidade;
        AtualizarUI();
    }

    private void AtualizarUI()
    {
        textoMoedas.text = "Moedas: " + moedas;
    }
}