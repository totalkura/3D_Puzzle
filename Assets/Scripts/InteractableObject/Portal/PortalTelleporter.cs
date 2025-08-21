using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTelleporter : MonoBehaviour
{
    public PortalController potal;

    private void OnTriggerEnter(Collider other)
    {
        potal.Teleport();
    }


}
