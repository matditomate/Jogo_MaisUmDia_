using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    // Instância única do DialogueManager.
    // Isso permite chamar DialogueManager.Instance de outros scripts.
    public static DialogueManager Instance;

    // Tipos de painel que o sistema pode usar.
    // Dialogue = painel com personagem, nome, retrato e fala.
    // Thought = painel de pensamento, com apenas texto.
    private enum PanelType
    {
        Dialogue,
        Thought
    }

    [Header("Ink")]
    // Arquivo JSON gerado a partir do arquivo .ink.
    [SerializeField] private TextAsset inkJson;

    [Header("Painel Diálogo")]
    // Painel usado para falas normais de personagens.
    [SerializeField] private GameObject painelDialogo;

    // Texto principal da fala no painel de diálogo.
    [SerializeField] private TMP_Text textoFala;

    // Texto que mostra o nome do personagem falando.
    [SerializeField] private TMP_Text textoNome;

    // Imagem do retrato do personagem.
    [SerializeField] private Image personagemPortrait;

    [Header("Painel Pensamento")]
    // Painel usado para pensamentos do Robin.
    [SerializeField] private GameObject painelPensamento;

    // Texto exibido no painel de pensamento.
    [SerializeField] private TMP_Text textoPensamento;

    [Header("Painel Escolha")]
    // Painel que aparece quando o Ink encontra escolhas.
    [SerializeField] private GameObject painelEscolha;

    // Texto da pergunta ou fala antes das escolhas.
    [SerializeField] private TMP_Text textoPergunta;

    // Botões das escolhas.
    [SerializeField] private Button[] botoesEscolha;

    // Textos dentro dos botões de escolha.
    [SerializeField] private TMP_Text[] textosBotoes;

    [Header("Typing")]
    // Velocidade com que as letras aparecem na tela.
    [SerializeField] private float typingSpeed = 0.03f;

    // História atual do Ink.
    private Story story;

    // Indica se existe um diálogo ativo no momento.
    private bool dialogueActive;

    // Indica se o texto está sendo digitado letra por letra.
    private bool isTyping;

    // Guarda a fala atual.
    private string currentLine;

    // Guarda a Coroutine da digitação para poder interromper se necessário.
    private Coroutine typingCoroutine;

    // Painel atual usado pelo diálogo.
    private PanelType currentPanel = PanelType.Dialogue;

    // Painel anterior, usado para voltar depois de uma escolha.
    private PanelType previousPanel = PanelType.Dialogue;

    private void Awake()
    {
        // Garante que exista apenas um DialogueManager na cena.
        if (Instance == null)
        {
            Instance = this;

            // Faz o DialogueManager continuar existindo ao trocar de cena.
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Se já existir outro DialogueManager, destrói este.
            Destroy(gameObject);
            return;
        }

        // Cria a história do Ink se o arquivo JSON foi colocado no Inspector.
        if (inkJson != null)
            story = new Story(inkJson.text);

        // Começa com todos os painéis escondidos.
        HideAllPanels();
    }

    private void Update()
    {
        // Se não tem diálogo ativo, não faz nada.
        if (!dialogueActive)
            return;

        // Se o painel de escolha estiver aberto, o jogador deve clicar em uma opção.
        // Nesse caso, o espaço não avança o diálogo.
        if (painelEscolha.activeSelf)
            return;

        // Pressionar espaço avança o diálogo.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Se o texto ainda está aparecendo letra por letra,
            // o espaço completa a frase imediatamente.
            if (isTyping)
                FinishTyping();

            // Se o texto já terminou, o espaço passa para a próxima fala.
            else
                ContinueStory();
        }
    }

    public void StartDialogue(string knot)
    {
        // Se a história ainda não foi criada, cria usando o JSON.
        if (story == null)
            story = new Story(inkJson.text);

        // Reseta o estado da história.
        story.ResetState();

        // Tenta ir para o knot informado.
        try
        {
            story.ChoosePathString(knot);
        }
        catch
        {
            Debug.LogError($"Knot '{knot}' não encontrado no Ink.");
            return;
        }

        // Ativa o diálogo.
        dialogueActive = true;

        // Por padrão, começa no painel de diálogo.
        // O Ink pode trocar para pensamento usando a tag #panel:thought.
        currentPanel = PanelType.Dialogue;

        // Começa a mostrar a primeira fala.
        ContinueStory();
    }

    private void ContinueStory()
    {
        // Se não há mais texto para continuar...
        if (!story.canContinue)
        {
            // ...mas existem escolhas, mostra as escolhas.
            if (story.currentChoices.Count > 0)
            {
                DisplayChoices();
                return;
            }

            // Se não tem texto nem escolhas, termina o diálogo.
            EndDialogue();
            return;
        }

        // Pega a próxima linha do Ink.
        currentLine = story.Continue().Trim();

        // Lê as tags da linha atual.
        // Exemplo: #speaker:ROBIN ou #panel:thought.
        ReadTags();

        // Mostra o painel correto de acordo com a tag lida.
        ShowCurrentPanel();

        // Se já havia uma digitação acontecendo, interrompe.
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        // Começa a digitar o texto atual.
        typingCoroutine = StartCoroutine(TypeText(currentLine));
    }

    private IEnumerator TypeText(string text)
    {
        // Marca que o texto está sendo digitado.
        isTyping = true;

        // Limpa o texto do painel atual.
        GetCurrentText().text = "";

        // Escreve uma letra por vez.
        foreach (char letter in text)
        {
            GetCurrentText().text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Marca que a digitação terminou.
        isTyping = false;

        // Se após essa fala existirem escolhas, mostra o painel de escolhas.
        if (story.currentChoices.Count > 0)
            DisplayChoices();
    }

    private void FinishTyping()
    {
        // Para a Coroutine de digitação.
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        // Mostra a frase inteira de uma vez.
        GetCurrentText().text = currentLine;

        // Marca que não está mais digitando.
        isTyping = false;

        // Se existirem escolhas depois dessa fala, mostra as escolhas.
        if (story.currentChoices.Count > 0)
            DisplayChoices();
    }

    private TMP_Text GetCurrentText()
    {
        // Retorna qual texto deve ser usado dependendo do painel atual.
        if (currentPanel == PanelType.Thought)
            return textoPensamento;

        return textoFala;
    }

    private void DisplayChoices()
    {
        // Guarda o painel atual para voltar para ele depois da escolha.
        previousPanel = currentPanel;

        // Esconde todos os painéis antes de mostrar o painel de escolha.
        HideAllPanels();

        // Ativa o painel de escolhas.
        painelEscolha.SetActive(true);

        // Mostra a fala/pergunta atual no painel de escolha.
        textoPergunta.text = currentLine;

        // Percorre todos os botões configurados no Inspector.
        for (int i = 0; i < botoesEscolha.Length; i++)
        {
            // Se existe uma escolha do Ink para esse botão...
            if (i < story.currentChoices.Count)
            {
                // Ativa o botão.
                botoesEscolha[i].gameObject.SetActive(true);

                // Coloca o texto da escolha no botão.
                textosBotoes[i].text = story.currentChoices[i].text;

                // Salva o índice da escolha.
                int choiceIndex = i;

                // Remove eventos antigos do botão para evitar cliques duplicados.
                botoesEscolha[i].onClick.RemoveAllListeners();

                // Adiciona o evento de clique para essa escolha.
                botoesEscolha[i].onClick.AddListener(() =>
                {
                    // Informa ao Ink qual escolha foi selecionada.
                    story.ChooseChoiceIndex(choiceIndex);

                    // Esconde o painel de escolha.
                    painelEscolha.SetActive(false);

                    // Volta para o painel anterior.
                    currentPanel = previousPanel;

                    // Continua a história a partir da escolha.
                    ContinueStory();
                });
            }
            else
            {
                // Se não existe escolha para esse botão, esconde ele.
                botoesEscolha[i].gameObject.SetActive(false);
            }
        }
    }

    private void ReadTags()
    {
        // Percorre todas as tags da linha atual do Ink.
        foreach (string tag in story.currentTags)
        {
            // Divide a tag usando ":".
            // Exemplo: "speaker:ROBIN" vira ["speaker", "ROBIN"].
            string[] parts = tag.Split(':');

            if (parts.Length < 2)
                continue;

            string key = parts[0].Trim().ToLower();
            string value = parts[1].Trim();

            // Decide o que fazer com cada tipo de tag.
            switch (key)
            {
                case "speaker":
                    SetSpeaker(value);
                    break;

                case "panel":
                    SetPanelByTag(value);
                    break;
            }
        }
    }

    private void SetPanelByTag(string panelName)
    {
        panelName = panelName.ToLower();

        // Tags aceitas para painel de diálogo:
        // #panel:dialogue
        // #panel:dialogo
        if (panelName == "dialogue" || panelName == "dialogo")
        {
            currentPanel = PanelType.Dialogue;
        }

        // Tags aceitas para painel de pensamento:
        // #panel:thought
        // #panel:pensamento
        else if (panelName == "thought" || panelName == "pensamento")
        {
            currentPanel = PanelType.Thought;
        }
    }

    private void SetSpeaker(string speakerName)
    {
        // Define o nome do personagem no painel de diálogo.
        if (textoNome != null)
            textoNome.text = speakerName;

        // Se não existe imagem configurada, para aqui.
        if (personagemPortrait == null)
            return;

        // Tenta carregar o retrato dentro da pasta Resources/Portraits.
        // Exemplo: Resources/Portraits/ROBIN
        Sprite portrait = Resources.Load<Sprite>("Portraits/" + speakerName);

        // Se encontrou o retrato, troca a imagem.
        if (portrait != null)
            personagemPortrait.sprite = portrait;

        // Se não encontrou, mostra aviso no Console.
        else
            Debug.LogWarning($"Retrato não encontrado: Resources/Portraits/{speakerName}");
    }

    private void ShowCurrentPanel()
    {
        // Antes de mostrar o painel certo, esconde todos.
        HideAllPanels();

        // Mostra o painel de diálogo ou o painel de pensamento.
        if (currentPanel == PanelType.Dialogue)
            painelDialogo.SetActive(true);
        else
            painelPensamento.SetActive(true);
    }

    private void HideAllPanels()
    {
        // Esconde painel de diálogo.
        if (painelDialogo != null)
            painelDialogo.SetActive(false);

        // Esconde painel de pensamento.
        if (painelPensamento != null)
            painelPensamento.SetActive(false);

        // Esconde painel de escolha.
        if (painelEscolha != null)
            painelEscolha.SetActive(false);
    }

    private void EndDialogue()
    {
        // Marca que o diálogo terminou.
        dialogueActive = false;

        // Esconde todos os painéis.
        HideAllPanels();

        // Limpa o texto do painel de diálogo.
        if (textoFala != null)
            textoFala.text = "";

        // Limpa o texto do painel de pensamento.
        if (textoPensamento != null)
            textoPensamento.text = "";

        // Limpa o texto da pergunta no painel de escolha.
        if (textoPergunta != null)
            textoPergunta.text = "";
    }

    public bool IsDialogueActive()
    {
        // Retorna se existe diálogo ativo.
        return dialogueActive;
    }

    public void CloseDialogue()
    {
        // Fecha o diálogo manualmente.
        EndDialogue();
    }
}