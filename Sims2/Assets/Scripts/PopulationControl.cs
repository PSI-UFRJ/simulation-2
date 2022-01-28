using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationControl : MonoBehaviour
{
    public enum PopulationType { Animal, Plant }

    public PopulationType type;

    private Environment env;

    [SerializeField] private int numberOfIndividuals = 1;

    [SerializeField] private float weightT = 1;
    [SerializeField] private float weightL = 1;
    [SerializeField] private float weightU = 1;

    private const int INCREASE = 1;
    private const int DECREASE = -1;
    private const int MAINTAIN = 0;
    private int INTERVATION = MAINTAIN;


    // Start is called before the first frame update
    void Start()
    {
        SetEnvironment(1, (float)45.5, (float)0.65);
    }

    // Update is called once per frame
    void Update()
    {
        IntervenePopulation();
    }

    private void IntervenePopulation()
    {
        // a cada período de tempo aumentar, diminuir ou fazer nada com o número de indivíduos da população
        if(numberOfIndividuals > 0)
        {
            numberOfIndividuals += INTERVATION;
        }
    }

    public void ControlPopulation(float temperature, float light, float umidity)
    {
        float result = (weightT * IntervationTemperature(temperature) + weightL * IntervationLight(light) + weightU * IntervationUmidity(umidity)) / (weightT + weightL + weightU);

        // Fazer if-else para caso o resultado caia num range, muda o INTERVATION para INCREASE ou DECREASE ou MAINTAIN
    }

    private int IntervationTemperature(float temperature) //  Calcular se no ponto de vista da temperatura, a população deveria crescer, diminuir ou ficar igual
    {
        return INCREASE;
    }

    private int IntervationLight(float light) //  Calcular se no ponto de vista da luz, a população deveria crescer, diminuir ou ficar igual
    {
        return DECREASE;
    }

    private int IntervationUmidity(float umidity) //  Calcular se no ponto de vista da umidade, a população deveria crescer, diminuir ou ficar igual
    {
        return MAINTAIN;
    }

    public void SetEnvironment(float temperature, float light, float umidity)
    {
        env.SetLight(light);
        env.SetTemperature(temperature);
        env.SetUmidity(umidity);
    }

}

class Environment
{
    private float temperature;
    private float light;
    private float umidity;

    public float GetTemperature()
    {
        return temperature;
    }

    public float GetLight()
    {
        return light;
    }

    public float GetUmidity()
    {
        return umidity;
    }

    public void SetTemperature(float t)
    {
        temperature = t;
    }

    public void SetLight(float l)
    {
        light = l;
    }

    public void SetUmidity(float u)
    {
        umidity = u;
    }
}
