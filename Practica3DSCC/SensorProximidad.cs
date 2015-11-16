using System;
using Microsoft.SPOT;
using GTM = Gadgeteer.Modules;
using GT = Gadgeteer;

namespace Practica3DSCC
{
    // Referencia tipo "delegate" para función callback ObjectOn
    public delegate void ObjectOnEventHandler();

    // Referencia tipo "delegate" para función callback ObjectOff
    public delegate void ObjectOffEventHandler();


    

    /*
     * Clase SensorProximidad, encapsula el funcionanmiento del sensor de proximidad infrarrojo.
     * Esta clase gestiona los dos componentes del sensor: el LED infrarrojo y el foto-transistor.
     * Además, dispara dos eventos: ObjectOn y ObjectOff cuando el sensor detecta la presencia o
     * ausencia de un objeto.
     */
    class SensorProximidad
    {
        //EVENTO ObjectOff: Disparar este evento cuando el sensor detecte la ausencia del objeto
        public event ObjectOffEventHandler ObjectOff;

        //EVENTO ObjectOn: Disparar este evento cuando el sensor detecte la presencia de un objeto
        public event ObjectOnEventHandler ObjectOn;

        private GT.Timer timer; // every second (1000ms)

        private enum Estado
        {
            Ninguno,
            Presente,
            Ausente
        }

        Estado estado;

        private GT.SocketInterfaces.AnalogInput entrada = null;
        private GT.SocketInterfaces.DigitalOutput salida = null;
        
        public SensorProximidad(GTM.GHIElectronics.Extender extender)
        {
            //TODO: Inicializar el sensor

            entrada = extender.CreateAnalogInput(GT.Socket.Pin.Four);
            salida = extender.CreateDigitalOutput(GT.Socket.Pin.Eight, false);
            timer = new GT.Timer(1000);
            timer.Tick += timer_Tick;
            estado = Estado.Ninguno;  
        }

        void timer_Tick(GT.Timer timer)
        {
            Double voltaje = entrada.ReadVoltage();
            Debug.Print("Voltaje: " + entrada.ReadVoltage());

            if (voltaje < 3 )
            {
                if (estado == Estado.Ninguno || estado == Estado.Ausente)
                {
                    ObjectOn();
                    estado = Estado.Presente;
                }                
            }
            else
            {
                if (estado == Estado.Presente)
                {
                    ObjectOff();
                    estado = Estado.Ausente;
                }                
            }
        }

        public void StartSampling()
        {
            //TODO: Activar el LED infrarrojo y empezar a muestrear el foto-transistor
            //entrada.ReadVoltage();
            Debug.Print("Voltaje: " + entrada.ReadVoltage());
            salida.Write(true);
            Debug.Print("Encendido");
            timer.Start(); 
        }

        public void StopSampling()
        {
            //TODO: Desactivar el LED infrarrojo y detener el muestreo del foto-transistor
            salida.Write(false);
            Debug.Print("Apagado");
            timer.Stop(); 
        }
    }
}
