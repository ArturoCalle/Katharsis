using UnityEngine;
using UnityEngine.Animations.Rigging;
public class RigController : MonoBehaviour
{
    public Rig rigMirada;
    public Rig rigPuño;
    private float speed = 1f;
    public static RigController instance;

    private void Start()
    {
        instance = this;
    }

    public void Mirar(Transform trompi)
    {
        gameObject.transform.GetChild(0).transform.position = trompi.position;
        rigMirada.weight = Mathf.MoveTowards(rigMirada.weight, 1, speed * Time.deltaTime);
    }
    public void DejarDeMirar()
    {
        rigMirada.weight = Mathf.MoveTowards(rigMirada.weight, 0, speed * Time.deltaTime);
    }
    public void PegarPuño(Transform trompi)
    {
        gameObject.transform.GetChild(0).transform.position = trompi.position;
        rigMirada.weight = Mathf.MoveTowards(rigMirada.weight, 1, speed * Time.deltaTime);
    }
    public void DejarDePegarPuñor()
    {
        rigMirada.weight = Mathf.MoveTowards(rigMirada.weight, 0, speed * Time.deltaTime);
    }
}
