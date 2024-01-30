using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonGuardado : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("ButtonGuardado").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    private void OnButtonClick()
    {
        GameObject.Find("PanelGuardado").GetComponent<UnityEngine.UI.Image>().enabled = !GameObject.Find("PanelGuardado").GetComponent<UnityEngine.UI.Image>().enabled;
        GameObject.Find("Mensajesss").GetComponent<UnityEngine.UI.Text>().enabled = !GameObject.Find("Mensajesss").GetComponent<UnityEngine.UI.Text>().enabled;
    }
}
