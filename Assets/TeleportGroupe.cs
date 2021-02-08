using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGroupe : MonoBehaviour
{
    public MeshRenderer[] meshRenderer;
    public Color color;

    void Start()
    {
        for (int i = 0; i < meshRenderer.Length; i++)
        {
            meshRenderer[i].materials[1].color = color;
        }
    }

   
    
}
