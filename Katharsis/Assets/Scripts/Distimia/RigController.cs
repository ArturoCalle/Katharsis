using UnityEngine;
using UnityEngine.Animations.Rigging;
public class RigController : MonoBehaviour
{
    public Rig rigMirada;
    public Rig rigPuño;
    public Rig rigPatada;
    private float lookSpeed = 1f;
    private float punchSpeed = 1f;
    private float kickSpeed = 1f;
    public static RigController instance;

    private void Start()
    {
        instance = this;
    }

    public void Mirar(Transform trompi)
    {
        gameObject.transform.GetChild(0).transform.position = trompi.position + new Vector3(0, 4f, 0);
        rigMirada.weight = Mathf.MoveTowards(rigMirada.weight, 1, lookSpeed * Time.deltaTime);
    }
    public void DejarDeMirar()
    {
        rigMirada.weight = Mathf.MoveTowards(rigMirada.weight, 0, lookSpeed * Time.deltaTime);
    }
    public void Patear(Transform trompi)
    {
        gameObject.transform.GetChild(0).transform.position = trompi.position;
        rigMirada.weight = Mathf.MoveTowards(rigMirada.weight, 1, kickSpeed * Time.deltaTime);
    }
    public void DejarDePatear()
    {
        rigMirada.weight = Mathf.MoveTowards(rigMirada.weight, 0, kickSpeed * Time.deltaTime);
    }
    public void PegarPuño(Transform trompi)
    {
        gameObject.transform.GetChild(0).transform.position = trompi.position;
        rigMirada.weight = Mathf.MoveTowards(rigMirada.weight, 1, punchSpeed * Time.deltaTime);
    }
    public void DejarDePegarPuñor()
    {
        rigMirada.weight = Mathf.MoveTowards(rigMirada.weight, 0, punchSpeed * Time.deltaTime);
    }
}
