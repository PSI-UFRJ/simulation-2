using System;
using System.Linq;
using UnityEngine;

public class RotateObjectController : MonoBehaviour
{
    public float PCRotationSpeed = 10f;
    public float MobileRotationSpeed = 0.4f;
    //Drag the camera object here
    public Camera cam;
    private Collider terrariumCollider;

    private bool canMoveRightBtn; // Guarda a informação se pode mover o objeto - botão direito do mouse
    private bool draggingRightBtn; // Guarda a informação se o objeto está sendo arrastado - botão direito do mouse

    private bool canMoveLeftBtn; // Guarda a informação se pode mover o objeto - botão esquerdo do mouse
    private bool draggingLeftBtn; // Guarda a informação se o objeto está sendo arrastado - botão esquerdo do mouse

    // Item1: offset do eixo x; Item2: offset do eixo y
    private Tuple<float, float> shapeOffset; //Guarda o cálculo do offset a fim de permitir que o movimento das formas acompanhe de forma mais precisa o mouse

    [SerializeField]
    private LayerMask clickableLayer;

    private void Start()
    {
        terrariumCollider = this.gameObject.GetComponent<Collider>();
    }

    void Update ()
    {

        // Get right button
        OnRightButtonMouseDrag();

        // Get left button
        OnLeftButtonMouseDrag();

        // get the user touch input
        foreach (Touch touch in Input.touches) {
            Debug.Log("Touching at: " + touch.position);
            Ray camRay = cam.ScreenPointToRay (touch.position);
            RaycastHit raycastHit;
            if(Physics.Raycast (camRay, out raycastHit, 10))
            {
                if (touch.phase == TouchPhase.Began) {
                    Debug.Log("Touch phase began at: " + touch.position);
                } else if (touch.phase == TouchPhase.Moved) {
                    Debug.Log("Touch phase Moved");
                    transform.Rotate (touch.deltaPosition.y * MobileRotationSpeed, 
                        -touch.deltaPosition.x * MobileRotationSpeed, 0, Space.World);
                } else if (touch.phase == TouchPhase.Ended) {
                    Debug.Log("Touch phase Ended");    
                }    
            }
        }
    }

    private void ExecuteMouseRightButtonDownActions()
    {

        // Sanity check
        if (!Input.GetMouseButtonDown(1))
        {
            return;
        }

        RaycastHit rayhit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayhit, Mathf.Infinity, clickableLayer))
        {
            if (rayhit.collider == terrariumCollider)
            {
                #region DragAndDrop
                canMoveRightBtn = true;
                #endregion
            }
            else
            {
                canMoveRightBtn = false;
            }
        }

        if (canMoveRightBtn)
        {
            draggingRightBtn = true;
        }
    }

    private void ExecuteMouseRightButtonUpActions()
    {
        // Sanity check
        if (!Input.GetMouseButtonUp(1))
        {
            return;
        }

        canMoveRightBtn = false;
        draggingRightBtn = false;
    }

    private void OnRightButtonMouseDrag()
    {
        ExecuteMouseRightButtonDownActions();
        if (draggingRightBtn) //Responsável pela a rotação
        {
            float rotX = Input.GetAxis("Mouse X") * PCRotationSpeed;
            float rotY = Input.GetAxis("Mouse Y") * PCRotationSpeed;

            Vector3 right = Vector3.Cross(cam.transform.up, transform.position - cam.transform.position);
            Vector3 up = Vector3.Cross(transform.position - cam.transform.position, right);

            transform.rotation = Quaternion.AngleAxis(-rotX, up) * transform.rotation;
            transform.rotation = Quaternion.AngleAxis(rotY, right) * transform.rotation;
        }
        ExecuteMouseRightButtonUpActions();
    }


    private void ExecuteMouseLeftButtonDownActions()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Sanity check
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        RaycastHit rayhit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayhit, Mathf.Infinity, clickableLayer))
        {
            if (rayhit.collider == terrariumCollider)
            {

                #region DragAndDrop
                canMoveLeftBtn = true;
                #endregion
            }
            else
            {
                canMoveLeftBtn = false;
            }
        }

        if (canMoveLeftBtn)
        {
            // Salva o offset do eixo x e do eixo y
            shapeOffset = new Tuple<float, float>(mousePos.x - this.transform.position.x, mousePos.y - this.transform.position.y);
            draggingLeftBtn = true;
        }
    }

    private void ExecuteMouseLeftButtonUpActions()
    {
        // Sanity check
        if (!Input.GetMouseButtonUp(0))
        {
            return;
        }

        canMoveLeftBtn = false;
        draggingLeftBtn = false;
    }

    private void OnLeftButtonMouseDrag()
    {
        ExecuteMouseLeftButtonDownActions();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (draggingLeftBtn) //Responsável pela a rotação
        {
            // Desloca o objeto de forma suave baseado no offset calculado
            this.transform.position = new Vector3(mousePos.x - shapeOffset.Item1, mousePos.y - shapeOffset.Item2, this.transform.position.z);
        }
        ExecuteMouseLeftButtonUpActions();
    }
}
