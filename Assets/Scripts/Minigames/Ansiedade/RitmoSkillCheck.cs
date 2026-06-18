using UnityEngine;
using UnityEngine.Events;

// REMOVA OU COMENTE ESSA LINHA:
// [System.Serializable] public class EventoPontuacao : UnityEvent<int> { }

public class RitmoSkillCheck : MonoBehaviour
{
    [Header("Configurações do Ritmo")]
    [SerializeField] private float tempoPorFase = 0.2f; 
    [SerializeField] private string nomeTrigger = "IniciarAnimacao"; 

    [Header("Eventos de Pontuação")]
    // USE O UNITYEVENT GENÉRICO DIRETAMENTE AQUI:
    public UnityEvent<int> aoComputarPontos; 

    private Animator meuAnimator;
    private float tempoDeVida = 0f;
    private bool clicou = false;

    void Start()
    {
        meuAnimator = GetComponent<Animator>();
        if (meuAnimator != null)
        {
            meuAnimator.SetTrigger(nomeTrigger);
        }
    }

    void Update()
    {
        tempoDeVida += Time.deltaTime;

        if (tempoDeVida > (tempoPorFase * 4) && !clicou)
        {
            clicou = true;
            FinalizarBola(-1); 
        }
    }

    public void RegistrarClique()
    {
        if (clicou) return; 
        clicou = true;

        int faseAtual = Mathf.FloorToInt(tempoDeVida / tempoPorFase) + 1;
        int pontosGanhos = CalcularPoints(faseAtual);
        FinalizarBola(pontosGanhos);
    }

    private int CalcularPoints(int faseNoClique)
    {
        switch (faseNoClique)
        {
            case 3: return 5; // Perfeito (Terceiro)
            case 2: return 2;  // Médio (Segundo)
            case 1:
            case 4: return -1;  // Ruim (Primeiro e Quarto)
            default: return -2;
        }
    }

    private void FinalizarBola(int pontos)
    {
        if (aoComputarPontos != null)
        {
            aoComputarPontos.Invoke(pontos);
            aoComputarPontos.RemoveAllListeners(); 
        }

        Destroy(gameObject);
    }
}