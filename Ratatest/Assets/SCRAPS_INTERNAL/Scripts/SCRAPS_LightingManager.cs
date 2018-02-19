using UnityEngine;

public class SCRAPS_LightingManager : MonoBehaviour {

    public enum lType
    {
        indoors = 0,
        outdoorsDay = 1,
        outdoorsNight = 2
    }

    public static lType currentLighting = lType.outdoorsDay;

    public static bool isDaytime = true;

    private float indoorsLight = 0.05f;
    private float indoorsReflect = 0.05f;

    private float outDayLight = 0.8f;
    private float outDayReflect = 0.8f;

    private float outNigLight = 0.1f;
    private float outNigReflect = 0.1f;

    public Material skybox;
    private float dayExp = 1.3f, nigExp = 0.8f;

    void Start()
    {
        //overwrite time of day
        string iniString = INIWorker.IniReadValue(INIWorker.Sections.Config, INIWorker.Keys.value2);
        iniString.ToLower();

        if (iniString == "night")
            isDaytime = false;
        else
            isDaytime = true;

        if (isDaytime)
            currentLighting = lType.outdoorsDay;
        else
            currentLighting = lType.outdoorsNight;
    }

    void Update()
    {
        if(currentLighting == lType.indoors)
        {
            if (RenderSettings.ambientIntensity != indoorsLight)
                RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity , indoorsLight, 0.25f * Time.deltaTime);

            if (RenderSettings.reflectionIntensity != indoorsReflect)
                RenderSettings.reflectionIntensity = Mathf.Lerp(RenderSettings.reflectionIntensity, indoorsReflect, 0.25f * Time.deltaTime);
        }
        else if(currentLighting == lType.outdoorsDay)
        {
            if (RenderSettings.ambientIntensity != outDayLight)
                RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, outDayLight, 0.25f * Time.deltaTime);

            if (RenderSettings.reflectionIntensity != outDayReflect)
                RenderSettings.reflectionIntensity = Mathf.Lerp(RenderSettings.reflectionIntensity, outDayReflect, 0.25f * Time.deltaTime);

            if (skybox.GetFloat("_Exposure") != dayExp)
                skybox.SetFloat("_Exposure", Mathf.Lerp(skybox.GetFloat("_Exposure"), dayExp, 0.25f * Time.deltaTime));
        }
        else if (currentLighting == lType.outdoorsNight)
        {
            if (RenderSettings.ambientIntensity != outNigLight)
                RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, outNigLight, 0.25f * Time.deltaTime);

            if (RenderSettings.reflectionIntensity != outNigReflect)
                RenderSettings.reflectionIntensity = Mathf.Lerp(RenderSettings.reflectionIntensity, outNigReflect, 0.25f * Time.deltaTime);

            if (skybox.GetFloat("_Exposure") != nigExp)
                skybox.SetFloat("_Exposure", Mathf.Lerp(skybox.GetFloat("_Exposure"), nigExp, 0.25f * Time.deltaTime));
        }
    }
}
