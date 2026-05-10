using UnityEngine;
using UnityEngine.UI;

public class LimpezaSujeira : MonoBehaviour
{
    private Image imagemSujeira;
    [SerializeField] private static float velocidadeLimpeza = 20f;
    [SerializeField] private GameObject canvasMinigame;

    // Referência ao gatilho para travá-lo
    [SerializeField] private TriggerPia scriptTrigger;

    void Start()
    {
        imagemSujeira = GetComponent<Image>();
        Debug.Log("Velocidade de limpeza:" + velocidadeLimpeza);
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
        // Ativa a trava no gatilho
        if (scriptTrigger != null)
        {
            TriggerPia.minigameBloqueado = true;
        }

        canvasMinigame.SetActive(false);
        Cursor.visible = true;
        Robin.AlterarEnergia(2);
        Robin.AlterarDiversao(-1);
        velocidadeLimpeza /= 2;
        Debug.Log("Velocidade de limpeza:" + velocidadeLimpeza);

        // Resetar a sujeira para a próxima vez que for desbloqueado
        Color c = imagemSujeira.color;
        c.a = 1f;
        imagemSujeira.color = c;

    }
    // Desbloquear pia no futuro (em qualquer outro script do seu jogo)

    void EventoSujaPia()
    {
        // Acessamos direto pelo nome da classe TriggerPia
        TriggerPia.minigameBloqueado = false;

        Debug.Log("A pia foi suja por um evento externo!");
    }
}

