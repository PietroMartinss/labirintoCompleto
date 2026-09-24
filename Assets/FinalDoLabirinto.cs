using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class FinalDoLabirinto : MonoBehaviour
{
    [Header("Configuração")]
    public string tagDoPlayer = "Player";

#if UNITY_EDITOR
    public SceneAsset cenaCreditos;
#endif

    public float tempoAntesDosCreditos = 1f;

    private bool ativado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (ativado)
            return;

        if (other.CompareTag(tagDoPlayer))
        {
            ativado = true;
            StartCoroutine(IrParaCreditos());
        }
    }

    private IEnumerator IrParaCreditos()
    {
        yield return new WaitForSeconds(tempoAntesDosCreditos);

#if UNITY_EDITOR
        SceneManager.LoadScene(cenaCreditos.name);
#endif
    }
}