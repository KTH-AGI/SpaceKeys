using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

/**
 * Parts of code from https://github.com/shapedbyrainstudios/fmod-audio-system
*/

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;
    private EventInstance eventInstance;
    private StudioEventEmitter eventEmitter;
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene.");
        }
        instance = this;

        eventInstances = new List<EventInstance>();  // TODO: finish these lines
        eventEmitters = new List<StudioEventEmitter>();
    }

    private void Start()
    {
        InitializeEvent(FMODEvents.instance.background1);
        InitializeEvent(FMODEvents.instance.background2);
        InitializeEvent(FMODEvents.instance.background3);
        Debug.Log("Background audio played");
        InitializeEvent(FMODEvents.instance.drums1);
        Debug.Log("Drums 1 timeline activated");
        InitializeEvent(FMODEvents.instance.drums2);
        Debug.Log("Drums 2 timeline activated");
        InitializeEvent(FMODEvents.instance.bass1);
        Debug.Log("Bass 1 timeline activated");
        InitializeEvent(FMODEvents.instance.harmony1);
        Debug.Log("Harmony 1 timeline activated");
        InitializeEvent(FMODEvents.instance.harmony2);
        Debug.Log("Harmony 2 timeline activated");
    }

    private void InitializeEvent(EventReference eventReference)
    {
        eventInstance = CreateInstance(eventReference);
        eventInstance.start();
    }

    private void InitializeEventAtTime(EventReference eventReference, int time)
    {  // not used yet
        eventInstance = CreateInstance(eventReference);
        eventInstance.setTimelinePosition(time);
        eventInstance.start();
    }

    public void SetParameterForSeconds(string parameterName, float parameterStartValue, float parameterEndValue, float seconds)
    {
        RuntimeManager.StudioSystem.setParameterByName(parameterName, parameterStartValue);
        Debug.Log(parameterName + " set to " + parameterStartValue + " for " + seconds + " seconds, with reset value " + parameterEndValue);
        StartCoroutine(SetParameterAfterDelay(parameterName, parameterEndValue, seconds));
    }
    private System.Collections.IEnumerator SetParameterAfterDelay(string parameterName, float parameterEndValue, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Set the parameter after the delay
        RuntimeManager.StudioSystem.setParameterByName(parameterName, parameterEndValue);
        Debug.Log(parameterName + " reset to " + parameterEndValue);
    }
    public void SetParameterAndPlay(EventReference sound, string parameterName, float parameterValue)  // Vector3 worldPos, 
    {
        // Set parameter
        eventInstance = CreateInstance(sound);
        eventInstance.setParameterByName(parameterName, parameterValue);

        // Log to make sure the value is set correctly
        float setParameterValue;
        eventInstance.getParameterByName(parameterName, out setParameterValue);
        Debug.Log(parameterName + " is set to " + setParameterValue);

        // Play audio
        // RuntimeManager.PlayOneShot(sound, worldPos);
        eventInstance.start();
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }

    private void CleanUp()
    {
        // stop and release any created instances
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
        // stop all of the event emitters, because if we don't they may hang around in other scenes
        foreach (StudioEventEmitter eventEmitter in eventEmitters)
        {
            eventEmitter.Stop();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
