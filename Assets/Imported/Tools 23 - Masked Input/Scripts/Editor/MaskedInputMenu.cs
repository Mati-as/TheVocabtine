using UnityEngine;
using UnityEditor;


public class MaskedInputMenu
{
    [MenuItem("GameObject/Tools-23/MaskedInput")]
    public static void CreateMaskedInput()
    {
        GameObject canvasGO = GameObject.Find("Canvas");
        GameObject selectedGO = Selection.activeGameObject;
        if (selectedGO == null)
        {
            selectedGO = canvasGO;
        }
        if (canvasGO != null)
        {
            GameObject prefab = Resources.Load<GameObject>("Masked_Inputfield");
            if (prefab != null)
            {
                GameObject instance = GameObject.Instantiate(prefab, selectedGO.transform);
                instance.name = prefab.name;
            }
            else
            {
                Debug.LogError("Failed to load InputFieldPrefab from Resources folder.");
            }
        }
        else
        {
            Debug.LogError("Failed to find Canvas GameObject in scene.");
        }
    }
}
