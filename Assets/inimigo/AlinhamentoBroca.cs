using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AlinhamentoBroca : MonoBehaviour
{
    public Transform brocaPonta;
    public float velocidadeRotacao = 10f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 direcao = agent.velocity;
        direcao.y = 0f;

        if (direcao.sqrMagnitude > 0.01f)
        {
            Quaternion rotacaoAlvo = Quaternion.LookRotation(direcao.normalized);
            Transform alvo = brocaPonta != null ? brocaPonta : transform;
            alvo.rotation = Quaternion.Slerp(alvo.rotation, rotacaoAlvo, velocidadeRotacao * Time.deltaTime);
        }
    }
}
