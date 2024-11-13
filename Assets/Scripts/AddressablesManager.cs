using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesManager : MonoBehaviour
{
    public AssetReference assetReference;
    AsyncOperationHandle<GameObject> handle;
    List<GameObject> prefabs = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            Debug.Log("Entrou");
            if (prefabs.Count <= 0) LoadAsset();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")){
            Debug.Log("Saiu");
            if (prefabs.Count > 0) UnloadAsset();
        }
    }

    private void LoadAsset(){
        handle = assetReference.InstantiateAsync(transform.position, Quaternion.identity);
        handle.Completed += handle => { prefabs.Add(handle.Result); };
    }

    private void UnloadAsset(){
        foreach (GameObject prefab in prefabs){
            Addressables.ReleaseInstance(prefab);
        }
        prefabs.Clear();
    }

}
