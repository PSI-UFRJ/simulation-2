using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrariumController : MonoBehaviour
{

    [SerializeField] private Material[] grass;
    [SerializeField] private Material[] soil;
    [SerializeField] private Material[] filterLayer;
    [SerializeField] private Material[] gravel;

    [SerializeField] private GameObject[] vegetationLayer1;
    [SerializeField] private GameObject[] vegetationLayer2;
    [SerializeField] private GameObject[] vegetationLayer3;
    [SerializeField] private GameObject[] bugLayer1;
    [SerializeField] private GameObject[] bugLayer2;

    private Material currGrass;
    private Material currSoil;
    private Material currFilterLayer;
    private Material currGravel;
    private GameObject currVegetation1;
    private GameObject currVegetation2;
    private GameObject currVegetation3;
    private GameObject currBug1;
    private GameObject currBug2;

    private List<GameObject> layers;
    [SerializeField] private GameObject layer1;
    [SerializeField] private GameObject layer2;
    [SerializeField] private GameObject layer3;
    [SerializeField] private GameObject layer4;
    [SerializeField] private GameObject layer5;
    [SerializeField] private GameObject layer6;
    [SerializeField] private GameObject layer7;
    [SerializeField] private GameObject layer8;
    [SerializeField] private GameObject layer9;


    [SerializeField] private GameObject lid;

    public const int LAYER1 = 1;
    public const int LAYER2 = 2;
    public const int LAYER3 = 3;
    public const int LAYER4 = 4;
    public const int LAYER5 = 5;
    public const int LAYER6 = 6;
    public const int LAYER7 = 7;
    public const int LAYER8 = 8;
    public const int LAYER9 = 9;



    // Start is called before the first frame update
    void Start()
    {
        layers = new List<GameObject>() { layer1, layer2, layer3, layer4, layer5, layer6, layer7, layer8, layer9 };
    }

    public void CloseBowl()
    {
        if(lid == null)
        {
            return;
        }

        lid.SetActive(true);
        lid.GetComponent<Animation>().Play();

        this.gameObject.GetComponent<Transform>().Find("WaterDropsParticle").gameObject.SetActive(true);
        this.gameObject.GetComponent<Transform>().Find("12985_Fish_bowl_v1_l1").Find("12985_fish_bowl_Castle").gameObject.SetActive(true);

        GameObject.Find("ControlPanel").GetComponent<PanelController>().setOxygenParticles(true);
        GameObject.Find("ControlPanel").GetComponent<PanelController>().setCarbonDioxideParticles(true);
        GameObject.Find("ControlPanel").GetComponent<PanelController>().setWaterParticles(true);
    }

    public void EnableLayer(int layer)
    {
        if ((layer < LAYER1) || (layer > LAYER9))
        {
            return;
        }

        layers[layer - 1].SetActive(true);
    }

    public bool IsLayerEnable(int layer)
    {
        if ((layer >= LAYER1) && (layer <= LAYER9))
        {
            return layers[layer - 1].activeSelf;
        }
        else
        {
            return false;
        }
    }

    public void SetMaterialInLayer(int layer, int matIndex)
    {
        if (layer == LAYER1)
        {
            if ((matIndex > gravel.Length - 1) || (matIndex < 0))
            {
                return;
            }

            layer1.GetComponentInChildren<MeshRenderer>().material = gravel[matIndex];
            currGravel = gravel[matIndex];
        }
        else if(layer == LAYER2)
        {
            if ((matIndex > filterLayer.Length - 1) || (matIndex < 0))
            {
                return;
            }

            layer2.GetComponentInChildren<MeshRenderer>().material = filterLayer[matIndex];
            currFilterLayer = filterLayer[matIndex];
        }
        else if(layer == LAYER3)
        {
            if ((matIndex > soil.Length - 1) || (matIndex < 0))
            {
                return;
            }

            layer3.GetComponentInChildren<MeshRenderer>().material = soil[matIndex];
            currSoil = soil[matIndex];
        }
        else if(layer == LAYER4)
        {
            if ((matIndex > grass.Length - 1) || (matIndex < 0))
            {
                return;
            }

            layer4.GetComponentInChildren<MeshRenderer>().material = grass[matIndex];
            currGrass = grass[matIndex];
        }
        else if (layer == LAYER5)
        {
            if ((matIndex > vegetationLayer1.Length - 1) || (matIndex < 0))
            {
                return;
            }

            if (currVegetation1 != null)
            {
                currVegetation1.SetActive(false);
            }

            vegetationLayer1[matIndex].SetActive(true);
            currVegetation1 = vegetationLayer1[matIndex];
        }
        else if (layer == LAYER6)
        {
            if ((matIndex > vegetationLayer2.Length - 1) || (matIndex < 0))
            {
                return;
            }

            if (currVegetation2 != null)
            {
                currVegetation2.SetActive(false);
            }

            vegetationLayer2[matIndex].SetActive(true);
            currVegetation2 = vegetationLayer2[matIndex];
        }
        else if (layer == LAYER7)
        {
            if ((matIndex > vegetationLayer3.Length - 1) || (matIndex < 0))
            {
                return;
            }

            if (currVegetation3 != null)
            {
                currVegetation3.SetActive(false);
            }

            vegetationLayer3[matIndex].SetActive(true);
            currVegetation3 = vegetationLayer3[matIndex];
        }
        else if (layer == LAYER8)
        {
            if ((matIndex > bugLayer1.Length - 1) || (matIndex < 0))
            {
                return;
            }

            if (currBug1 != null)
            {
                currBug1.SetActive(false);
            }

            bugLayer1[matIndex].SetActive(true);
            currBug1 = bugLayer1[matIndex];
        }
        else if (layer == LAYER9)
        {
            if ((matIndex > bugLayer2.Length - 1) || (matIndex < 0))
            {
                return;
            }

            if (currBug2 != null)
            {
                currBug2.SetActive(false);
            }

            bugLayer2[matIndex].SetActive(true);
            currBug2 = bugLayer2[matIndex];
        }
    }

}
