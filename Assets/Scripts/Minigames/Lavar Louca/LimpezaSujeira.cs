using UnityEngine;
using UnityEngine.UI;

public class LimpezaSujeira : MonoBehaviour
{
    private Image imagemSujeira;
    [SerializeField] private float velocidadeLimpeza = 20f;
    [SerializeField] private GameObject canvasMinigame;

    [SerializeField] private TriggerPia scriptTrigger;
    private Casa scriptCasa;

    [Header("Sons do Minigame")]
    public AudioClip somEsponja;
    [SerializeField] private AudioSource fonteAgua; // O novo gerenciador exclusivo da água

    // Esse método roda toda vez que o minigame é aberto na tela
    private void OnEnable()
    {
        if (imagemSujeira == null) 
            imagemSujeira = GetComponent<Image>();

        // Garante que a sujeira volte a ficar 100% visível ao reabrir
        Color c = imagemSujeira.color;
        c.a = 1f;
        imagemSujeira.color = c;

        // Inicia o som da água assim que o painel do minigame abrir
        if (fonteAgua != null)
        {
            fonteAgua.Play();
        }
    }

    void Start()
    {
        imagemSujeira = GetComponent<Image>();
        Debug.Log("Velocidade de limpeza:" + velocidadeLimpeza);
        scriptCasa = Object.FindAnyObjectByType<Casa>();
    }

    void Update()
    {
        // Monitora se o jogador soltou o clique para parar SOMENTE a esponja
        if (Input.GetMouseButtonUp(0))
        {
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PararLooping();
            }
        }
    }

    public void AoEsfregar()
    {
        if (Input.GetMouseButton(0))
        {
            // Toca a esponja no gerenciador global
            if(somEsponja != null)
            {
                AudioManager.instance.TocarLooping(somEsponja);
            }
            
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
        
        // Desativar o canvas vai disparar a função OnDisable() automaticamente!
        canvasMinigame.SetActive(false); 
        Cursor.visible = true;
        
        // Aplica as consequências de vitória
        Robin.AlterarEnergia(-2);
        Robin.AlterarDiversao(-1);
        scriptCasa.AlterarHoraio(0.5f);
        
        velocidadeLimpeza /= 2;
        Debug.Log("Velocidade de limpeza:" + velocidadeLimpeza);
    }

    // Segurança extra: Quando o painel fechar, tudo para de tocar
    private void OnDisable()
    {
        // Para a esponja
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PararLooping();
        }

        // Para a água escorrendo
        if (fonteAgua != null)
        {
            fonteAgua.Stop();
        }
    }

    // Chamar para liberar a pia em um próximo dia ou evento
    void EventoSujaPia()
    {
        TriggerPia.minigameBloqueado = false;
        Debug.Log("A pia foi suja por um evento externo!");
    }
}