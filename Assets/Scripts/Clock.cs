using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clock : MonoBehaviour
{
    const float
        degreesPerHour = 30f,
        degreesPerMinute = 6f,
        degreesPerSecond = 6f;
    
    public Transform hoursTransform, minutesTransform, secondsTransform;
    public bool continous = true;
    public bool useStopwatch = false; // Toggle between clock and stopwatch

    private DateTime startTime;
    private TimeSpan stopwatchElapsed = TimeSpan.Zero;
    private bool stopwatchRunning = false;
    private bool previousUseStopwatch; // Track previous state of useStopwatch

    void Awake(){
        previousUseStopwatch = useStopwatch; // Initialize previous state
        CheckUpdate();
    }

    void Update(){
        // Detect changes in the `useStopwatch` toggle
        if (useStopwatch != previousUseStopwatch) {
            HandleModeSwitch();
            previousUseStopwatch = useStopwatch;
        }

        // Check the mode (clock or stopwatch) and update accordingly
        CheckUpdate();
    }

    void HandleModeSwitch(){
        if (useStopwatch) {
            // Reset stopwatch to zero position when switching to stopwatch mode
            ResetStopwatch();
        } else {
            // Immediately display the current time when switching to clock mode
            UpdateDiscreteClock();
        }
    }

    void CheckUpdate(){
        if (useStopwatch) {
            if (continous && stopwatchRunning){
                UpdateStopwatchContinous();
            }
            else if (!continous && stopwatchRunning) {
                UpdateStopwatchDiscrete();
            }
        } else {
            if (continous){
                UpdateContinousClock();
            }
            else {
                UpdateDiscreteClock();
            }
        }
    }

    [ContextMenu("Start Stopwatch")]
    public void StartStopwatch(){
        if (!stopwatchRunning) {
            stopwatchRunning = true;
            startTime = DateTime.Now - stopwatchElapsed; // Set start time to continue from last elapsed
            Debug.Log("Stopwatch started.");
        }
    }

    [ContextMenu("Stop Stopwatch")]
    public void StopStopwatch(){
        if (stopwatchRunning) {
            stopwatchRunning = false;
            stopwatchElapsed = DateTime.Now - startTime; // Store elapsed time
            Debug.Log("Stopwatch stopped.");
        }
    }

    [ContextMenu("Reset Stopwatch")]
    public void ResetStopwatch(){
        stopwatchRunning = false;
        stopwatchElapsed = TimeSpan.Zero;
        UpdateStopwatchDisplays(TimeSpan.Zero);
        Debug.Log("Stopwatch reset.");
    }

    void UpdateDiscreteClock(){
        DateTime time = DateTime.Now;
        hoursTransform.localRotation = 
            Quaternion.Euler(0f, time.Hour * degreesPerHour, 0f);
        minutesTransform.localRotation = 
            Quaternion.Euler(0f, time.Minute * degreesPerMinute, 0f);
        secondsTransform.localRotation = 
            Quaternion.Euler(0f, time.Second * degreesPerSecond, 0f);
    }

    void UpdateContinousClock(){
        TimeSpan time = DateTime.Now.TimeOfDay;
        hoursTransform.localRotation = 
            Quaternion.Euler(0f, (float)time.TotalHours * degreesPerHour, 0f);
        minutesTransform.localRotation = 
            Quaternion.Euler(0f, (float)time.TotalMinutes * degreesPerMinute, 0f);
        secondsTransform.localRotation = 
            Quaternion.Euler(0f, (float)time.TotalSeconds * degreesPerSecond, 0f);
    }

    void UpdateStopwatchDiscrete(){
        // Update the elapsed time only if the stopwatch is running
        if (stopwatchRunning) {
            stopwatchElapsed = DateTime.Now - startTime;
        }

        // Only update once per second in discrete mode
        int seconds = (int)stopwatchElapsed.TotalSeconds;
        hoursTransform.localRotation = Quaternion.Euler(0f, (float)(stopwatchElapsed.TotalHours) * degreesPerHour, 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, (float)(stopwatchElapsed.TotalMinutes) * degreesPerMinute, 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f, seconds * degreesPerSecond, 0f);
    }

    void UpdateStopwatchContinous(){
        // Update the elapsed time only if the stopwatch is running
        if (stopwatchRunning) {
            stopwatchElapsed = DateTime.Now - startTime;
        }
        UpdateStopwatchDisplays(stopwatchElapsed);
    }

    void UpdateStopwatchDisplays(TimeSpan time){
        hoursTransform.localRotation = 
            Quaternion.Euler(0f, (float)time.TotalHours * degreesPerHour, 0f);
        minutesTransform.localRotation = 
            Quaternion.Euler(0f, (float)time.TotalMinutes * degreesPerMinute, 0f);
        secondsTransform.localRotation = 
            Quaternion.Euler(0f, (float)time.TotalSeconds * degreesPerSecond, 0f);
    }
}
