using UnityEngine;
using UnityEngine.UI;

public class OptionSetting : MonoBehaviour
{
    public Image bright;
    public float brightValue;

    void Start()
    {
        brightValue = GetComponent<OptionsMenu>().sliderBright.value;
        bright.color = new Color(bright.color.r, bright.color.g, bright.color.b, brightValue);

    }

}
