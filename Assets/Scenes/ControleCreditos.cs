using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ControleCreditos : MonoBehaviour
{
#if UNITY_EDITOR
    public SceneAsset cenaMenu;
#endif

    public float tempoNosCreditos = 8f;

    private void Start()
    {
        Invoke("IrParaMenu", tempoNosCreditos);
    }

    private void IrParaMenu()
    {
#if UNITY_EDITOR
        SceneManager.LoadScene(cenaMenu.name);
#endif
    }
}