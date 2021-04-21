
using UnityEngine;
using UnityEngine.UI;

public class StoreButton : MonoBehaviour
{
    public StoreButton SB;
    public GameObject panel;
    public Button stb;
    void Start()
    {
        stb = FindObjectOfType<Button>();
    }
    public void OpenPanel()
    {
        if (panel != null)
        {
            bool isActive = panel.activeSelf;
            panel.SetActive(!isActive);
        }
    }

   public void DisabledButton()
   {
       if(panel == true && stb.gameObject.name == "StoreOpen")
       {
           stb.interactable = false;
           stb.enabled = false;
       }
   }
}
