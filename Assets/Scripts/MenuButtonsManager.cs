using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonsManager : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject toiletPrefab;
    [SerializeField] private GameObject bottlePrefab;

    [SerializeField] private Transform particlesParent;
    [SerializeField] private Transform contentParent;
    [SerializeField] private string dontDestroyTag;
    
    public void ReloadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void TurnOnToilet()
    {
        ClearScene();
        Instantiate(toiletPrefab, new Vector3(0,-0.36f,0), Quaternion.identity, contentParent);
    }
    public void TurnOnBottle()
    {
        ClearScene();
        Instantiate(bottlePrefab, new Vector3(0,0,0), Quaternion.identity, contentParent);
    }

    private void ClearScene()
    {
        var particlesParentChildren = particlesParent.GetComponentsInChildren<Transform>();
        foreach (var child in particlesParentChildren)
        {
            if(!child.CompareTag(dontDestroyTag))
                Destroy(child.gameObject);
        }
            
        var contentParentChildren = contentParent.GetComponentsInChildren<Transform>();
        foreach (var child in contentParentChildren)
            if(!child.CompareTag(dontDestroyTag))
                Destroy(child.gameObject);
        
    }
    
}
