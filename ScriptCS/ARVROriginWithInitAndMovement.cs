using Godot;
using System;

public class ARVROriginWithInitAndMovement : ARVROrigin
{ 
    // Initial GDNatives libs
    NativeScript initLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrInitConfig.gdns");
    NativeScript performanceLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrPerformance.gdns");
    NativeScript displayRefreshRateLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrDisplayRefreshRate.gdns");
    NativeScript guardianSystemLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrGuardianSystem.gdns");
    NativeScript trackingTransformLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrTrackingTransform.gdns");
    NativeScript utilitiesLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrUtilities.gdns");
    NativeScript vrApiProxyLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrVrApiProxy.gdns");
    
    // these will be initialized in the _ready() function; but they will be only available
    // on device
    // the init config is needed for setting parameters that are needed before the VR system starts up
    Reference ovrInitConfig = null;


    // the other APIs are available during runtime; details about the exposed functions can be found
    // in the *.h files in https://github.com/GodotVR/godot_oculus_mobile/tree/master/src/config
    Reference ovrPerformance = null;
    Reference ovrDisplayRefreshRate = null;
    Reference ovrGuardianSystem = null;
    Reference ovrTrackingTransform = null;
    Reference ovrUtilities = null;
    Reference ovrVrApiProxy = null;
    
    // some of the Oculus VrAPI constants are defined in this file. Have a look into it to learn more
    OvrVrApiTypes ovrVrApiTypes = new OvrVrApiTypes();
    
    public override void _Ready()
    {
        
    }
}
