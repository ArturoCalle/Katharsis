using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotaUIMenu : MonoBehaviour
{
    public Recolectable recolectable;
    public Text texto;
    public Image imagen;
    public Text aviso;
    public PanelNotas panelNotas;
    // Start is called before the first frame update
    void Start()
    {
        if(recolectable == null)
        {
            recolectable = new Recolectable();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PanelNotas.instance.mostrandoNota)
        {
            mostrarNota();
            if(Input.GetKeyDown(KeyCode.Z))
            {
                panelNotas.mostrandoNota = false;
                panelNotas.setLock(false);
                ocultarNota();
            }
        }
    }
    public void mostrarNota()
    {
        texto.text = recolectable.getNombre();

        gameObject.GetComponent<Image>().enabled= true;
        texto.enabled = true;
        aviso.enabled = true;
    }
    public void ocultarNota()
    {
        gameObject.GetComponent<Image>().enabled = false;
        texto.enabled = false;
        aviso.enabled = false;
        gameObject.SetActive(false);

    }
    public void actualizarNota(Recolectable r)
    {
        recolectable = r;
        texto.text = recolectable.getNombre();
    }

}
