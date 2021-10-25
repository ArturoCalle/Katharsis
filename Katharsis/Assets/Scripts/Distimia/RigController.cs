using UnityEngine;
using UnityEngine.Animations.Rigging;
public class RigController : MonoBehaviour
{
    public Rig rigMirada;
    public Rig rigLH;
    public Rig rigRH;
    private float speed = 1f;
    public static RigController instance;

    private void Start()
    {
        instance = this;
    }
    public void Mirar(Transform trompi)
    {
        rigMirada.gameObject.transform.GetChild(0).transform.position = trompi.position;
        rigMirada.weight = Mathf.MoveTowards(rigMirada.weight, 1, speed * Time.deltaTime);
        SetTarget(trompi);
    }
    public void DejarDeMirar()
    {
        rigMirada.weight = Mathf.MoveTowards(rigMirada.weight, 0, speed * Time.deltaTime);
    }
    private void SetTarget(Transform trompi)
    {
        rigLH.gameObject.transform.GetChild(0).transform.GetChild(0).transform.position = trompi.position;
        rigRH.gameObject.transform.GetChild(0).transform.GetChild(0).transform.position = trompi.position;
    }
}
