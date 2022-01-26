using UnityEngine;

public class SupplyCrate : MonoBehaviour
{
    public GameObject Contents;
    public GameObject UI;

    private void Awake()
    {
        UI.SetActive(false);
    }
}
