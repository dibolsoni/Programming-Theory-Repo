using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Color startColor;
    private Color hightLightColor;
    private Renderer rend;
    private bool isHightLighted = false;
    private Tower tower;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        hightLightColor = Color.green;
    }

    private void OnMouseDown()
    {
        if (BuilderManager.Instance.isBuilderUIActive)
            return;
        // instance of the tower prefab
        Highlight();
        BuilderManager.Instance.SelectNode(this);
    }

    private void OnMouseEnter()
    {
        // change the color of the node
        if (!isHightLighted)
            rend.material.color = Color.Lerp(startColor, hightLightColor, 0.5f);
    }

    private void OnMouseExit()
    {
        // change the color of the node 
        if (!isHightLighted)
            Reset();
    }

    private void Highlight()
    {
        isHightLighted = true;
        rend.material.color = hightLightColor;
    }

    public void Reset()
    {
        rend.material.color = startColor;
        isHightLighted = false;
    }

    public void BuildTower(Tower tower)
    {
        if (this.tower != null)
        {
            NotificationManager.Instance.ShowNotification("Can't build here");
            return;
        }
        this.tower = Instantiate(tower, transform.position, Quaternion.identity);
        GameManager.Instance.changeGold(-tower.cost);
        Reset();
    }
}
