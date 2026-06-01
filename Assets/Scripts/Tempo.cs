using UnityEngine;

public class Tempo : MonoBehaviour
{

    [SerializeField] private float escalaDeTempo = 1f; // 1 segundo real = 1 minuto no jogo
    private float minutosFlutuantes = 0f;
    public int totalMinutosDoJogo = 0;
    public bool tempoAtivo = true;
    void Update()
    {

        // Acumula a passagem dos segundos reais multiplicados pela sua escala
        if (!tempoAtivo) return;

        minutosFlutuantes += Time.deltaTime * escalaDeTempo;

        // Quando acumular 1 minuto inteiro de jogo...
        if (minutosFlutuantes >= 1f)
        {
            totalMinutosDoJogo += 1;
            minutosFlutuantes -= 1f; // Reseta o tique mantendo o que sobrou

            // Aqui você pode chamar a fórmula para atualizar a tela!
            CalcularRelogio(totalMinutosDoJogo);

        }
    }

    public void CalcularRelogio(int totalMinutosAcumulados)
    {
        // Divisão inteira pega quantos blocos de 24h já se passaram
        int dias = totalMinutosAcumulados / 1440;

        // O resto (%) tira os dias e descobre as horas que sobraram
        int horas = (totalMinutosAcumulados % 1440) / 60;

        // O resto final são os minutos vigentes daquela hora
        int minutos = totalMinutosAcumulados % 60;

        // Exemplo de exibição no formato 00:00
        string relogioTexto = $"Dia {dias} - {horas:D2}:{minutos:D2}";
        // Debug.Log(relogioTexto);
    }

    // Formula que calcula baseado na ansiedade
    public float CalcularVelocidadeDoTempo(float velocidadeBase, float ansiedadeAtual)
    {
        // Se a ansiedade for 0, o multiplicador é 1 (velocidade normal)
        // Se a ansiedade for 10, o multiplicador vira 2 (o tempo passa o dobro do rápido!)
        // pode mudar a formula para aumentar ainda mais o multiplicador
        float modificadorEmocional = 1f + (ansiedadeAtual / 10f);

        return velocidadeBase * modificadorEmocional;
    }
    public void PausarTempo() => tempoAtivo = false;
    public void RetomarTempo() => tempoAtivo = true;
}