using UnityEngine;
using UnityEngine.UI;

public class LimpezaSujeira : MonoBehaviour
{
    private Image imagemSujeira;
    [SerializeField] private static float velocidadeLimpeza = 20f;
    [SerializeField] private GameObject canvasMinigame;

    [SerializeField] private TriggerPia scriptTrigger;
    private Casa scriptCasa;

    // Esse método roda toda vez que o minigame é aberto na tela
    private void OnEnable()
    {
        if (imagemSujeira == null) 
            imagemSujeira = GetComponent<Image>();

        // Garante que a sujeira volte a ficar 100% visível ao reabrir
        Color c = imagemSujeira.color;
        c.a = 1f;
        imagemSujeira.color = c;
    }

    void Start()
    {
        imagemSujeira = GetComponent<Image>();
        Debug.Log("Velocidade de limpeza:" + velocidadeLimpeza);
        scriptCasa = Object.FindAnyObjectByType<Casa>();
    }

    public void AoEsfregar()
    {
        if (Input.GetMouseButton(0))
        {
            Color corAtual = imagemSujeira.color;
            corAtual.a -= velocidadeLimpeza * Time.deltaTime;
            imagemSujeira.color = corAtual;

            if (corAtual.a <= 0)
            {
                FinalizarMinigame();
            }
        }
    }

    void FinalizarMinigame()
    {
        if (scriptTrigger != null)
        {
            TriggerPia.minigameBloqueado = true;
        }
        
        CameraPanLateral.minigameAtivo = false; 
        canvasMinigame.SetActive(false);
        Cursor.visible = true;
        
        // Aplica as consequências de vitória normalmente
        Robin.AlterarEnergia(-2);
        Robin.AlterarDiversao(-1);
        scriptCasa.AlterarHoraio(0.5f);
        
        velocidadeLimpeza /= 2;
        Debug.Log("Velocidade de limpeza:" + velocidadeLimpeza);
    }

    // Chamar para liberar a pia em um próximo dia ou evento
    void EventoSujaPia()
    {
        TriggerPia.minigameBloqueado = false;
        Debug.Log("A pia foi suja por um evento externo!");
    }
}