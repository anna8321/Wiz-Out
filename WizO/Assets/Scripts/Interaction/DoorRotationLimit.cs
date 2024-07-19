using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Autohand.Demo
{
    public class LimitedDoorRotation : MonoBehaviour
    {
        [Header("Configuration de la porte")]
        public HingeJoint hinge;
        public float minYRotation = 0f;  // Rotation minimum sur l'axe Y
        public float maxYRotation = 90f; // Rotation maximum sur l'axe Y
        public float minThreshold = 0.05f;
        public float midThreshold = 0.05f;
        public float maxThreshold = 0.05f;

        [Space]
        public UnityEvent OnMax;
        public UnityEvent OnMid;
        public UnityEvent OnMin;

        private Vector3 closedPosition;
        private Quaternion closedRotation;
        private bool min = false;
        private bool max = false;
        private bool mid = true;

        private void Awake()
        {
            if (!hinge && GetComponent<HingeJoint>())
                hinge = GetComponent<HingeJoint>();

            closedPosition = transform.position;
            closedRotation = transform.rotation;

            JointLimits limits = hinge.limits;
            limits.min = minYRotation;
            limits.max = maxYRotation;
            hinge.limits = limits;
        }

        protected void FixedUpdate()
        {
            float hingeAngle = hinge.angle;

            if (!max && mid && hingeAngle >= maxThreshold)
            {
                Max();
            }

            if (!min && mid && hingeAngle <= minThreshold)
            {
                Min();
            }

            if (hingeAngle <= midThreshold && max && !mid)
            {
                Mid();
            }

            if (hingeAngle >= -midThreshold && min && !mid)
            {
                Mid();
            }
        }

        void Max()
        {
            mid = false;
            max = true;
            OnMax?.Invoke();
        }

        void Mid()
        {
            min = false;
            max = false;
            mid = true;
            OnMid?.Invoke();
        }

        void Min()
        {
            min = true;
            mid = false;
            OnMin?.Invoke();
        }

        public void CloseDoor()
        {
            transform.position = closedPosition;
            transform.rotation = closedRotation;
            if (hinge != null && hinge.GetComponent<Rigidbody>().collisionDetectionMode == CollisionDetectionMode.ContinuousDynamic)
                hinge.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
            hinge.GetComponent<Rigidbody>().isKinematic = true;
        }

        private void OnDrawGizmosSelected()
        {
            if (!hinge && GetComponent<HingeJoint>())
                hinge = GetComponent<HingeJoint>();
        }
    }
}
