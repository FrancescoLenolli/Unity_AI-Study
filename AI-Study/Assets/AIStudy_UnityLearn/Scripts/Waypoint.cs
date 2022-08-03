using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Waypoint : MonoBehaviour
{
    private MeshRenderer textRenderer;
    private MeshRenderer meshRenderer;
    private bool named = false;

    private void Start ()
    {
        if(!name.StartsWith("WayPoint")) return;
        RenameWPs(null);
    }

    private void OnEnable()
    {
        if (named) return;
        RenameWPs(null);
    }

    private void OnDestroy()
    {
        RenameWPs(gameObject);
    }

    [ContextMenu("Set Visibility")]
    public void SetVisibility()
    {
        if (!meshRenderer)
            meshRenderer = GetComponent<MeshRenderer>();
        if (!textRenderer)
            textRenderer = GetComponentInChildren<MeshRenderer>();

        textRenderer.enabled = !meshRenderer.isVisible;
        meshRenderer.enabled = !meshRenderer.isVisible;
    }

    private void RenameWPs(GameObject overlook)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("wp");
        var gosAlt = FindObjectsOfType<Waypoint>();
        int i = 1;
        foreach (GameObject go in gos)  
        { 
            if(go != overlook)
            {
                go.name = "WP" + string.Format("{0:000}",i);
                go.GetComponentInChildren<TextMesh>().text = go.name;
                i++; 
            } 
        }

        named = true;
    }
}
