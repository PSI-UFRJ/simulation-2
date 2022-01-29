using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationControl : MonoBehaviour
{
    public enum PopulationType { Animal, Plant }

    public PopulationType type;

    private Environment env;

    private Transform[] individuals;

    [SerializeField] private int numberOfIndividuals = 0;

    [SerializeField] private float weightT = 1;
    [SerializeField] private float weightL = 1;
    [SerializeField] private float weightU = 1;
    [SerializeField] private float weightP = 1;
    [SerializeField] private float weightA = 1;

    [SerializeField] private List<float> temperatureUpperLimits;
    [SerializeField] private List<float> lightUpperLimits;
    [SerializeField] private List<float> umidityUpperLimits;
    [SerializeField] private List<int>   plantsUpperLimits;
    [SerializeField] private List<int>   animalsUpperLimits;

    private const int INCREASE = 1;
    private const int DECREASE = -1;
    private const int MAINTAIN = 0;
    private int INTERVATION = MAINTAIN;


    // Start is called before the first frame update
    void Start()
    {
        env = new Environment();
        individuals = this.gameObject.GetComponentsInChildren<Transform>(true).Where(x => x.parent == this.transform).ToArray();
        SetEnvironment(1, (float)45.5, (float)0.65);
        IntervenePopulation(env.GetTemperature(), env.GetLight(), env.GetUmidity(), 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IntervenePopulation(float temperature, float light, float umidity, int numOfPlants, int numOfAnimals)
    {
        // a cada período de tempo aumentar, diminuir ou fazer nada com o número de indivíduos da população

        ControlPopulation(temperature, light, umidity, numOfPlants, numOfAnimals);

        if(numberOfIndividuals > 0)
        {
            numberOfIndividuals += INTERVATION;

            if (INTERVATION == INCREASE)
            {
                SpawnIndividual();
            }
            else if (INTERVATION == DECREASE)
            {
                KillIndividual();
            }
        }
    }

    private void SpawnIndividual()
    {
        if ((individuals == null) || (individuals.Length == 0))
        {
            return;
        }

        foreach(Transform t in individuals)
        {
            if (!t.gameObject.activeSelf)
            {
                t.gameObject.SetActive(true);
                break;
            }
        }
    }

    private void KillIndividual()
    {
        if ((individuals == null) || (individuals.Length == 0))
        {
            return;
        }

        foreach (Transform t in individuals)
        {
            if (t.gameObject.activeSelf)
            {
                t.gameObject.SetActive(false);
                break;
            }
        }
    }

    public void ControlPopulation(float temperature, float light, float umidity, int numOfPlants, int numOfAnimals)
    {
        float result = 0;

        if (type == PopulationType.Plant)
        {
            result = (weightT * IntervationTemperature(temperature) + weightL * IntervationLight(light) + weightU * IntervationUmidity(umidity)) / (weightT + weightL + weightU);
        }
        else if(type == PopulationType.Animal)
        {
            result = (weightP * IntervationPlants(numOfPlants) + (weightA * IntervationAnimals(numOfAnimals))) / (weightP + weightA);
        }

        INTERVATION = (result > 0) ? INCREASE : (result == 0) ? MAINTAIN : DECREASE;

    }

    private int IntervationTemperature(float temperature) //  Calcular se no ponto de vista da temperatura, a população deveria crescer, diminuir ou ficar igual
    {
        if(temperature <= temperatureUpperLimits[0])
        {
            return INCREASE;
        }
        else if (temperature <= temperatureUpperLimits[1])
        {
            return MAINTAIN;
        }
        else if (temperature <= temperatureUpperLimits[2])
        {
            return DECREASE;
        }
        else
        {
            return DECREASE;
        }
    }

    private int IntervationLight(float light) //  Calcular se no ponto de vista da luz, a população deveria crescer, diminuir ou ficar igual
    {
        if (light <= lightUpperLimits[0])
        {
            return INCREASE;
        }
        else if (light <= lightUpperLimits[1])
        {
            return MAINTAIN;
        }
        else if (light <= lightUpperLimits[2])
        {
            return DECREASE;
        }
        else
        {
            return DECREASE;
        }
    }

    private int IntervationUmidity(float umidity) //  Calcular se no ponto de vista da umidade, a população deveria crescer, diminuir ou ficar igual
    {
        if (umidity <= umidityUpperLimits[0])
        {
            return INCREASE;
        }
        else if (umidity <= umidityUpperLimits[1])
        {
            return MAINTAIN;
        }
        else if (umidity <= umidityUpperLimits[2])
        {
            return DECREASE;
        }
        else
        {
            return DECREASE;
        }
    }

    private int IntervationPlants(int numOfPlants)
    {
        if (numOfPlants <= plantsUpperLimits[0])
        {
            return INCREASE;
        }
        else if (numOfPlants <= plantsUpperLimits[1])
        {
            return MAINTAIN;
        }
        else if (numOfPlants <= plantsUpperLimits[2])
        {
            return DECREASE;
        }
        else
        {
            return DECREASE;
        }
    }
    
    private int IntervationAnimals(int numOfAnimals)
    {
        return DECREASE;
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
