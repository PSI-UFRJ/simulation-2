using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrariumController : MonoBehaviour
{

    [SerializeField] private Material[] grass;
    [SerializeField] private Material[] soil;
    [SerializeField] private Material[] filterLayer;
    [SerializeField] private Material[] gravel;

    private Material currGrass;
    private Material currSoil;
    private Material currFilterLayer;
    private Material currGravel;

    private List<GameObject> layers;
    [SerializeField] private GameObject layer1;
    [SerializeField] private GameObject layer2;
    [SerializeField] private GameObject layer3;
    [SerializeField] private GameObject layer4;

    public const int LAYER1 = 1;
    public const int LAYER2 = 2;
    public const int LAYER3 = 3;
    public const int LAYER4 = 4;
    public const int LAYER5 = 5;
    public const int LAYER6 = 6;
    public const int LAYER7 = 7;
    public const int LAYER8 = 8;



    // Start is called before the first frame update
    void Start()
    {
        layers = new List<GameObject>() { layer1, layer2, layer3, layer4 };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableLayer(int layer)
    {
        if ((layer < LAYER1) || (layer > LAYER8))
        {
            return;
        }

        layers[layer - 1].SetActive(true);
    }

    public bool IsLayerEnable(int layer)
    {
        if ((layer >= LAYER1) && (layer <= LAYER8))
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
    }
}
