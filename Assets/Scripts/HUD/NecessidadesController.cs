using UnityEngine;
using UnityEngine.UI;

public class NecessidadeController : MonoBehaviour
{

    [SerializeField] private Image necessidadeUIBar;
    [SerializeField] private string objeto;
    [SerializeField] private string necessidade;

    //Códigos de cores para as barras:
    private string verde = "#66C67D";
    private string amarelo = "#EDB144";
    private string vermelho = "#E43636";

    void Update()
    {
        AlterarBarra();
    }

    void AlterarBarra()
    {
        var dados = DataManager.instance;
        if (dados == null || necessidadeUIBar == null)
            return;

        int? valor = PegarValor(dados);
        if (!valor.HasValue)
            return;

        int valorNecessidade = valor.Value;
        necessidadeUIBar.fillAmount = TamanhoBarra(valorNecessidade);
        necessidadeUIBar.color = CorBarra(valorNecessidade);
    }

    int? PegarValor(DataManager dados)
    {
        return (objeto, necessidade) switch
        {
            ("Robin", "fome") => dados.saciedadeRobin,
            ("Robin", "higiene") => dados.higieneRobin,
            ("Robin", "energia") => dados.energiaRobin,
            ("Robin", "diversao") => dados.diversaoRobin,
            ("Robin", "ansiedade") => dados.ansiedadeRobin,
            ("Casa", "aguaPlanta") => dados.aguaPlanta,
            ("Cafe", "fome") => dados.saciedadeCafe,
            ("Cafe", "atencao") => dados.atencaoCafe,
            _ => null
        };
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
