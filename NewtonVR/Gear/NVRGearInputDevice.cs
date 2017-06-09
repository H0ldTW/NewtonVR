﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

#if NVR_Gear
namespace NewtonVR
{
    public class NVRGearInputDevice : NVRInputDevice
    {
        private GameObject RenderModel;

        private Dictionary<NVRButtons, OVRInput.Button> ButtonMapping = new Dictionary<NVRButtons, OVRInput.Button>(new NVRButtonsComparer());

        public override void Initialize(NVRHand hand)
        {
            base.Initialize(hand);

            SetupButtonMapping();
        }

        protected virtual void SetupButtonMapping()
        {
			ButtonMapping.Add(NVRButtons.Touchpad, OVRInput.Button.One);
            ButtonMapping.Add(NVRButtons.B, OVRInput.Button.Two);
        }

        private OVRInput.Button GetButtonMap(NVRButtons button)
		{
            if (ButtonMapping.ContainsKey(button) == false)
            {
                //Debug.LogError("No Oculus button configured for: " + button.ToString());
                return OVRInput.Button.None;
            }
            return ButtonMapping[button];
        }

        public override float GetAxis1D(NVRButtons button)
        {
				return 0.0f; 
        }

        public override Vector2 GetAxis2D(NVRButtons button)
        {
				return (button == NVRButtons.Touchpad) ? new  Vector2(Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y")) : Vector2.zero; 
        }

        public override bool GetPressDown(NVRButtons button)
        {
                return OVRInput.GetDown(GetButtonMap(button));
        }

        public override bool GetPressUp(NVRButtons button)
        {
                return OVRInput.GetUp(GetButtonMap(button));
        }

        public override bool GetPress(NVRButtons button)
        {
                return OVRInput.Get(GetButtonMap(button));
        }

        public override bool GetTouchDown(NVRButtons button)
        {
				return false;
        }

        public override bool GetTouchUp(NVRButtons button)
        {
				return false;
        }

        public override bool GetTouch(NVRButtons button)
        {
				return (button == NVRButtons.Touchpad) ? Input.GetButton("Oculus_GearVR_TouchPad") : OVRInput.Get(GetButtonMap(button));
        }

        public override bool GetNearTouchDown(NVRButtons button)
        {
                return false;
        }

        public override bool GetNearTouchUp(NVRButtons button)
        {
				return false;
        }

        public override bool GetNearTouch(NVRButtons button)
        {
				return false;
        }

        public override void TriggerHapticPulse(ushort durationMicroSec = 500, NVRButtons button = NVRButtons.Touchpad)
        {
        }

        public override bool IsCurrentlyTracked
        {
            get
            {
                return true;
            }
        }


        public override GameObject SetupDefaultRenderModel()
        {
			RenderModel = new GameObject("Render Model for " + Hand.gameObject.name);
            RenderModel.name = "Render Model for " + Hand.gameObject.name;
            RenderModel.transform.parent = Hand.transform;
            RenderModel.transform.localPosition = Vector3.zero;
            RenderModel.transform.localRotation = Quaternion.identity;
            RenderModel.transform.localScale = Vector3.one;

            return RenderModel;
        }

        public override bool ReadyToInitialize()
        {
            return true;
        }

        public override string GetDeviceName()
        {
			return "GearVR";
        }

        public override Collider[] SetupDefaultPhysicalColliders(Transform ModelParent)
        {
			Collider[] colliders = null;

			SphereCollider defaultCollider = ModelParent.gameObject.AddComponent<SphereCollider>();
			defaultCollider.isTrigger = true;
			defaultCollider.radius = 0.15f;

			colliders = new Collider[] { defaultCollider };

			return colliders;
        }

        public override Collider[] SetupDefaultColliders()
        {
            Collider[] Colliders = null;
            
            SphereCollider OculusCollider = RenderModel.AddComponent<SphereCollider>();
            OculusCollider.isTrigger = true;
            OculusCollider.radius = 0.15f;

            Colliders = new Collider[] { OculusCollider };

            return Colliders;
        }
        
    }
}
#else
namespace NewtonVR
{
	public class NVRGearInputDevice : NVRInputDevice
    {
        public override bool IsCurrentlyTracked
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override float GetAxis1D(NVRButtons button)
        {
            throw new NotImplementedException();
        }

        public override Vector2 GetAxis2D(NVRButtons button)
        {
            throw new NotImplementedException();
        }

        public override string GetDeviceName()
        {
            throw new NotImplementedException();
        }

        public override bool GetNearTouch(NVRButtons button)
        {
            throw new NotImplementedException();
        }

        public override bool GetNearTouchDown(NVRButtons button)
        {
            throw new NotImplementedException();
        }

        public override bool GetNearTouchUp(NVRButtons button)
        {
            throw new NotImplementedException();
        }

        public override bool GetPress(NVRButtons button)
        {
            throw new NotImplementedException();
        }

        public override bool GetPressDown(NVRButtons button)
        {
            throw new NotImplementedException();
        }

        public override bool GetPressUp(NVRButtons button)
        {
            throw new NotImplementedException();
        }

        public override bool GetTouch(NVRButtons button)
        {
            throw new NotImplementedException();
        }

        public override bool GetTouchDown(NVRButtons button)
        {
            throw new NotImplementedException();
        }

        public override bool GetTouchUp(NVRButtons button)
        {
            throw new NotImplementedException();
        }

        public override bool ReadyToInitialize()
        {
            throw new NotImplementedException();
        }

        public override Collider[] SetupDefaultColliders()
        {
            throw new NotImplementedException();
        }

        public override Collider[] SetupDefaultPhysicalColliders(Transform ModelParent)
        {
            throw new NotImplementedException();
        }

        public override GameObject SetupDefaultRenderModel()
        {
            throw new NotImplementedException();
        }

        public override void TriggerHapticPulse(ushort durationMicroSec = 500, NVRButtons button = NVRButtons.Touchpad)
        {
            throw new NotImplementedException();
        }
    }
}
#endif
