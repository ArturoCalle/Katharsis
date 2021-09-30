using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneIAController : MonoBehaviour
{
    public GameObject prefabDistimia;

    private GameObject distimia = null;
    public List<GameObject> trigger;

    public static SceneIAController instance;

    public List<GameObject> targets;

    private void Start()
    {
        instance = this;
    }

    public void instanciarDistimia(Transform posicion)
    {
        prefabDistimia.transform.position = posicion.position;
        distimia = Instantiate(prefabDistimia);
    }

    public void destroyDistimia()
    {
        Destroy(distimia);
    }

}
