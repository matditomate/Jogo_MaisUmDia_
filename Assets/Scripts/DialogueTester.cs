using System.Collections;
using UnityEngine;

public class DialogueTester : MonoBehaviour
{
    private enum EtapaTeste
    {
        Nenhuma,
        DepoisRota1Manha,
        DepoisRota2Atrasado,
        EscolhendoAtividades,
        DepoisAtividade,
        DepoisPrepararFaculdade,
        DepoisEstacaoMetro,
        DepoisMinigameAnsiedade,
        DepoisAula
    }

    [Header("Teste")]
    [SerializeField] private bool iniciarAutomaticamente = true;

    [Header("Status do Robin")]
    [SerializeField] private int fomeRobin = 0;
    [SerializeField] private int energiaRobin = 0;
    [SerializeField] private int ansiedadeRobin = 0;
    [SerializeField] private int progressoRobin = 0;
    [SerializeField] private int higieneRobin = 0;

    [Header("Status do Café")]
    [SerializeField] private int fomeCafe = 0;
    [SerializeField] private int carinhoCafe = 0;

    [Header("Status das Plantas")]
    [SerializeField] private int aguaPlantas = 0;

    private int atividadesFeitas = 0;

    private bool fezComida = false;
    private bool tomouBanho = false;
    private bool alimentouGato = false;
    private bool fezDoomscrolling = false;
    private bool regouPlantas = false;

    private EtapaTeste etapaAtual = EtapaTeste.Nenhuma;

    private void Start()
    {
        if (iniciarAutomaticamente)
            StartCoroutine(IniciarQuandoDialogueManagerExistir());
    }

    private IEnumerator IniciarQuandoDialogueManagerExistir()
    {
        while (DialogueManager.Instance == null)
            yield return null;

        IniciarPrimeiroDia();
    }

    private void Update()
    {
        if (DialogueManager.Instance == null)
            return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            IniciarPrimeiroDia();
            return;
        }

        if (DialogueManager.Instance.IsDialogueActive())
            return;

        if (etapaAtual == EtapaTeste.EscolhendoAtividades)
        {
            LerTeclasDasAtividades();
        }
    }
    private void IniciarPrimeiroDia()
    {
        ResetarTeste();

        Debug.Log("Iniciando teste do primeiro dia.");

        DialogueManager.Instance.StartDialogue("primeiro_dia", ReceberResultado);
    }

    private void ReceberResultado(string resultado)
    {
        Debug.Log("Resultado recebido do Ink: " + resultado);

        if (resultado == "despertador_levantar")
        {
            progressoRobin += 2;
            MostrarStatus();

            etapaAtual = EtapaTeste.DepoisRota1Manha;
            DialogueManager.Instance.StartDialogue("rota_1_manha", ReceberResultado);
            return;
        }

        if (resultado == "despertador_snooze")
        {
            fomeRobin += 3;
            energiaRobin += 2;
            ansiedadeRobin += 1;
            progressoRobin -= 2;
            MostrarStatus();

            etapaAtual = EtapaTeste.DepoisRota2Atrasado;
            DialogueManager.Instance.StartDialogue("rota_2_atrasado", ReceberResultado);
            return;
        }

        ContinuarPelaEtapaAtual();
    }

    private void ContinuarPelaEtapaAtual()
    {
        switch (etapaAtual)
        {
            case EtapaTeste.DepoisRota1Manha:
                etapaAtual = EtapaTeste.EscolhendoAtividades;
                MostrarMenuAtividades();
                break;

            case EtapaTeste.DepoisRota2Atrasado:
                etapaAtual = EtapaTeste.DepoisPrepararFaculdade;
                DialogueManager.Instance.StartDialogue("preparar_para_faculdade", ReceberResultado);
                break;

            case EtapaTeste.DepoisAtividade:
                if (atividadesFeitas >= 3)
                {
                    Debug.Log("Três atividades feitas. Indo para a faculdade.");

                    etapaAtual = EtapaTeste.DepoisPrepararFaculdade;
                    DialogueManager.Instance.StartDialogue("preparar_para_faculdade", ReceberResultado);
                }
                else
                {
                    etapaAtual = EtapaTeste.EscolhendoAtividades;
                    MostrarMenuAtividades();
                }
                break;

            case EtapaTeste.DepoisPrepararFaculdade:
                etapaAtual = EtapaTeste.DepoisEstacaoMetro;
                DialogueManager.Instance.StartDialogue("estacao_metro", ReceberResultado);
                break;

            case EtapaTeste.DepoisEstacaoMetro:
                Debug.Log("Aqui entraria o minigame de ansiedade.");

                etapaAtual = EtapaTeste.DepoisMinigameAnsiedade;
                DialogueManager.Instance.StartDialogue("depois_minigame_ansiedade", ReceberResultado);
                break;

            case EtapaTeste.DepoisMinigameAnsiedade:
                Debug.Log("Aqui entraria o minigame da aula.");

                etapaAtual = EtapaTeste.DepoisAula;
                DialogueManager.Instance.StartDialogue("depois_aula", ReceberResultado);
                break;

            case EtapaTeste.DepoisAula:
                Debug.Log("Fim do teste do primeiro dia.");
                MostrarStatus();
                etapaAtual = EtapaTeste.Nenhuma;
                break;

            default:
                Debug.Log("Diálogo terminou sem etapa definida.");
                break;
        }
    }

    private void LerTeclasDasAtividades()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            FazerAtividadeComer();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            FazerAtividadeBanho();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            FazerAtividadeGato();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            FazerAtividadeDoomscrolling();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            FazerAtividadePlantas();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            IrParaFaculdade();
        }
    }

    private void FazerAtividadeComer()
    {
        if (fezComida)
        {
            Debug.Log("Robin já comeu.");
            return;
        }

        fezComida = true;
        atividadesFeitas++;

        fomeRobin -= 4;
        energiaRobin += 3;
        progressoRobin += 1;

        MostrarStatus();

        etapaAtual = EtapaTeste.DepoisAtividade;
        DialogueManager.Instance.StartDialogue("depois_atividade_comer", ReceberResultado);
    }

    private void FazerAtividadeBanho()
    {
        if (tomouBanho)
        {
            Debug.Log("Robin já tomou banho.");
            return;
        }

        tomouBanho = true;
        atividadesFeitas++;

        higieneRobin += 3;
        energiaRobin += 1;
        progressoRobin += 1;

        MostrarStatus();

        etapaAtual = EtapaTeste.DepoisAtividade;
        DialogueManager.Instance.StartDialogue("depois_atividade_banho", ReceberResultado);
    }

    private void FazerAtividadeGato()
    {
        if (alimentouGato)
        {
            Debug.Log("Café já foi alimentado.");
            return;
        }

        alimentouGato = true;
        atividadesFeitas++;

        fomeCafe -= 4;
        carinhoCafe += 2;

        MostrarStatus();

        etapaAtual = EtapaTeste.DepoisAtividade;
        DialogueManager.Instance.StartDialogue("depois_atividade_gato", ReceberResultado);
    }

    private void FazerAtividadeDoomscrolling()
    {
        if (fezDoomscrolling)
        {
            Debug.Log("Robin já ficou olhando redes sociais.");
            return;
        }

        fezDoomscrolling = true;
        atividadesFeitas++;

        ansiedadeRobin += 2;
        progressoRobin -= 2;

        MostrarStatus();

        etapaAtual = EtapaTeste.DepoisAtividade;
        DialogueManager.Instance.StartDialogue("depois_atividade_doomscrolling", ReceberResultado);
    }

    private void FazerAtividadePlantas()
    {
        if (regouPlantas)
        {
            Debug.Log("As plantas já foram regadas.");
            return;
        }

        regouPlantas = true;
        atividadesFeitas++;

        aguaPlantas += 4;

        MostrarStatus();

        etapaAtual = EtapaTeste.DepoisAtividade;
        DialogueManager.Instance.StartDialogue("depois_atividade_plantas", ReceberResultado);
    }

    private void IrParaFaculdade()
    {
        Debug.Log("Indo direto para a faculdade.");

        etapaAtual = EtapaTeste.DepoisPrepararFaculdade;
        DialogueManager.Instance.StartDialogue("preparar_para_faculdade", ReceberResultado);
    }

    private void MostrarMenuAtividades()
    {
        Debug.Log(
            "Escolha uma atividade da manhã:\n" +
            "C = Fazer comida e comer café da manhã\n" +
            "B = Tomar banho\n" +
            "G = Alimentar o Café\n" +
            "D = Ficar olhando redes sociais\n" +
            "P = Regar as plantas\n" +
            "F = Se arrumar e ir à faculdade\n\n" +
            "Atividades feitas: " + atividadesFeitas + "/3"
        );
    }

    private void ResetarTeste()
    {
        fomeRobin = 0;
        energiaRobin = 0;
        ansiedadeRobin = 0;
        progressoRobin = 0;
        higieneRobin = 0;

        fomeCafe = 0;
        carinhoCafe = 0;
        aguaPlantas = 0;

        atividadesFeitas = 0;

        fezComida = false;
        tomouBanho = false;
        alimentouGato = false;
        fezDoomscrolling = false;
        regouPlantas = false;

        etapaAtual = EtapaTeste.Nenhuma;
    }

    private void MostrarStatus()
    {
        Debug.Log(
            "STATUS ROBIN\n" +
            "Fome: " + fomeRobin + "\n" +
            "Energia: " + energiaRobin + "\n" +
            "Ansiedade: " + ansiedadeRobin + "\n" +
            "Progresso: " + progressoRobin + "\n" +
            "Higiene: " + higieneRobin + "\n\n" +

            "CAFÉ\n" +
            "Fome: " + fomeCafe + "\n" +
            "Carinho: " + carinhoCafe + "\n\n" +

            "PLANTAS\n" +
            "Água: " + aguaPlantas + "\n\n" +

            "Atividades feitas: " + atividadesFeitas + "/3"
        );
    }
}