using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class BalaoSpawner : MonoBehaviour
{
    public GameObject prefabBalao;

    public static BalaoSpawner instance;

    public RectTransform canvas;

    public BalaoSpawnerPensamentos balaoSpawnerPensamentos;

    public float intervalo = 0.5f;

    public float minY = 350f;
    public float maxY = 360f;

    public float chanceMinigame = 0.7f;

    public float cooldown = 60f;

    private float tempo = 0f;

    private bool podeAparecer = true;

    public bool aulaTerminou;

    public bool Pensamento;

    int indiceProfessor = 0;

    int indiceBlaBla = 0;

    private string[] BlaBla =
    {
        "Bla",
        "Bla",
        "Bla",
        "Bla"
    };

    private string[] palavrasProfessor =
    {
        "design é",
        "sobre intencao",
        "cada elemento",
        "precisa comunicar",
        "alguma coisa",
        "nao coloquem",
        "cor so",
        "porque ficou",
        "bonito perguntem",
        "o motivo",
        "dessa escolha",
        "a hierarquia",
        "visual organiza",
        "a leitura",
        "o olho",
        "sempre procura",
        "contraste primeiro",
        "o espaco",
        "vazio tambem",
        "é informacao",
        "menos elementos",
        "podem criar",
        "mais impacto",
        "o alinhamento",
        "errado causa",
        "desconforto visual",
        "a tipografia",
        "transmite personalidade",
        "uma fonte",
        "muda completamente",
        "a percepcao",
        "o design",
        "bom parece",
        "simples mas",
        "existe muito",
        "pensamento por",
        "tras o",
        "usuario nao",
        "deve precisar",
        "adivinhar a",
        "clareza vem",
        "antes de",
        "estilo a",
        "repeticao cria",
        "consistencia a",
        "consistencia gera",
        "confianca formas",
        "arredondadas passam",
        "sensacao amigavel",
        "formas rigidas",
        "parecem mais",
        "serias o",
        "equilibrio visual",
        "evita poluicao",
        "nem tudo",
        "precisa chamar",
        "atencao ao",
        "mesmo tempo",
        "use contraste",
        "para guiar",
        "o olhar",
        "cores quentes",
        "trazem energia",
        "cores frias",
        "passam calma",
        "todo detalhe",
        "influencia a",
        "experiencia design",
        "nao é",
        "decoracao design",
        "resolve problemas",
        "o usuario",
        "é mais",
        "importante que",
        "o criador",
        "animacoes precisam",
        "ter proposito",
        "movimento excessivo",
        "distrai observem",
        "como aplicativos",
        "organizam informacao",
        "design invisivel",
        "geralmente funciona",
        "melhor uma",
        "interface boa",
        "reduz esforco",
        "mental nao",
        "tenham medo",
        "de remover",
        "elementos a",
        "simplicidade exige",
        "decisao cada",
        "pixel ocupa",
        "espaco valioso",
        "a composicao",
        "cria ritmo",
        "visual o",
        "cerebro gosta",
        "de padroes",
        "quebrar padroes",
        "tambem pode",
        "gerar destaque",
        "a legibilidade",
        "sempre vem",
        "primeiro o",
        "tamanho do",
        "texto muda",
        "a prioridade",
        "margens ajudam",
        "a respirar",
        "design é",
        "comunicacao visual",
        "a estetica",
        "sem funcao",
        "perde valor",
        "referencias ajudam",
        "a construir",
        "repertorio todo",
        "designer precisa",
        "observar o",
        "mundo bons",
        "designers percebem",
        "detalhes pequenos",
        "o processo",
        "importa tanto",
        "quanto o",
        "resultado prototipos",
        "evitam problemas",
        "futuros feedback",
        "faz parte",
        "da criacao",
        "design é",
        "teste constante",
        "a primeira",
        "ideia raramente",
        "é a",
        "melhor a",
        "experimentacao gera",
        "solucoes inesperadas",
        "a clareza",
        "supera a",
        "complexidade bons",
        "layouts conduzem",
        "naturalmente o",
        "usuario design",
        "emocional cria",
        "conexao o",
        "usuario sente",
        "antes de",
        "analisar cada",
        "escolha visual",
        "conta uma",
        "historia"
    };

    void Awake()
    {
        instance = this;
        Pensamento = false;
        StartCoroutine(CooldownMinigame(10));
    }

    void Update()
    {
        if (aulaTerminou) return;

        tempo += Time.deltaTime;

        if (tempo >= intervalo)
        {
            tempo = 0f;
            CriarBalao();
            TentarMinigame();
        }
    }

    void CriarBalao()
    {


        if (Pensamento)
        {
            if (indiceBlaBla < BlaBla.Length)
            {
                balaoSpawnerPensamentos.enabled = true;
                BalaoProfessor(ref indiceBlaBla, BlaBla);
            }
            else
            {
                indiceBlaBla = 0;
                this.enabled =false;
            }
        }
        else
        {
            if (indiceProfessor < palavrasProfessor.Length)
            {
                BalaoProfessor(ref indiceProfessor, palavrasProfessor);
            }
            else
            {
                aulaTerminou = true;
                Debug.Log("Fim da aula");
            }
        }
    }

    public void TentarMinigame()
    {
        if (!podeAparecer) return;

        if (Random.value <= chanceMinigame)
        {
            Pensamento = true;

            StartCoroutine(CooldownMinigame(cooldown));
        }
    }

     IEnumerator CooldownMinigame(float x)
    {
        podeAparecer = false;

        yield return new WaitForSeconds(x);

        podeAparecer = true;
    }
    
    void BalaoProfessor(ref int x, string[] y)
    {
        GameObject balao = Instantiate(prefabBalao, canvas);
        Baloes script = balao.GetComponent<Baloes>();

        RectTransform rect = balao.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(300f, 68f);

        float valor = Random.Range(-1f, 1f);

        float resultado1 =
            Mathf.Sign(valor) *
            Mathf.Pow(Mathf.Abs(valor), 2f) * 2f;

        float resultado2 =
            Mathf.Sign(valor) *
            Mathf.Pow(Mathf.Abs(valor), 0.5f) * 2f;

        script.modificador = (resultado1 + resultado2) / 2f;

        script.tipoBalao = true;

        script.Fala.text = y[x];
        x++;
    }
}