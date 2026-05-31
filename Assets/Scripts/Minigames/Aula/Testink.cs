using UnityEngine;
using Ink.Runtime;

public class Testink : MonoBehaviour
{
    public TextAsset historiaJSON;

    void Start()
    {
        Debug.Log("Start executou!");

        Story historia = new Story(historiaJSON.text);

        Debug.Log("Pode continuar? " + historia.canContinue);

        if (historia.canContinue)
        {
            Debug.Log(historia.Continue());
        }

        Debug.Log("Quantidade de escolhas: " + historia.currentChoices.Count);
    }
}