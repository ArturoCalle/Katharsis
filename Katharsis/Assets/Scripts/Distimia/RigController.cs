using UnityEngine;
using UnityEngine.Animations.Rigging;
public class RigController : MonoBehaviour
{
    public Rig rig;
    private float lookSpeed = 1f;
    public static RigController instance;

    private void Start()
    {
        instance = this;

    }

    public void setRigWeightToOne(Transform trompi)
    {
        gameObject.transform.GetChild(0).transform.position = trompi.position;
        rig.weight = Mathf.MoveTowards(rig.weight, 1, lookSpeed * Time.deltaTime);
    }
    public void setRigWeightToZero()
    {
        rig.weight = Mathf.MoveTowards(rig.weight, 0, lookSpeed * Time.deltaTime);
    }
}
