using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class BalaoSpawnerPensamentos : MonoBehaviour
{
    public GameObject prefabBalao;

    public static BalaoSpawnerPensamentos instance;

    public RectTransform canvas;

    public BalaoSpawner balaoSpawner;

    public float intervalo = 0.5f;

    public float minY = 350f;

    public float maxY = 360f;

    public float cooldown = 60f;

    private float tempo = 0f;

    int indicePensamento = 0;

    private List<KeyValuePair<string, bool>> listaEmbaralhada;

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
    }

    void Start()
    {
        EmbaralharPalavras();
    }

    void EmbaralharPalavras()
    {
        listaEmbaralhada = palavrasVermelhas
            .OrderBy(x => Random.value)
            .ToList();
    }

    void Update()
    {
        if (BalaoSpawner.instance.aulaTerminou) return;

        tempo += Time.deltaTime;

        if (tempo >= intervalo)
        {
            tempo = 0f;
            CriarBalaoP();
        }
    }

    void CriarBalaoP()
    {
        if (BalaoSpawner.instance.Pensamento)
        {
            if (listaEmbaralhada.Count == 0) return;

            if (indicePensamento >= listaEmbaralhada.Count)
            {
                indicePensamento = 0;
            }

            GameObject balao = Instantiate(prefabBalao, canvas);
            Baloes script = balao.GetComponent<Baloes>();

            var item = listaEmbaralhada[indicePensamento];

            RectTransform rect = balao.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(
                Random.Range(-400, 350),
                Random.Range(-200, 190)
            );

            script.Fala.text = item.Key;
            script.Fala.color = item.Value ? Color.red : Color.green;

            script.tipoBalao = false;

            indicePensamento++;
        }
        else
        {
            EmbaralharPalavras();
            balaoSpawner.enabled = true;
            this.enabled = false;
        }
    }
}