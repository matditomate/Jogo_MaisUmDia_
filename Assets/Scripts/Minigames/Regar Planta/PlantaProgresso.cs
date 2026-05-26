using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Usamos IPointerEnterHandler para detectar quando o mouse do regador passa por cima da planta
public class PlantaProgresso : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private float aguaAtual = 0f;
    [SerializeField] private float aguaMaxima = 100f;
    [SerializeField] private float velocidadeRegar = 25f; // Quanto enche por segundo

    private bool plantaSatisfeita = false;

    // Referência estática para controlar o fim do minigame (2 plantas no total)
    private static int plantasResolvidas = 0;
    [SerializeField] private GameObject painelMinigame; // Para fechar no final

    [Header("Evolução da Planta")]
    [SerializeField] private Sprite sprite25Porcento;   // Imagem quando passar de 25%
    [SerializeField] private Sprite sprite50Porcento;   // Imagem quando passar de 50%
    [SerializeField] private Sprite sprite75Porcento;   // Imagem quando passar de 75%

    private Image imagemComponente;

    void Start()
    {
        plantasResolvidas = 0; // Reseta o contador ao abrir o minigame
        
        // Pega o componente de Image anexado a esta planta
        imagemComponente = GetComponent<Image>();
    }

    // Detecta que o regador está posicionado sobre a planta
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Se o jogador estiver segurando o clique esquerdo E a planta não terminou de ser regada
        if (Input.GetMouseButton(0) && !plantaSatisfeita)
        {
            RegarPlanta();
        }
    }

    // Também checa se continua segurando enquanto move o mouse sobre ela
    void Update()
    {
        if (Input.GetMouseButton(0) && !plantaSatisfeita)
        {
            // Verifica se o mouse ainda está em cima deste objeto UI
            if (EventSystem.current.IsPointerOverGameObject() && 
                RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition))
            {
                RegarPlanta();
            }
        }
    }

    void RegarPlanta()
    {
        aguaAtual += velocidadeRegar * Time.deltaTime;
        aguaAtual = Mathf.Clamp(aguaAtual, 0f, aguaMaxima);

        Debug.Log($"{gameObject.name} recebendo água: {aguaAtual}");
        AtualizarSpriteProgresso();
        if (aguaAtual >= aguaMaxima && !plantaSatisfeita)
        {
            plantaSatisfeita = true;
            plantasResolvidas++;
            Debug.Log($"{gameObject.name} totalmente regada!");

            ChecarFinalJogo();
        }
    }

    void AtualizarSpriteProgresso()
    {
        if (imagemComponente == null) return;

        // Calcula a porcentagem atual (0 a 100)
        float porcentagem = (aguaAtual / aguaMaxima) * 100f;

        // Troca o sprite de acordo com as metas que você pediu
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
        // Se as duas plantas coletaram água máxima
        if (plantasResolvidas >= 2)
        {
            Debug.Log("Minigame Plantas Concluído!");
            
            // Aplica os status estáticos na Robin e trava o minigame
            TriggerPlanta.minigameBloqueado = true;
            Robin.AlterarDiversao(4);    // Aumenta a diversão
            Robin.AlterarProgresso(2);    // Progresso positivo na depressão
            Casa.AlterarAguaPlanta(4);   // Aumenta quanto de água tem na planta
            
            CameraPanLateral.minigameAtivo = false; // DESCONGELA A CÂMERA E AS PORTAS!

            // Restaura o mouse padrão e fecha o minigame
            Cursor.visible = true;
            if (painelMinigame != null)
            {
                painelMinigame.SetActive(false);
            }
        }
    }
}