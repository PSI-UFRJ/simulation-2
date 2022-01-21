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

    [SerializeField] private GameObject layer1;
    [SerializeField] private GameObject layer2;
    [SerializeField] private GameObject layer3;
    [SerializeField] private GameObject layer4;


    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaterial(int layer, int matIndex)
    {
        if (layer == 1)
        {
            if ((matIndex > gravel.Length - 1) || (matIndex < 0))
            {
                return;
            }

            Debug.Log(layer1.ToString());

            layer1.GetComponentInChildren<MeshRenderer>().material = gravel[matIndex];
        }
        else if(layer == 2)
        {
            if ((matIndex > filterLayer.Length - 1) || (matIndex < 0))
            {
                return;
            }

            layer2.GetComponentInChildren<MeshRenderer>().material = filterLayer[matIndex];
        }
        else if(layer == 3)
        {
            if ((matIndex > soil.Length - 1) || (matIndex < 0))
            {
                return;
            }

            layer3.GetComponentInChildren<MeshRenderer>().material = soil[matIndex];
        }
        else if(layer == 4)
        {
            if ((matIndex > grass.Length - 1) || (matIndex < 0))
            {
                return;
            }

            layer4.GetComponentInChildren<MeshRenderer>().material = grass[matIndex];
        }
    }
}
