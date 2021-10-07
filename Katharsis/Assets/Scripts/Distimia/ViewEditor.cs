using UnityEditor;
using UnityEngine;
using UnityStandardAssets.Assets.ThirdPerson;

[CustomEditor(typeof(DistimiaAI))]
public class ViewEditor : Editor
{
    private void OnSceneGUI()
    {
        DistimiaAI fov = (DistimiaAI)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.cabeza.transform.position, Vector3.up, Vector3.forward, 360, fov.radioBusqueda);

        Vector3 viewAngle01 = DirectionFromAngle(fov.cabeza.transform.eulerAngles.y, -fov.anguloDeBusqueda / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.cabeza.transform.eulerAngles.y, fov.anguloDeBusqueda / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.cabeza.transform.position, fov.cabeza.transform.position + viewAngle01 * fov.radioBusqueda);
        Handles.DrawLine(fov.cabeza.transform.position, fov.cabeza.transform.position + viewAngle02 * fov.radioBusqueda);

        if (fov.puedeVerTrompi)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.cabeza.transform.position, fov.Jugador.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
