using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections; // IMPORTANTE: Necessário para usar Corotinas (IEnumerator)

public class PlantaProgresso : MonoBehaviour
{
    [SerializeField] private float aguaAtual = 0f;
    [SerializeField] private float aguaMaxima = 100f;
    [SerializeField] private float velocidadRegar = 25f;

    private bool plantaSatisfeita = false;

    private static int plantasResolvidas = 0;
    [SerializeField] private GameObject painelMinigame;

    [Header("Evolução da Planta")]
    [SerializeField] private Sprite sprite25Porcento;
    [SerializeField] private Sprite sprite50Porcento;
    [SerializeField] private Sprite sprite75Porcento;

    private Image imagemComponente;
    private Sprite spriteInicial;
    private Casa scriptCasa;

    public AudioClip somRegando;

    private void Awake()
    {
        imagemComponente = GetComponent<Image>();
        if (imagemComponente != null)
        {
            spriteInicial = imagemComponente.sprite;
        }

        scriptCasa = Object.FindAnyObjectByType<Casa>();
    }

    private void OnEnable()
    {
        aguaAtual = 0f;
        plantaSatisfeita = false;
        plantasResolvidas = 0;

        if (imagemComponente != null && spriteInicial != null)
        {
            imagemComponente.sprite = spriteInicial;
        }
    }

    void Update()
    {
        if (plantaSatisfeita) return;

        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() &&
                RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition))
            {
                RegarPlanta();

                if (somRegando != null)
                {
                    AudioManager.instance.TocarLooping(somRegando);
                }
                return;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            AudioManager.instance.PararLooping();
        }
    }

    void RegarPlanta()
    {
        if (plantaSatisfeita) return;

        aguaAtual += velocidadRegar * Time.deltaTime;
        aguaAtual = Mathf.Clamp(aguaAtual, 0f, aguaMaxima);

        Debug.Log($"{gameObject.name} recebendo água: {aguaAtual}");
        AtualizarSpriteProgresso();

        if (aguaAtual >= aguaMaxima)
        {
            plantaSatisfeita = true;
            plantasResolvidas++;
            Debug.Log($"{gameObject.name} totalmente regada! Total resolvidas: {plantasResolvidas}");

            // Força o som a parar imediatamente no momento do acerto
            AudioManager.instance.PararLooping();

            ChecarFinalJogo();
        }
    }

    void AtualizarSpriteProgresso()
    {
        if (imagemComponente == null) return;

        float porcentagem = ((float)aguaAtual / aguaMaxima) * 100f;

        if (porcentagem >= 75f && sprite75Porcento != null)
        {
            AplicarSprite(sprite75Porcento);
        }
        else if (porcentagem >= 50f && sprite50Porcento != null)
        {
            AplicarSprite(sprite50Porcento);
        }
        else if (porcentagem >= 25f && sprite25Porcento != null)
        {
            AplicarSprite(sprite25Porcento);
        }
    }

    void AplicarSprite(Sprite novoSprite)
    {
        imagemComponente.sprite = novoSprite;

        imagemComponente.SetNativeSize();

        RectTransform rect = imagemComponente.GetComponent<RectTransform>();

        rect.sizeDelta *= 3f;
    }

    void ChecarFinalJogo()
    {
        if (plantasResolvidas >= 2)
        {
            Debug.Log("Minigame Plantas Concluído com Sucesso!");

            // CORREÇÃO CRÍTICA: Iniciamos uma corotina no próprio script da planta para fechar o jogo com segurança
            StartCoroutine(FecharPainelComSeguranca());
        }
    }

    // NOVA COROTINA: Garante o corte do som antes de desativar o painel
    private IEnumerator FecharPainelComSeguranca()
    {
        // 1. Mandamos o AudioManager parar o looping enquanto o painel ainda está ativo
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PararLooping();
        }

        // 2. Esperamos o final deste frame. Isso dá tempo para a Unity processar o comando de parada do áudio
        yield return new WaitForEndOfFrame();

        // 3. Agora que o som parou com 100% de certeza, rodamos o restante da sua lógica de vitória
        TriggerPlanta.minigameBloqueado = true;
        Robin.AlterarDiversao(4);
        Robin.AlterarProgresso(2);
        Casa.AlterarAguaPlanta(4);
        scriptCasa.AlterarHoraio(0.75f);

        DialogueManager.Instance.StartDialogue("depois_atividade_plantas");

        CameraPanLateral.minigameAtivo = false;

        Cursor.visible = true;

        if (painelMinigame != null)
        {
            painelMinigame.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PararLooping();
        }
    }
}