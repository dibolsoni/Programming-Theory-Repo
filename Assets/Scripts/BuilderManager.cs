using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuilderManager : MonoBehaviour
{
    public static BuilderManager Instance;
    // ENCAPSULATION
    public Node node { get; private set; }
    public Tower basicTowerPrefab;
    public Tower fireTowerPrefab;
    public Tower coldTowerPrefab;
    private Tower towerToBuild;
    public GameObject BuilderUI;


    // ENCAPSULATION
    public bool hasNode
    {
        get
        {
            return node != null;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SelectNode(Node node)
    {
        BuilderUI.SetActive(false);
        this.node = node;
        if (node.tower == null)
            BuilderUI.SetActive(true);
    }

    public void SetBasicTower()
    {
        towerToBuild = basicTowerPrefab;
    }

    public void SetFireTower()
    {
        towerToBuild = fireTowerPrefab;
    }

    public void SetColdTower()
    {
        towerToBuild = coldTowerPrefab;
    }

    public bool CanBuild()
    {
        return towerToBuild is not null && towerToBuild.cost <= GameManager.Instance.playerState.goldAmount;
    }


    // ENCAPSULATION
    public void OnBuild()
    {
        if (!CanBuild())
        {
            NotificationManager.Instance.ShowNotification("Not enough gold");
            return;
        }
        node.BuildTower(towerToBuild);
        Close();
    }


    public void Close()
    {
        BuilderUI.SetActive(false);
        towerToBuild = null;
        node.Reset();
        node = null;
    }


}
