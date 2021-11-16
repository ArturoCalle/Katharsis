using UnityEngine;
using UnityEngine.Animations.Rigging;
public class RigController : MonoBehaviour
{
    public Rig rigMirada; //controlador hueso de cabeza
    public Rig rigLH; //controlador hueso de mano izquierda
    public Rig rigRH; //controlador hueso de mano derecha
    private float speed = 1f; //velocidad de transici�n entre la animaci�n corriente y la mezcla de animaci�n con el hueso
    public static RigController instance;

    private void Start()
    {
        instance = this;
    }
    /**
     * Activa el hueso de cabeza apuntando a la posici�n de trompi recibida como par�metro
     */
    public void Mirar(Transform trompi)
    {
        rigMirada.gameObject.transform.GetChild(0).transform.position = trompi.position;
        rigMirada.weight = Mathf.MoveTowards(rigMirada.weight, 1, speed * Time.deltaTime);
        SetTarget(trompi);
    }
    /**
     * Desactiva el hueso de la cabeza
     */
    public void DejarDeMirar()
    {
        rigMirada.weight = Mathf.MoveTowards(rigMirada.weight, 0, speed * Time.deltaTime);
    }
    /**
     * Las animaciones de Distimia estan editadas para activar y desactivar los huesos, el target debe asignarse aca
     * para que la animaci�n se dirija a trompi.
     */
    private void SetTarget(Transform trompi)
    {
        rigLH.gameObject.transform.GetChild(0).transform.GetChild(0).transform.position = trompi.position;
        rigRH.gameObject.transform.GetChild(0).transform.GetChild(0).transform.position = trompi.position;
    }
}
