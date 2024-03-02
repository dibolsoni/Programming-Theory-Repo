using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuilderManager : MonoBehaviour
{
    public static BuilderManager Instance;
    private Node node;
    public Tower basicTowerPrefab;
    public Tower fireTowerPrefab;
    public Tower coldTowerPrefab;
    [SerializeField]
    private Tower towerToBuild;
    public GameObject BuilderUI;


    // ENCAPSULATION
    public bool isBuilderUIActive
    {
        get
        {
            return node is not null;
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
        BuilderUI.SetActive(true);
        this.node = node;
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


    // ENCAPSULATION
    public void OnBuild()
    {
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
