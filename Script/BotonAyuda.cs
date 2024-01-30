using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonAyuda : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("ButtonAyuda").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    private void OnButtonClick()
    {
        GameObject.Find("PanelRecomendaciones").GetComponent<UnityEngine.UI.Image>().enabled = !GameObject.Find("PanelRecomendaciones").GetComponent<UnityEngine.UI.Image>().enabled;
        GameObject.Find("Mensajess").GetComponent<UnityEngine.UI.Text>().enabled = !GameObject.Find("Mensajess").GetComponent<UnityEngine.UI.Text>().enabled;
        GameObject.Find("Camara").GetComponent<UnityEngine.UI.Text>().enabled = !GameObject.Find("Camara").GetComponent<UnityEngine.UI.Text>().enabled;
    }
}
