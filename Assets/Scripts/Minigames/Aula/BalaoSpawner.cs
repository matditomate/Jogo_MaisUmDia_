using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BalaoSpawner : MonoBehaviour
{
    public GameObject prefabBalao;

    public static BalaoSpawner instance;

    public RectTransform canvas;

    public float intervalo = 0.5f;

    public float minY = 350f;
    public float maxY = 360f;

    public float chanceMinigame = 0.7f;

    public float cooldown = 60f;

    private float tempo = 0f;

    private bool podeAparecer = true;

    bool aulaTerminou;

    public bool Pensamento;

    int indiceProfessor = 0;

    int indicePensamento = 0;

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

    public Dictionary<string, bool> palavrasVermelhas =
        new Dictionary<string, bool>()
    {
        { "inutil", true },
        { "fracasso", true },
        { "patetico", true },
        { "ridiculo", true },
        { "fraco", true },
        { "lento", true },
        { "burro", true },
        { "medroso", true },
        { "irrelevante", true },
        { "estranho", true },
        { "perdedor", true },
        { "desastre", true },
        { "falho", true },
        { "incompetento", true },
        { "quebrado", true },
        { "apagado", true },
        { "desconfortavel", true },
        { "desajeitado", true },
        { "vazio", true },
        { "cansado", true },
        { "Preste atenção", false }
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
        GameObject balao = Instantiate(prefabBalao, canvas);
        Baloes script = balao.GetComponent<Baloes>();

        if (Pensamento)
        {
            var lista = palavrasVermelhas.ToList();

            if (lista.Count == 0) return;

            if (indicePensamento >= lista.Count)
                indicePensamento = 0;

            var item = lista[indicePensamento];

            RectTransform rect = balao.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(
                Random.Range(-328, 328),
                Random.Range(-450, -50)
            );

            script.Fala.text = item.Key;
            script.Fala.color = item.Value ? Color.red : Color.green;

            indicePensamento++;
        }
        else
        {
            RectTransform rect = balao.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(273f, 252f);

            float valor = Random.Range(-1f, 1f);

            float resultado1 =
                Mathf.Sign(valor) *
                Mathf.Pow(Mathf.Abs(valor), 2f) * 2f;

            float resultado2 =
                Mathf.Sign(valor) *
                Mathf.Pow(Mathf.Abs(valor), 0.5f) * 2f;

            script.modificador = (resultado1 + resultado2) / 2f;

            if (indiceProfessor >= palavrasProfessor.Length)
            {
                aulaTerminou = true;
                Debug.Log("Fim da aula");
                return;
            }

            script.Fala.text = palavrasProfessor[indiceProfessor];
            indiceProfessor++;
        }
    }

    void TentarMinigame()
    {
        if (!podeAparecer) return;

        if (Random.value <= chanceMinigame)
        {
            Pensamento = true;

            CameraMinigame.instance?.MoverCamera(0f, -2.5f);

            StartCoroutine(CooldownMinigame(cooldown));
        }
    }

    IEnumerator CooldownMinigame(float x)
    {
        podeAparecer = false;

        yield return new WaitForSeconds(x);

        podeAparecer = true;
    }
}