using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomObjectController : MonoBehaviour
{
    float defaultScale;
    float scaleMax;
    float scaleMin;
    float scrollSpeed;
    float scale;

    // Start is called before the first frame update
    void Start()
    {
        defaultScale = this.gameObject.transform.localScale.x;
        scaleMax     = 50 * this.gameObject.transform.localScale.x;
        scaleMin     = this.gameObject.transform.localScale.x / 10;
        scrollSpeed  = 5f;
        scale        = defaultScale;
    }

    // Update is called once per frame
    void Update()
    {
        scale += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        scale  = Mathf.Clamp(scale, scaleMin, scaleMax);

        this.transform.localScale = new Vector3(scale, scale, scale);
    }
}
