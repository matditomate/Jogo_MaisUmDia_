using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;

public class NecessidadeController : MonoBehaviour
{

    [SerializeField] private Image necessidadeUIBar;
    [SerializeField] private string objeto;
    [SerializeField] private string necessidade;

    //Códigos de cores para as barras:
    private string verde = "#66C67D";
    private string amarelo = "#EDB144";
    private string vermelho = "#E43636";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AlterarBarra();
    }

    // Update is called once per frame
    void Update()
    {
        AlterarBarra();
    }

    void AlterarBarra()
    {
        int valorNecessidade = (int)PegarValorDinamico();
        necessidadeUIBar.fillAmount = TamanhoBarra(valorNecessidade);
        necessidadeUIBar.color = CorBarra(valorNecessidade);
    }

    object PegarValorDinamico()
    {
        Type tipoDaClasse = Type.GetType(objeto);

        object instanciaDaClasse = FindObjectOfType(tipoDaClasse);

        FieldInfo campoReal = tipoDaClasse.GetField(necessidade);

        if (campoReal != null)
        {
            return campoReal.GetValue(instanciaDaClasse);
        }

        Debug.LogError($"Não achei o atributo {necessidade} na classe {objeto}");
        return null;
    }

    //Calcular a porcentagem do tamanho da barra
    float TamanhoBarra(int valorNecessidade)
    {
        float porcentagem;

        if(valorNecessidade >= 5)
        {
            porcentagem = (valorNecessidade + 0.1f)/10f;
        }
        else
        {
            porcentagem = (valorNecessidade - 0.1f)/10f;
        }

        return porcentagem;
    }

    //Mudar a cor da barra dependendo do status
    Color CorBarra(int valorNecessidade)
    {
        string corHex;

        if(necessidade != "ansiedade")
        {
            if(valorNecessidade >= 8)
            {
                corHex = verde;
            } else if(valorNecessidade >= 4)
            {
                corHex = amarelo;
            } else
            {
                corHex = vermelho;
            }
        }
        else
        {
            if(valorNecessidade >= 8)
            {
                corHex = vermelho;
            } else if(valorNecessidade >= 4)
            {
                corHex = amarelo;
            } else
            {
                corHex = verde;
            }
        }
        
        if(ColorUtility.TryParseHtmlString(corHex, out Color cor))
        {
            return cor;
        }

        return Color.white;
    }
}