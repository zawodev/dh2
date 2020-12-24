using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WeatherController : MonoBehaviour {

    public static WeatherController weatherController;
    public Light2D _light;
    public Color startColor = Color.white;
    Color presentColor;

    [Range(1f, 100f)]
    [Tooltip("fast fade -> slow fade")]
    public float smooth = 30f;
    float _smooth;

    //=================

    [Header("=== WEATHER ===")]
    public bool weatherCycleEnabled;
    public enum StaticWeather { none = 0, sunny = 1, cloudy = 2, rainy = 3, snowy = 4 }
    public StaticWeather staticWeather;

    public WeatherTime[] weatherTime;

    //==================

    [Header("=== DAY TIME ===")]

    public bool timeCycleEnabled;
    public DayTime[] dayTime;


    //==================

    int i = 0;
    float timer;
    private void Awake() {
        if (weatherController != null) {
            weatherController.timeCycleEnabled = timeCycleEnabled;
            Destroy(gameObject);
            return;
        }
        else weatherController = this;
        //DontDestroyOnLoad(gameObject);

        if (timeCycleEnabled) {
            presentColor = dayTime[0].color;
            timer = dayTime[0].timeLength;
        }
        else {
            _light.color = presentColor = startColor;
        }
    }
    private void Update() {

        if (weatherCycleEnabled) { // WEATHER CYCLE ENABLED
            if (staticWeather == StaticWeather.none) {

            }
            else {
                SwitchParticles((int)staticWeather - 1);
                weatherCycleEnabled = false;
            }
        }

        // ======================================================

        if (timeCycleEnabled) { // TIME CYCLE ENABLED
            timer -= Time.deltaTime;

            presentColor = Color.Lerp(presentColor, dayTime[i].color, 1f / (_smooth * smooth));
            _light.color = presentColor; //??

            if (timer < 0) {
                //timeDist = dayTime[i].timeLength;
                i = (i + 1) % dayTime.Length;

                _smooth = dayTime[i].timeLength;
                timer = dayTime[i].timeLength;
            }
        }
    }
    void SwitchParticles(int k) {
        for (int i = 0; i < 4; i++) {
            if (i == k) {
                foreach (Transform child in weatherTime[i].parent) {
                    child.GetComponent<ParticleSystem>().Play();
                }
            }
            else {
                foreach (Transform child in weatherTime[i].parent) {
                    child.GetComponent<ParticleSystem>().Stop();
                }
            }
        }
    }
}
[System.Serializable]
public class DayTime {
    public string name;
    [Space]
    public float timeLength;
    public Color color;
}
[System.Serializable]
public class WeatherTime {
    public string name;
    [Space]
    public float timeLength;

    public Transform parent;
    ParticleSystem[] particles;
}

