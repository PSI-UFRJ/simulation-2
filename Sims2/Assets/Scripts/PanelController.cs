using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private List<GameObject> materialPanels;
    private int currentIndex;

    [SerializeField]
    private List<GameObject> envPanels;
    [SerializeField] GameObject environment;
    private int currentEnvPanel;


    private TerrariumController terrariumController;
    private Dictionary<int, UnityEngine.UI.Button> selectedBtns;
    private Sprite selectedBtnNormal;
    private Dictionary<GameObject, int> panelLayerMapping;

    private bool isPopupOn;
    private GameObject currPopup;
    [SerializeField] GameObject missingChoicePopup;

    private const int CONSTRUCT = 1;
    private const int OBSERVE = 2;
    private int currMode;

    private float currTemperatureFactor;
    private float currHumidityFactor;

    [SerializeField] GameObject environmentLight;
    [SerializeField] GameObject camLight;
    [SerializeField] GameObject waterDrops;
    [SerializeField] GameObject waterTrail;

    [SerializeField] GameObject LightController;
    [SerializeField] GameObject TemperatureController;
    [SerializeField] GameObject HumidityController;

    void Start()
    {
        currentIndex = 0;
        currentEnvPanel = 0;
        currMode = CONSTRUCT;

        currTemperatureFactor = TemperatureController.GetComponent<UnityEngine.UI.Slider>().value * 8 / 7400 + 13.02f;
        currHumidityFactor = HumidityController.GetComponent<UnityEngine.UI.Slider>().value * 14.2857f - 4.2857f;

        terrariumController = GameObject.Find("Terrarium").GetComponent<TerrariumController>();
        panelLayerMapping = new Dictionary<GameObject, int>()
        {
            {materialPanels[0], TerrariumController.LAYER1},
            {materialPanels[1], TerrariumController.LAYER2},
            {materialPanels[2], TerrariumController.LAYER3},
            {materialPanels[3], TerrariumController.LAYER4},
            {materialPanels[4], TerrariumController.LAYER5},
            {materialPanels[5], TerrariumController.LAYER6},
            {materialPanels[6], TerrariumController.LAYER7},
            {materialPanels[7], TerrariumController.LAYER8},
            {materialPanels[8], TerrariumController.LAYER9},
        };

        selectedBtns = new Dictionary<int, UnityEngine.UI.Button>()
        {
            {TerrariumController.LAYER1, null},
            {TerrariumController.LAYER2, null},
            {TerrariumController.LAYER3, null},
            {TerrariumController.LAYER4, null},
            {TerrariumController.LAYER5, null},
            {TerrariumController.LAYER6, null},
            {TerrariumController.LAYER7, null},
            {TerrariumController.LAYER8, null},
            {TerrariumController.LAYER9, null}
        };

        isPopupOn = false;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateControls();
    }

    public void UpdateControls()
    {
        float LightControllerValue = LightController.GetComponent<UnityEngine.UI.Slider>().value;
        UnityEngine.UI.Text LightControllerValueText = LightController.GetComponent<Transform>().Find("ValueText").GetComponent<UnityEngine.UI.Text>();

        LightControllerValueText.text = Math.Round(LightControllerValue * 66.6666,2).ToString() + "%";

        float TemperatureControllerValue = TemperatureController.GetComponent<UnityEngine.UI.Slider>().value;
        UnityEngine.UI.Text TemperatureControllerValueText = TemperatureController.GetComponent<Transform>().Find("ValueText").GetComponent<UnityEngine.UI.Text>();

        TemperatureControllerValueText.text = Math.Round(TemperatureControllerValue * 24/7400 + 48.081, 2).ToString() + "° C";


        float HumidityControllerValue = HumidityController.GetComponent<UnityEngine.UI.Slider>().value;
        UnityEngine.UI.Text HumidityControllerValueText = HumidityController.GetComponent<Transform>().Find("ValueText").GetComponent<UnityEngine.UI.Text>();

        HumidityControllerValueText.text = Math.Round(HumidityControllerValue * 142.8571 - 42.8571, 2).ToString() + "%";

    }

    public void CloseTerrarium()
    {
        if((currentIndex != TerrariumController.LAYER9 - 1) || (materialPanels == null) || (terrariumController == null))
        {
            return;
        }

        if (!WasOptionChosen())
        {
            CallMissingChoicePopup();
            return;
        }

        terrariumController.CloseBowl();
        materialPanels[currentIndex].SetActive(false);
        currMode = OBSERVE;
        environment.SetActive(true);
    }

    public void OpenTerrarium()
    {
        
    }

    public void CallMissingChoicePopup()
    {
        if (isPopupOn)
        {
            DisablePopup(currPopup);
        }

        SetPopup(missingChoicePopup, true, "");
        currPopup = missingChoicePopup;
    }

    private bool WasOptionChosen()
    {
        return selectedBtns[currentIndex + 1] != null;
    }

    public void ChangePanelUp()
    {
        if ((materialPanels == null) || (envPanels == null))
        {
            return;
        }

        if(currMode == CONSTRUCT)
        {
            if (!WasOptionChosen())
            {
                CallMissingChoicePopup();
                return;
            }

            if (currentIndex + 1 < materialPanels.Count)
            {
                materialPanels[currentIndex].SetActive(false);
                currentIndex++;
                materialPanels[currentIndex].SetActive(true);
            }
        }
        else if (currMode == OBSERVE)
        {
            if (currentEnvPanel + 1 < envPanels.Count)
            {
                envPanels[currentEnvPanel].SetActive(false);
                currentEnvPanel++;
                envPanels[currentEnvPanel].SetActive(true);
            }
        }

    }

    public void ChangePanelDown()
    {
        if ((materialPanels == null) || (envPanels == null))
        {
            return;
        }

        if (currMode == CONSTRUCT)
        {
            if (currentIndex - 1 >= 0)
            {
                materialPanels[currentIndex].SetActive(false);
                currentIndex--;
                materialPanels[currentIndex].SetActive(true);
            }
        }
        else if (currMode == OBSERVE)
        {
            if (currentEnvPanel - 1 >= 0)
            {
                envPanels[currentEnvPanel].SetActive(false);
                currentEnvPanel--;
                envPanels[currentEnvPanel].SetActive(true);
            }
        }

    }

    public void ChangeMaterial(int mat)
    {
        if ((panelLayerMapping == null) || (materialPanels == null))
        {
            return;
        }

        if (!panelLayerMapping.ContainsKey(materialPanels[currentIndex]))
        {
            return;
        }

        int layer = panelLayerMapping[materialPanels[currentIndex]];

        if (!terrariumController.IsLayerEnable(layer))
        {
            terrariumController.EnableLayer(layer);
        }

        terrariumController.SetMaterialInLayer(layer, mat);
    }

    public void SelectButton(UnityEngine.UI.Button btn)
    {

        if(selectedBtns[currentIndex + 1] != null)
        {
            selectedBtns[currentIndex + 1].GetComponent<UnityEngine.UI.Image>().sprite = selectedBtnNormal;
        }

        selectedBtns[currentIndex + 1] = btn;
        string selectedBtnImg = selectedBtns[currentIndex + 1].GetComponent<UnityEngine.UI.Image>().sprite.name.Replace("Normal", "Selected");
        selectedBtnNormal = selectedBtns[currentIndex + 1].GetComponent<UnityEngine.UI.Image>().sprite;
        selectedBtns[currentIndex + 1].GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load("UI Components/" + selectedBtnImg, typeof(Sprite)) as Sprite;        
    }

    public void SetPopup(GameObject popup, bool isActive, string message)
    {
        isPopupOn = isActive;

        if (message != "")
        {
            Transform popupText = popup.GetComponent<Transform>().Find("Image").Find("Text");
            popupText.GetComponent<UnityEngine.UI.Text>().text = message;
        }

        popup.SetActive(isActive);
    }

    public void DisablePopup(GameObject popup)
    {
        isPopupOn = false;
        popup.SetActive(false);
    }

    public void SetLight(float value)
    {
        environmentLight.GetComponent<Light>().intensity = value;

        GameObject[] oxygenParticles = GameObject.FindGameObjectsWithTag("OxygenParticle");
        GameObject[] carbonDioxideParticles = GameObject.FindGameObjectsWithTag("CarbonDioxideParticle");

        foreach (GameObject oxygenParticle in oxygenParticles) {
            oxygenParticle.GetComponent<ParticleSystem>().maxParticles = Mathf.RoundToInt(value * 6.7f);
        }

        foreach (GameObject carbonDioxideParticle in carbonDioxideParticles)
        {
            carbonDioxideParticle.GetComponent<ParticleSystem>().maxParticles = Mathf.RoundToInt(value * 6.7f);
        }

    }

    public void SetTemperature(float value)
    {
        camLight.GetComponent<Light>().color = ColorFromTemperature(Mathf.Abs(value));
        currTemperatureFactor = value * 8 / 7400 + 13.02f;
        updateWaterParticle();
    }

    public void SetHumidity(float value)
    {
        if(value >= 0.7)
        {
            waterTrail.SetActive(true);
        }

        if (value < 0.7)
        {
            waterTrail.SetActive(false);
        }

        if (value >= 0.35)
        {
            waterDrops.SetActive(true);
        }

        if (value < 0.35)
        {
            waterDrops.SetActive(false);
            waterTrail.SetActive(false);
        }

        if(value >= 0.7 && value < 0.8)
        {
            waterTrail.GetComponent<ParticleSystem>().emissionRate = (value * 1f);
        }else if (value >= 0.8 && value < 0.9)
        {
            waterTrail.GetComponent<ParticleSystem>().emissionRate = (value * 3f);
        }else if (value >= 0.9 && value < 1)
        {
            waterTrail.GetComponent<ParticleSystem>().emissionRate = (value * 5.5f);
        }

        waterDrops.GetComponent<ParticleSystem>().emissionRate = value * 100f;

        currHumidityFactor = value * 14.2857f - 4.2857f;
        updateWaterParticle();
    }


    public void updateWaterParticle()
    {
        GameObject[] waterParticles = GameObject.FindGameObjectsWithTag("WaterParticle");

        foreach (GameObject waterParticle in waterParticles)
        {
            float particleNumber = currTemperatureFactor - currHumidityFactor / 2;
            waterParticle.GetComponent<ParticleSystem>().maxParticles = particleNumber > 2f ? Mathf.RoundToInt(particleNumber) : 2;
        }
    }
    public static Color ColorFromTemperature(float temperature)
    {
        temperature /= 100f;

        var red = 255f;
        var green = 255f;
        var blue = 255f;

        if (temperature >= 66f)
        {
            red = temperature - 60f;
            red = 329.698727446f * Mathf.Pow(red, -0.1332047592f);
        }

        if (temperature < 66f)
        {
            green = temperature;
            green = 99.4708025861f * Mathf.Log(green) - 161.1195681661f;
        }
        else
        {
            green = temperature - 60f;
            green = 288.1221695283f * Mathf.Pow(green, -0.0755148492f);
        }

        if (temperature <= 19f)
        {
            blue = 0f;
        }
        else if (temperature <= 66f)
        {
            blue = temperature - 10f;
            blue = 138.5177312231f * Mathf.Log(blue) - 305.0447927307f;
        }

        red /= 255f;
        green /= 255f;
        blue /= 255f;

        return new Color(red, green, blue);
    }

    public void setOxygenParticles(Boolean checkbox)
    {

        GameObject[] oxygenParticles = GameObject.FindGameObjectsWithTag("OxygenParticle");

        foreach (GameObject oxygenParticle in oxygenParticles)
        {
            oxygenParticle.gameObject.GetComponent<ParticleSystem>().enableEmission = checkbox;
        }

        GameObject[] oxygenParticleBugs = GameObject.FindGameObjectsWithTag("OxygenParticleBugs");

        foreach (GameObject oxygenParticleBug in oxygenParticleBugs)
        {
            oxygenParticleBug.gameObject.GetComponent<ParticleSystem>().enableEmission = checkbox;
        }
    }

    public void setCarbonDioxideParticles(Boolean checkbox)
    {

        GameObject[] carbonDioxideParticles = GameObject.FindGameObjectsWithTag("CarbonDioxideParticle");

        foreach (GameObject carbonDioxideParticle in carbonDioxideParticles)
        {
            carbonDioxideParticle.gameObject.GetComponent<ParticleSystem>().enableEmission = checkbox;
        }

        GameObject[] carbonDioxideParticleBugs = GameObject.FindGameObjectsWithTag("CarbonDioxideParticleBug");

        foreach (GameObject carbonDioxideParticleBug in carbonDioxideParticleBugs)
        {
            carbonDioxideParticleBug.gameObject.GetComponent<ParticleSystem>().enableEmission = checkbox;
        }
    }

    public void setWaterParticles(Boolean checkbox)
    {

        GameObject[] waterParticles = GameObject.FindGameObjectsWithTag("WaterParticle");

        foreach (GameObject waterParticle in waterParticles)
        {
            waterParticle.gameObject.GetComponent<ParticleSystem>().enableEmission = checkbox;
        }
    }


    public void SetProcess(int index)
    {

        if (index == 0)
        {
            GameObject.Find("Option1Checkbox").GetComponentInChildren<UnityEngine.UI.Text>().text = "Evapotranspiração";
            GameObject.Find("Option2Checkbox").GetComponentInChildren<UnityEngine.UI.Text>().text = "Precipitação";
        }
        else if (index == 1 || index == 2)
        {
            GameObject.Find("Option1Checkbox").GetComponentInChildren<UnityEngine.UI.Text>().text = "Respiração";
            GameObject.Find("Option2Checkbox").GetComponentInChildren<UnityEngine.UI.Text>().text = "Fotosíntese";
        }
    }
}
