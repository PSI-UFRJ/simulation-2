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
    private Dictionary<int, UnityEngine.UI.Button> selectedBtns;
    private Sprite selectedBtnNormal;
    private Dictionary<GameObject, int> panelLayerMapping;

    private bool isPopupOn;
    private GameObject currPopup;
    [SerializeField] GameObject missingChoicePopup;

    private const int CONSTRUCT = 1;
    private const int OBSERVE = 2;
    private int currMode;

    void Start()
    {
        currentIndex = 0;
        currMode = CONSTRUCT;
        terrariumController = GameObject.Find("Terrarium").GetComponent<TerrariumController>();
        panelLayerMapping = new Dictionary<GameObject, int>()
        {
            {materialPanels[0], TerrariumController.LAYER1},
            {materialPanels[1], TerrariumController.LAYER2},
            {materialPanels[2], TerrariumController.LAYER3},
            {materialPanels[3], TerrariumController.LAYER4}
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
            {TerrariumController.LAYER8, null}
        };

        isPopupOn = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CloseTerrarium()
    {
        if((currentIndex != TerrariumController.LAYER8 - 1) || (materialPanels == null) || (terrariumController == null))
        {
            return;
        }

        terrariumController.CloseBowl();
        materialPanels[currentIndex].SetActive(false);
        currMode = OBSERVE;
        //observingMode.SetActive(true);
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
        if ((materialPanels == null) || (currMode != CONSTRUCT))
        {
            return;
        }

        if (!WasOptionChosen())
        {
            CallMissingChoicePopup();
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
        if ((materialPanels == null) || (currMode != CONSTRUCT))
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
}
