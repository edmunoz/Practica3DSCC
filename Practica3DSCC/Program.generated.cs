//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Practica3DSCC {
    using Gadgeteer;
    using GTM = Gadgeteer.Modules;
    
    
    public partial class Program : Gadgeteer.Program {
        
        /// <summary>The Camera module using socket 3 of the mainboard.</summary>
        private Gadgeteer.Modules.GHIElectronics.Camera camera;
        
        /// <summary>The USB Client DP module using socket 1 of the mainboard.</summary>
        private Gadgeteer.Modules.GHIElectronics.USBClientDP usbClientDP;
        
        /// <summary>The Display T35 module using sockets 14, 13, 12 and 10 of the mainboard.</summary>
        private Gadgeteer.Modules.GHIElectronics.DisplayT35 displayT35;
        
        /// <summary>The Extender module using socket 9 of the mainboard.</summary>
        private Gadgeteer.Modules.GHIElectronics.Extender extender;
        
        /// <summary>This property provides access to the Mainboard API. This is normally not necessary for an end user program.</summary>
        protected new static GHIElectronics.Gadgeteer.FEZSpider Mainboard {
            get {
                return ((GHIElectronics.Gadgeteer.FEZSpider)(Gadgeteer.Program.Mainboard));
            }
            set {
                Gadgeteer.Program.Mainboard = value;
            }
        }
        
        /// <summary>This method runs automatically when the device is powered, and calls ProgramStarted.</summary>
        public static void Main() {
            // Important to initialize the Mainboard first
            Program.Mainboard = new GHIElectronics.Gadgeteer.FEZSpider();
            Program p = new Program();
            p.InitializeModules();
            p.ProgramStarted();
            // Starts Dispatcher
            p.Run();
        }
        
        private void InitializeModules() {
            this.camera = new GTM.GHIElectronics.Camera(3);
            this.usbClientDP = new GTM.GHIElectronics.USBClientDP(1);
            this.displayT35 = new GTM.GHIElectronics.DisplayT35(14, 13, 12, 10);
            this.extender = new GTM.GHIElectronics.Extender(9);
        }
    }
}
