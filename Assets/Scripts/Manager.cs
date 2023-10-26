using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;
    [SerializeField] float animationSpeed = 0.5f;
    [SerializeField] Slider animationSlider;
    [SerializeField] Text animationText;
    [SerializeField] FlexibleColorPicker flexibleColorPicker;
    float timer;

    [SerializeField] Camera walkingCamera;
    [SerializeField] Camera runningCamera;
    [SerializeField] Camera bikingCamera;
    [SerializeField] Camera fishingCamera;
    [SerializeField] Camera hMCamera;
    [SerializeField] Camera bikingStartCamera;

    [SerializeField] Camera originalWalkingCamera;
    [SerializeField] Camera originalRunningCamera;
    [SerializeField] Camera originalBikingCamera;
    [SerializeField] Camera originalFishingCamera;
    [SerializeField] Camera originalHMCamera;
    [SerializeField] Camera originalBikingStartCamera;

    const float UpYPosition = -3.35f;
    const float DownYPosition = -6.65f;
    const float LeftYPosition = -9.95f;
    const float RightYPosition = -13.25f;
    const float SpecialYPosition = -18.25f;
    const int originalCameraOffset = -25;

    const int FishAnimations = 4;
    const int BikeStartStopAnimations = 4;

    int walkFrame;
    //int bikeFrame;
    int fishFrame = -1;
    int bikeStartStopframe;

    bool walkleftAnimation;
    //bool runleftAnimation;
    bool fishCastOutAnimation = false;
    bool bikeStartStopLeftAnimation;

    const int XOffestPosition = 18;
    const int FrameSize = 33;
    const float center = 15.5f;
    const int divided = 10;

    void Start()
    {
        dropdown.onValueChanged.AddListener(delegate {
            onDropdownSelected(dropdown.value);
        });

        animationSlider.onValueChanged.AddListener(delegate {
            AnimationValue(animationSlider.value);
        });

        flexibleColorPicker.onColorChange.AddListener(delegate {
            ColourValue(flexibleColorPicker.color);
        });
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > animationSpeed)
        {
            timer = 0;
            NextFrame();
        }
    }


    void onDropdownSelected(int index)
    {
        switch (index)
        {
            case 0:
                MoveAllCameraYPositions(UpYPosition);
                break;
            case 1:
                MoveAllCameraYPositions(DownYPosition);
                break;
            case 2:
                MoveAllCameraYPositions(LeftYPosition);
                break;
            default:
                MoveAllCameraYPositions(RightYPosition);
                break;
        }
    }

    void AnimationValue(float value)
    {
        animationText.text = value.ToString("0.00");
        animationSpeed = 1/value;
    }

    void ColourValue(Color value)
    {
        walkingCamera.backgroundColor = value;
        runningCamera.backgroundColor = value;
        bikingCamera.backgroundColor = value;
        fishingCamera.backgroundColor = value;
        hMCamera.backgroundColor = value;
        bikingStartCamera.backgroundColor = value;

        originalWalkingCamera.backgroundColor = value;
        originalRunningCamera.backgroundColor = value;
        originalBikingCamera.backgroundColor = value;
        originalFishingCamera.backgroundColor = value;
        originalHMCamera.backgroundColor = value;
        originalBikingStartCamera.backgroundColor = value;
    }

    void MoveAllCameraYPositions(float newHeight)
    {
        walkingCamera.transform.position = new Vector3(walkingCamera.transform.position.x, newHeight, walkingCamera.transform.position.z);
        runningCamera.transform.position = new Vector3(runningCamera.transform.position.x, newHeight, runningCamera.transform.position.z);
        bikingCamera.transform.position = new Vector3(bikingCamera.transform.position.x, newHeight, bikingCamera.transform.position.z);
        fishingCamera.transform.position = new Vector3(fishingCamera.transform.position.x, newHeight, fishingCamera.transform.position.z);
        bikingStartCamera.transform.position = new Vector3(bikingStartCamera.transform.position.x, newHeight, bikingStartCamera.transform.position.z);
    }

    void NextFrame()
    {
        //Walk
        if(walkFrame == 0)
        {
            if(walkleftAnimation)
            {
                walkFrame = 1;
            }
            else
            {
                walkFrame = 2;
            }
        }
        else
        {
            walkFrame = 0;
            walkleftAnimation = !walkleftAnimation;
        }

        CurrentFrame(walkFrame,0,walkingCamera.transform,originalWalkingCamera.transform);
        CurrentFrame(walkFrame, 3,runningCamera.transform,originalRunningCamera.transform);
        CurrentFrame(walkFrame, 6, bikingCamera.transform,originalBikingCamera.transform);

        //Fishing
        if(fishFrame >= 3 || fishFrame < 0)
        {
            fishCastOutAnimation = !fishCastOutAnimation;
        }

        if(fishCastOutAnimation)
        {
            fishFrame++;
        }
        else
        {
            fishFrame--;
        }

        if(fishFrame<0)
        {
            CurrentFrame(0, 0, fishingCamera.transform, originalFishingCamera.transform);
            CurrentFrame(0, 0, hMCamera.transform, originalHMCamera.transform, DownYPosition);
        }
        else
        {
            CurrentFrame(fishFrame, 11, fishingCamera.transform, originalFishingCamera.transform);
            CurrentFrame(fishFrame, 0, hMCamera.transform, originalHMCamera.transform, SpecialYPosition);
        }

        //BikingStartStop
        bikeStartStopframe++;
        if (bikeStartStopframe >= 12)
        {
            bikeStartStopframe = 0;
        }

        if (bikeStartStopframe > 5)
        {
            CurrentFrame(0, 9, bikingStartCamera.transform, originalBikingStartCamera.transform);
        }
        else
        {
            CurrentFrame(bikeStartStopframe%3, 6, bikingStartCamera.transform,originalBikingStartCamera.transform);
        }
    }

    void CurrentFrame(int frame, int originalOffset, Transform currentCamera, Transform originalCamera, float specifiedYoffset = 0)
    {
        float Xpos = (XOffestPosition + (FrameSize * (frame + originalOffset)) + center) / divided;

        if(specifiedYoffset == 0)
        {
            currentCamera.position = new Vector3(Xpos, currentCamera.position.y, currentCamera.position.z);
            originalCamera.position = new Vector3(Xpos, currentCamera.position.y + originalCameraOffset, currentCamera.position.z);
        }
        else
        {
            currentCamera.position = new Vector3(Xpos, specifiedYoffset, currentCamera.position.z);
            originalCamera.position = new Vector3(Xpos, specifiedYoffset + originalCameraOffset, currentCamera.position.z);
        }
    }


}
