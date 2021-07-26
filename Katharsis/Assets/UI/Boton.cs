using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boton : MonoBehaviour
{
    public Text texto;

    Boton instance;
    string textoEnDisplay;
    bool prendido;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        prendido = false;
        textoEnDisplay = " ";
    }

    // Update is called once per frame
    void Update()
    {
        texto.text = textoEnDisplay;
        texto.GetComponent<Outline>().enabled = prendido;
    }
    public Boton getBoton()
    {
        return instance;
    }
    public void actualizarTexto(string nuevoTxt)
    {
        textoEnDisplay = nuevoTxt;
    }
    public void setActive(bool on_off)
    {
        prendido = on_off;
    }
    public string getTexto()
    {
        return textoEnDisplay;
    }
}
