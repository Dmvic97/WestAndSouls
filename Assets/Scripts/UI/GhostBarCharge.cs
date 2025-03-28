using UnityEngine;
using UnityEngine.UI;

public class GhostBarCharge : MonoBehaviour
{
    
    public Image resourceBar;
    public GhostMode ghostMode;

    void Update()
    {
        if (ghostMode != null && resourceBar != null)
        {
            resourceBar.fillAmount = ghostMode.currentGhostModeDuration / ghostMode.maxGhostModeDuration;
        }
    }
}
