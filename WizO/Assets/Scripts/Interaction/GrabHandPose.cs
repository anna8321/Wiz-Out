using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabHandPose : MonoBehaviour
{
    public HandData rightHandPose;
    public HandData leftHandPose;

    private Vector3 startingHandPosition;
    private Vector3 finalHandPosition;

    private Quaternion startingHandRotation;
    private Quaternion finalHandRotation;

    private Quaternion[] startingFingerRotations;
    private Quaternion[] finalFingerRotations;

    [SerializeField] private HandData leftHandModel;
    [SerializeField] private HandData rightHandModel; 

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable graInteractable = GetComponent<XRGrabInteractable>();

        //rightHandPose = GameObject.FindGameObjectWithTag("Right Hand").GetComponentInParent<HandData>();
        //leftHandPose = GameObject.FindGameObjectWithTag("Left Hand").GetComponentInParent<HandData>();
        graInteractable.selectEntered.AddListener(SetUpPose);
        graInteractable.selectExited.AddListener(UnsetPose);

        rightHandPose.gameObject.SetActive(false);
        leftHandPose.gameObject.SetActive(false);

    }

    public void SetUpPose(BaseInteractionEventArgs arg) 
    {
        if (arg.interactorObject is XRDirectInteractor)
        {
            if(arg.interactorObject.transform.gameObject.CompareTag("Left Hand"))
            {
                rightHandPose = leftHandModel;
                HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
                handData.animator.enabled = false;

                SetHandDataValues(handData, rightHandPose);
                SetHandData(handData, finalHandPosition, finalHandRotation, finalFingerRotations);
            }
            else if (arg.interactorObject.transform.gameObject.CompareTag("Right Hand"))
            {
                rightHandPose = rightHandModel;
                HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
                handData.animator.enabled = false;

                SetHandDataValues(handData, rightHandPose);
                SetHandData(handData, finalHandPosition, finalHandRotation, finalFingerRotations);
            }
           
        }
    }

    public void UnsetPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = true;

            SetHandData(handData, startingHandPosition, startingHandRotation, startingFingerRotations);
        }
    }

    public void SetHandDataValues(HandData h1, HandData h2)
    {
        startingHandPosition = new Vector3 (h1.root.localPosition.x / h1.root.localScale.x,
            h1.root.localPosition.y / h1.root.localScale.y, h1.root.localPosition.z / h1.root.localScale.z);
        finalHandPosition = new Vector3(h2.root.localPosition.x / h2.root.localScale.x,
            h2.root.localPosition.y / h2.root.localScale.y, h2.root.localPosition.z / h2.root.localScale.z);

        startingHandRotation = h1.root.localRotation;
        finalHandRotation = h2.root.localRotation;

        startingFingerRotations = new Quaternion[h1.fingerBones.Length];
        finalFingerRotations = new Quaternion[h1.fingerBones.Length];

        for (int i = 0; i < h1.fingerBones.Length; i++)
        {
            startingFingerRotations[i] = h1.fingerBones[i].localRotation;
            finalFingerRotations[i] = h2.fingerBones[i].localRotation;
        }
    }

    public void SetHandData(HandData h,  Vector3 newPosition, Quaternion newRotation, Quaternion[] newBonesRotation)
    {
        h.root.localPosition = newPosition;
        h.root.localRotation = newRotation;

        for (int i = 0; i < newBonesRotation.Length; i++)
        {
            h.fingerBones[i].localRotation = newBonesRotation[i];

        }

    }

   




}
