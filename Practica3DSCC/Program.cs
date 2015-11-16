﻿using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

//Estas referencias son necesarias para usar GLIDE
using GHI.Glide;
using GHI.Glide.Display;
using GHI.Glide.UI;

namespace Practica3DSCC
{
    public partial class Program
    {
        //Objetos de interface gráfica GLIDE
        private GHI.Glide.Display.Window controlWindow;
        private GHI.Glide.Display.Window camaraWindow;
        private Button btn_start;
        private Button btn_stop;
        private SensorProximidad sensor ; 
        // This method is run when the mainboard is powered up or reset. 

        enum Estado { Sensor_Off, Sensor_On, Monitoreo };

        private Estado estado;

        
    
        void ProgramStarted()
        {
            /*******************************************************************************************
            Modules added in the Program.gadgeteer designer view are used by typing 
            their name followed by a period, e.g.  button.  or  camera.
            
            Many modules generate useful events. Type +=<tab><tab> to add a handler to an event, e.g.:
                button.ButtonPressed +=<tab><tab>
            
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
                timer.Tick +=<tab><tab>
                timer.Start();
            *******************************************************************************************/
            
            sensor = new SensorProximidad(extender);
            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");


            camera.BitmapStreamed += camera_BitmapStreamed; 

            //Carga las ventanas
            controlWindow = GlideLoader.LoadWindow(Resources.GetString(Resources.StringResources.controlWindow));
            camaraWindow = GlideLoader.LoadWindow(Resources.GetString(Resources.StringResources.camaraWindow));
            GlideTouch.Initialize();

            //Inicializa los botones en la interface
            btn_start = (Button)controlWindow.GetChildByName("start");
            btn_stop = (Button)controlWindow.GetChildByName("stop");
            btn_start.TapEvent += btn_start_TapEvent;
            btn_stop.TapEvent += btn_stop_TapEvent;


            //Eventos sensor
            sensor.ObjectOn += sensor_ObjectOn;
            sensor.ObjectOff += sensor_ObjectOff;  

            //Selecciona mainWindow como la ventana de inicio
            Glide.MainWindow = controlWindow;


            estado = Estado.Monitoreo;

            
            
        }

        void camera_BitmapStreamed(Camera sender, Bitmap e)
        {
            displayT35.SimpleGraphics.DisplayImage(e, 0, 0);
 
        }



        private void cambiarEstado(Estado es)
        {
            TextBlock text = (TextBlock)controlWindow.GetChildByName("status");
            switch (es)
            {
                case Estado.Sensor_Off: 
                    text.Text = "Monitoreo Off";
                    
                    break;
                case Estado.Sensor_On:
                    camera.StopStreaming();
                    text.Text = "Monitoreo On";
                    Glide.MainWindow = controlWindow;
                    
                    break;
                case Estado.Monitoreo:
                    Glide.MainWindow = camaraWindow;
                    camera.StartStreaming();
                    break;
                default:
                    break;
            }
        }




        void sensor_ObjectOff()
        {
            Debug.Print("Objeto salio");
            cambiarEstado(Estado.Sensor_On);
        }

        void sensor_ObjectOn()
        {
            Debug.Print("Objeto entro");
            
            //camera.StartStreaming();
            cambiarEstado(Estado.Monitoreo);
        }

        void btn_stop_TapEvent(object sender)
        {
            Debug.Print("Stop");
            sensor.StopSampling();
            cambiarEstado(Estado.Sensor_Off);
        }

        void btn_start_TapEvent(object sender)
        {
            Debug.Print("Start");
            sensor.StartSampling();
            cambiarEstado(Estado.Sensor_On);
            
        }
    }
}
