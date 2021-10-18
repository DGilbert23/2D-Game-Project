using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void ToggleObject()
    {
        this.gameObject.SetActive(!this.gameObject.activeInHierarchy);
    }
}
