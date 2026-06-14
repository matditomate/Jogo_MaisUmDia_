using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlantaProgresso : MonoBehaviour
{
    [SerializeField] private float aguaAtual = 0f;
    [SerializeField] private float aguaMaxima = 100f;
    [SerializeField] private float velocidadRegar = 25f; 

    private bool plantaSatisfeita = false;

    // Controle estático do minigame
    private static int plantasResolvidas = 0;
    [SerializeField] private GameObject painelMinigame; 

    [Header("Evolução da Planta")]
    [SerializeField] private Sprite sprite25Porcento;   
    [SerializeField] private Sprite sprite50Porcento;   
    [SerializeField] private Sprite sprite75Porcento;   

    private Image imagemComponente;
    private Sprite spriteInicial; // Guarda o visual seco original
    private Casa scriptCasa;

    private void Awake()
    {
        // O Awake roda ANTES de qualquer OnEnable, garantindo o cache seguro dos componentes
        imagemComponente = GetComponent<Image>();
        if (imagemComponente != null)
        {
            spriteInicial = imagemComponente.sprite;
        }

        scriptCasa = Object.FindAnyObjectByType<Casa>();
    }

    // Esse método garante o reset completo sempre que o painel for aberto
    private void OnEnable()
    {
        aguaAtual = 0f;
        plantaSatisfeita = false;
        plantasResolvidas = 0; // Limpa o contador estático para o novo início

        // Volta o sprite para o estado seco original
        if (imagemComponente != null && spriteInicial != null)
        {
            imagemComponente.sprite = spriteInicial;
        }
    }

    void Update()
    {
        // O Update sozinho já gerencia perfeitamente a entrada e permanência do mouse na UI
        if (Input.GetMouseButton(0) && !plantaSatisfeita)
        {
            if (EventSystem.current.IsPointerOverGameObject() && 
                RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition))
            {
                RegarPlanta();
            }
        }
    }

    void RegarPlanta()
    {
        aguaAtual += velocidadRegar * Time.deltaTime;
        aguaAtual = Mathf.Clamp(aguaAtual, 0f, aguaMaxima);

        Debug.Log($"{gameObject.name} recebendo água: {aguaAtual}");
        AtualizarSpriteProgresso();

        if (aguaAtual >= aguaMaxima && !plantaSatisfeita)
        {
            plantaSatisfeita = true;
            plantasResolvidas++;
            Debug.Log($"{gameObject.name} totalmente regada! Total resolvidas: {plantasResolvidas}");

            ChecarFinalJogo();
        }
    }

    void UpdateSpriteProgresso() // Traduzido internamente para manter o padrão sem quebras
    {
        AtualizarSpriteProgresso();
    }

    void AtualizarSpriteProgresso()
    {
        if (imagemComponente == null) return;

        float porcentagem = (aguaAtual / aguaMaxima) * 100f;

        if (porcentagem >= 75f && sprite75Porcento != null)
        {
            imagemComponente.sprite = sprite75Porcento;
        }
        else if (porcentagem >= 50f && sprite50Porcento != null)
        {
            imagemComponente.sprite = sprite50Porcento;
        }
        else if (porcentagem >= 25f && sprite25Porcento != null)
        {
            imagemComponente.sprite = sprite25Porcento;
        }
    }

    void ChecarFinalJogo()
    {
        if (plantasResolvidas >= 2)
        {
            Debug.Log("Minigame Plantas Concluído com Sucesso!");
            
            TriggerPlanta.minigameBloqueado = true;
            Robin.AlterarDiversao(4);    
            Robin.AlterarProgresso(2);    
            Casa.AlterarAguaPlanta(4);   
            scriptCasa.AlterarHoraio(0.75f);
            
            CameraPanLateral.minigameAtivo = false; 

            Cursor.visible = true;
            if (painelMinigame != null)
            {
                painelMinigame.SetActive(false);
            }
        }
    }
}