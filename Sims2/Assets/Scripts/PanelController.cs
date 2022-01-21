using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private List<GameObject> materialPanels;
    private int currentIndex;

    private TerrariumController terrariumController;
    private UnityEngine.UI.Button selectedBtn;
    private Sprite selectedBtnNormal;
    private Dictionary<GameObject, int> panelLayerMapping;

    void Start()
    {
        currentIndex = 0;
        terrariumController = GameObject.Find("Terrarium").GetComponent<TerrariumController>();
        panelLayerMapping = new Dictionary<GameObject, int>()
        {
            {materialPanels[0], 1},
            {materialPanels[1], 2},
            {materialPanels[2], 3},
            {materialPanels[3], 4}

        };

        foreach(GameObject go in panelLayerMapping.Keys)
        {
            Debug.Log("" + panelLayerMapping[go]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangePanelUp()
    {
        if (materialPanels == null)
        {
            return;
        }

        if(currentIndex + 1 < materialPanels.Count)
        {
            materialPanels[currentIndex].SetActive(false);
            currentIndex++;
            materialPanels[currentIndex].SetActive(true);
        }


    }

    public void ChangePanelDown()
    {
        if (materialPanels == null)
        {
            return;
        }

        if(currentIndex - 1 >= 0)
        {
            materialPanels[currentIndex].SetActive(false);
            currentIndex--;
            materialPanels[currentIndex].SetActive(true);
        }

    }

    public void ChangeMaterial(int mat)
    {
        int layer = panelLayerMapping[materialPanels[currentIndex]];
        terrariumController.SetMaterial(layer, mat);
    }

    public void SelectButton(UnityEngine.UI.Button btn, string selectedBtnImg)
    {
        if(selectedBtn == null)
        {
            return;
        }

        selectedBtn.GetComponent<UnityEngine.UI.Image>().sprite = selectedBtnNormal;

        selectedBtn = btn;
        selectedBtnNormal = selectedBtn.GetComponent<UnityEngine.UI.Image>().sprite;
        selectedBtn.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load("UI Components/" + selectedBtnImg, typeof(Sprite)) as Sprite;

        
    }
}
