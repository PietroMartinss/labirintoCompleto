using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AjustarTextura : MonoBehaviour
{
    [Header("Tamanho desejado de cada repetição da textura")]
    public float tamanhoTextura = 1f;

    [Header("Manter a textura na mesma escala vertical")]
    public bool repetirNaAltura = false;

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();

        Vector3 tamanho = transform.lossyScale;

        float tilingHorizontal;

        // Descobre se a parede é mais comprida no X ou no Z
        if (tamanho.x >= tamanho.z)
        {
            // Parede horizontal
            tilingHorizontal = tamanho.x / tamanhoTextura;
        }
        else
        {
            // Parede vertical no plano XZ
            tilingHorizontal = tamanho.z / tamanhoTextura;
        }

        float tilingVertical;

        if (repetirNaAltura)
        {
            tilingVertical = tamanho.y / tamanhoTextura;
        }
        else
        {
            tilingVertical = 1f;
        }

        rend.material.mainTextureScale =
            new Vector2(tilingHorizontal, tilingVertical);
    }
}

