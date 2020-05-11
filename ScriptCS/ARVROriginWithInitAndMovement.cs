using Godot;
using System;

public class ARVROriginWithInitAndMovement : ARVROrigin
{ 
    // Initial GDNatives libs
    private NativeScript _initLib = null;
    NativeScript _performanceLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrPerformance.gdns");
    NativeScript _displayRefreshRateLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrDisplayRefreshRate.gdns");
    NativeScript _guardianSystemLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrGuardianSystem.gdns");
    NativeScript _trackingTransformLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrTrackingTransform.gdns");
    NativeScript _utilitiesLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrUtilities.gdns");
    NativeScript _vrApiProxyLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrVrApiProxy.gdns");
    
    // these will be initialized in the _ready() function; but they will be only available
    // on device
    // the init config is needed for setting parameters that are needed before the VR system starts up
    Reference ovrInitConfig = null;


    // the other APIs are available during runtime; details about the exposed functions can be found
    // in the *.h files in https://github.com/GodotVR/godot_oculus_mobile/tree/master/src/config
    Reference _ovrPerformance = null;
    Reference _ovrDisplayRefreshRate = null;
    Reference _ovrGuardianSystem = null;
    Reference _ovrTrackingTransform = null;
    Reference _ovrUtilities = null;
    Reference _ovrVrApiProxy = null;
    
    // some of the Oculus VrAPI constants are defined in this file. Have a look into it to learn more
    OvrVrApiTypes ovrVrApiTypes = new OvrVrApiTypes();
    
    public override void _Ready()
    {
        if (InitializeOvrMobileArvrInterface())
        {
            GD.Print("All OK");
        }
        else
        {
            GD.Print("Somethings Wrong");
        }
    }
    
    // this code check for the OVRMobile inteface; and if successful also initializes the
    // .gdns APIs used to communicate with the VR device
    private bool InitializeOvrMobileArvrInterface()
    {
        
        // Find the OVRMobile interface and initialise it if available
        var arvrInterface = ARVRServer.FindInterface("OVRMobile");
        
        if (arvrInterface != null)
        {
            GD.Print("Couldn't find OVRMobile interface");
            return true;
        }
        else
        {
            // the init config needs to be done before arvr_interface.initialize()
            _initLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrInitConfig.gdns");
            
            if (_initLib != null)
            {
                ovrInitConfig = (Reference) _initLib.New();

                if (ovrInitConfig != null)
                {
                    ovrInitConfig.Call("set_render_target_size_multiplier", 1); // setting to 1 here is the default
                }
                else
                {
                    GD.Print("Can't load pliguin for this platform");
                }
            }

            // Configure the interface init parameters.
            if (arvrInterface.Initialize())
            {
                GetViewport().Arvr = true;
                Engine.TargetFps = 72; // Quest

                // load the .gdns classes.
                _displayRefreshRateLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrDisplayRefreshRate.gdns");
                _guardianSystemLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrGuardianSystem.gdns");
                _performanceLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrPerformance.gdns");
                _trackingTransformLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrTrackingTransform.gdns");
                _utilitiesLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrUtilities.gdns");
                _vrApiProxyLib = GD.Load<NativeScript>("res://addons/godot_ovrmobile/OvrVrApiProxy.gdns");

                // and now instance the .gdns classes for use if load was successfull
                if (_displayRefreshRateLib != null) _ovrDisplayRefreshRate = (Reference) _displayRefreshRateLib.New();
                if (_guardianSystemLib != null) _ovrGuardianSystem = (Reference) _guardianSystemLib.New();
                if (_performanceLib != null) _ovrPerformance = (Reference) _performanceLib.New();
                if (_trackingTransformLib != null) _ovrTrackingTransform = (Reference) _trackingTransformLib.New();
                if (_utilitiesLib != null) _ovrUtilities = (Reference) _utilitiesLib.New();
                if (_vrApiProxyLib != null) _ovrVrApiProxy = (Reference) _vrApiProxyLib.New();

                GD.Print("Loaded OVRMobile");
                return true;
            }
            else
            {
                GD.Print("Failed to enable OVRMobile");
                return false;
            }
        }
    }
}
