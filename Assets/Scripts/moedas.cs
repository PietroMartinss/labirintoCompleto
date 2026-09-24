using UnityEngine;

public class moedas : MonoBehaviour
{
    [SerializeField] private int valor = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ContadorDeMoedas.Instance.AdicionarMoedas(valor);
            Destroy(gameObject);
        }
    }
}

