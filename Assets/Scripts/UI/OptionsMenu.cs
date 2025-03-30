using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [Header ("Referencias")]
    public Slider sliderVolume;
    public Slider sliderBright;
    public Image bright;
    public Toggle toggle;
    public TMP_Dropdown resolutionDropDown;
    Resolution[] resolutions;//Creamos un array para contener las resoluciones

    [Header ("Valores de las Barras")]
    public float sliderBrightValue;
    public float sliderVolumeValue;
    
    

    void Start()
    {
        sliderBright.value = PlayerPrefs.GetFloat("bright", 0.5f); //Iniciamos la primera vez con unos valores predeterminados,
                                                                   //luego utilizaremos la preferencia

        bright.color = new Color(bright.color.r, bright.color.g, bright.color.b, sliderBright.value); //cambia la transparencia (4 valor) segun la barra

        sliderVolume.value = PlayerPrefs.GetFloat("volumeAudio", 0.5f);
        AudioListener.volume = sliderVolume.value;

        //Comprobamos si la casilla está marcada o no para activar la pantalla completa
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        CheckResolution();
    }

    public void ChangeVolumeSlider(float value)
    {
        sliderVolumeValue = value;
        PlayerPrefs.SetFloat("volumeAudio", sliderVolumeValue); //Guardamos la preferencia
        AudioListener.volume = sliderVolume.value;
    }

    public void ChangeBrightSlider(float value)
    {
        sliderBrightValue = value;
        PlayerPrefs.SetFloat("bright", sliderBright.value); //Guardamos la preferencia
        bright.color = new Color(bright.color.r, bright.color.g, bright.color.b, sliderBright.value);
    }

    public void ActivateFullScreen(bool fullScreen) //Activa pantalla completa si el valor es verdadero
    {
        Screen.fullScreen = fullScreen;
    }

    void CheckResolution()
    /*
     Aquí comprobaremos las resoluciones que permite la pantalla, las añadiremos a la lista y
     comprobaremos la resolucion actual, pero no realizaremos el cambio de resolucion
    */
    {
        resolutions = Screen.resolutions; //Las resoluciones serán las que tenga la pantalla
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>(); //Creamos una lista
        int currentResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option); //Añadimos y mostramos las resoluciones que tiene la pantalla (ancho x alto)

            if( resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i; //Como estamos recorriendo la lista con el bucle, cuando la resolucion de la pantalla sea la misma que la opcion
                                       // la fijara como la actual
            }
        }
        //Añadimos las opciones a la lista vertical y fijamos la resolucion actual
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolution;
        resolutionDropDown.RefreshShownValue();
    }

    public void ChangeResolution(int indexResolution) //Cambiamos la resolucion
    {
        Resolution resolution = resolutions[indexResolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
