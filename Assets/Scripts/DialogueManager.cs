using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private enum PanelType
    {
        Dialogue,
        Thought
    }

    [System.Serializable]
    public class SpeakerPortrait
    {
        public string speakerName;
        public Sprite portrait;
    }

    [Header("Canvas do Diálogo")]
    [SerializeField] private Canvas canvasDialogoCanvas;

    private CanvasGroup canvasDialogoGroup;
    private GraphicRaycaster canvasDialogoRaycaster;

    [Header("Sort Order do Canvas")]
    [SerializeField] private int sortOrderNaFrente = 1000;
    [SerializeField] private int sortOrderAtras = -100;

    [Header("Ink")]
    [SerializeField] private TextAsset inkJson;

    [Header("Resultado do Ink")]
    [SerializeField] private string resultVariableName = "resultado_dialogo";

    [Header("Painel Diálogo")]
    [SerializeField] private GameObject painelDialogo;
    [SerializeField] private TMP_Text textoFala;
    [SerializeField] private TMP_Text textoNome;
    [SerializeField] private Image personagemPortrait;

    [Header("Painel Pensamento")]
    [SerializeField] private GameObject painelPensamento;
    [SerializeField] private TMP_Text textoPensamento;

    [Header("Painel Escolha")]
    [SerializeField] private GameObject painelEscolha;
    [SerializeField] private TMP_Text textoPergunta;
    [SerializeField] private Button botaoEscolha1;
    [SerializeField] private Button botaoEscolha2;

    [Header("Portraits")]
    [SerializeField] private SpeakerPortrait[] speakerPortraits;

    [Header("Typing")]
    [SerializeField] private float typingSpeed = 0.03f;

    private Story story;

    private bool dialogueActive;
    private bool isTyping;

    private string currentLine;

    private Coroutine typingCoroutine;

    private PanelType currentPanel = PanelType.Dialogue;
    private PanelType previousPanel = PanelType.Dialogue;

    private Action<string> onDialogueFinished;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        PrepararCanvasDialogo();

        if (inkJson != null)
            story = new Story(inkJson.text);
        else
            Debug.LogError("Ink Json não foi colocado no DialogueManager.");

        HideAllPanels();
        EsconderCanvasDialogo();
    }

    private void PrepararCanvasDialogo()
    {
        if (canvasDialogoCanvas == null)
        {
            canvasDialogoCanvas = GetComponentInChildren<Canvas>(true);
        }

        if (canvasDialogoCanvas == null)
        {
            Debug.LogError("Nenhum Canvas foi encontrado dentro do prefab do DialogueManager.");
            return;
        }

        canvasDialogoCanvas.overrideSorting = true;

        canvasDialogoGroup = canvasDialogoCanvas.GetComponent<CanvasGroup>();
        canvasDialogoRaycaster = canvasDialogoCanvas.GetComponent<GraphicRaycaster>();

        Debug.Log("Canvas do diálogo encontrado: " + GetPath(canvasDialogoCanvas.transform));
    }

    private string GetPath(Transform obj)
    {
        string path = obj.name;

        while (obj.parent != null)
        {
            obj = obj.parent;
            path = obj.name + "/" + path;
        }

        return path;
    }

    public void ColocarDialogoNaFrente()
    {
        if (canvasDialogoCanvas == null)
            return;

        canvasDialogoCanvas.overrideSorting = true;
        canvasDialogoCanvas.sortingOrder = sortOrderNaFrente;

        Debug.Log("Diálogo na frente. Sort Order: " + canvasDialogoCanvas.sortingOrder);
    }

    public void ColocarDialogoAtras()
    {
        if (canvasDialogoCanvas == null)
            return;

        canvasDialogoCanvas.overrideSorting = true;
        canvasDialogoCanvas.sortingOrder = sortOrderAtras;

        Debug.Log("Diálogo atrás. Sort Order: " + canvasDialogoCanvas.sortingOrder);
    }

    private void MostrarCanvasDialogo()
    {
        if (canvasDialogoCanvas == null)
            return;

        canvasDialogoCanvas.gameObject.SetActive(true);

        ColocarDialogoNaFrente();

        if (canvasDialogoGroup != null)
        {
            canvasDialogoGroup.alpha = 1f;
            canvasDialogoGroup.interactable = true;
            canvasDialogoGroup.blocksRaycasts = true;
        }

        if (canvasDialogoRaycaster != null)
            canvasDialogoRaycaster.enabled = true;
    }

    private void EsconderCanvasDialogo()
    {
        if (canvasDialogoCanvas == null)
            return;

        ColocarDialogoAtras();

        if (canvasDialogoGroup != null)
        {
            canvasDialogoGroup.alpha = 0f;
            canvasDialogoGroup.interactable = false;
            canvasDialogoGroup.blocksRaycasts = false;
        }

        if (canvasDialogoRaycaster != null)
            canvasDialogoRaycaster.enabled = false;

        canvasDialogoCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!dialogueActive)
            return;

        if (painelEscolha != null && painelEscolha.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
                FinishTyping();
            else
                ContinueStory();
        }
    }

    public void StartDialogue(string knot, Action<string> callback = null)
    {
        if (inkJson == null)
        {
            Debug.LogError("Ink Json não foi colocado no DialogueManager.");
            return;
        }

        if (story == null)
            story = new Story(inkJson.text);

        story.ResetState();

        onDialogueFinished = callback;

        try
        {
            story.ChoosePathString(knot);
        }
        catch
        {
            Debug.LogError($"Knot '{knot}' não encontrado no Ink.");
            return;
        }

        dialogueActive = true;
        currentPanel = PanelType.Dialogue;
        previousPanel = PanelType.Dialogue;

        MostrarCanvasDialogo();

        ContinueStory();
    }

    private void ContinueStory()
    {
        if (!story.canContinue)
        {
            if (story.currentChoices.Count > 0)
            {
                DisplayChoices();
                return;
            }

            EndDialogue();
            return;
        }

        currentLine = story.Continue().Trim();

        ReadTags();
        ShowCurrentPanel();

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(currentLine));
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;

        TMP_Text currentText = GetCurrentText();

        if (currentText != null)
            currentText.text = "";

        foreach (char letter in text)
        {
            if (currentText != null)
                currentText.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        if (story.currentChoices.Count > 0)
            DisplayChoices();
    }

    private void FinishTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        TMP_Text currentText = GetCurrentText();

        if (currentText != null)
            currentText.text = currentLine;

        isTyping = false;

        if (story.currentChoices.Count > 0)
            DisplayChoices();
    }

    private TMP_Text GetCurrentText()
    {
        if (currentPanel == PanelType.Thought)
            return textoPensamento;

        return textoFala;
    }

    private void DisplayChoices()
    {
        previousPanel = currentPanel;

        HideAllPanels();

        if (painelEscolha != null)
            painelEscolha.SetActive(true);

        if (textoPergunta != null)
            textoPergunta.text = currentLine;

        ConfigurarBotaoEscolha(botaoEscolha1, 0);
        ConfigurarBotaoEscolha(botaoEscolha2, 1);
    }

    private void ConfigurarBotaoEscolha(Button botao, int choiceIndex)
    {
        if (botao == null)
            return;

        if (choiceIndex >= story.currentChoices.Count)
        {
            botao.gameObject.SetActive(false);
            return;
        }

        botao.gameObject.SetActive(true);

        TMP_Text textoDoBotao = botao.GetComponentInChildren<TMP_Text>(true);

        if (textoDoBotao != null)
        {
            textoDoBotao.text = story.currentChoices[choiceIndex].text;
        }
        else
        {
            Debug.LogWarning("Nenhum TMP_Text encontrado dentro do botão: " + botao.name);
        }

        botao.onClick.RemoveAllListeners();

        botao.onClick.AddListener(() =>
        {
            EscolherOpcao(choiceIndex);
        });
    }

    private void EscolherOpcao(int choiceIndex)
    {
        story.ChooseChoiceIndex(choiceIndex);

        if (painelEscolha != null)
            painelEscolha.SetActive(false);

        currentPanel = previousPanel;

        ContinueStory();
    }

    private void ReadTags()
    {
        foreach (string tag in story.currentTags)
        {
            string[] parts = tag.Split(':');

            if (parts.Length < 2)
                continue;

            string key = parts[0].Trim().ToLower();
            string value = parts[1].Trim();

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

        if (panelName == "dialogue" || panelName == "dialogo")
        {
            currentPanel = PanelType.Dialogue;
        }
        else if (panelName == "thought" || panelName == "pensamento")
        {
            currentPanel = PanelType.Thought;
        }
    }

    private string GetNomeVisivel(string speakerName)
    {
        if (string.IsNullOrEmpty(speakerName))
            return "";

        for (int i = 1; i < speakerName.Length; i++)
        {
            if (char.IsUpper(speakerName[i]))
            {
                return speakerName.Substring(0, i);
            }
        }

        return speakerName;
    }

    private void SetSpeaker(string speakerName)
    {
        if (textoNome != null)
            textoNome.text = GetNomeVisivel(speakerName);

        if (personagemPortrait == null)
            return;

        foreach (SpeakerPortrait speakerPortrait in speakerPortraits)
        {
            if (speakerPortrait.speakerName == speakerName)
            {
                personagemPortrait.sprite = speakerPortrait.portrait;
                return;
            }
        }

        Debug.LogWarning($"Retrato não configurado para o personagem: {speakerName}");
    }

    private void ShowCurrentPanel()
    {
        HideAllPanels();

        if (currentPanel == PanelType.Dialogue)
        {
            if (painelDialogo != null)
                painelDialogo.SetActive(true);
        }
        else
        {
            if (painelPensamento != null)
                painelPensamento.SetActive(true);
        }
    }

    private void HideAllPanels()
    {
        if (painelDialogo != null)
            painelDialogo.SetActive(false);

        if (painelPensamento != null)
            painelPensamento.SetActive(false);

        if (painelEscolha != null)
            painelEscolha.SetActive(false);
    }

    private string GetDialogueResult()
    {
        if (story == null)
            return "";

        try
        {
            object value = story.variablesState[resultVariableName];

            if (value != null)
                return value.ToString();
        }
        catch
        {
            Debug.LogWarning(
                $"Variável '{resultVariableName}' não encontrada no Ink. " +
                $"Crie no Ink: VAR {resultVariableName} = \"\""
            );
        }

        return "";
    }

    private void EndDialogue()
    {
        dialogueActive = false;
        HideAllPanels();

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        isTyping = false;

        if (textoFala != null)
            textoFala.text = "";

        if (textoPensamento != null)
            textoPensamento.text = "";

        if (textoPergunta != null)
            textoPergunta.text = "";

        string resultado = GetDialogueResult();

        Action<string> callback = onDialogueFinished;
        onDialogueFinished = null;

        callback?.Invoke(resultado);

        EsconderCanvasDialogo();
    }

    public bool IsDialogueActive()
    {
        return dialogueActive;
    }

    public void CloseDialogue()
    {
        EndDialogue();
    }
}