using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.IO.Ports;
using System.Text;

namespace System.Diagnostics
{
    public enum DebuggerBrowsableState
    {
        Never = 0,
        Collapsed = 2,
        RootHidden = 3
    }
}

namespace NetduinoApplication2
{
    public class Program
    {
        public static void Main()
        {
            var led = new OutputPort(Pins.ONBOARD_LED, false);
            var mfrc = new Mfrc522(SPI.SPI_module.SPI1, Pins.GPIO_PIN_D9, Pins.GPIO_PIN_D10);
            int i = 0;
            // Request
            while (true)
            {
                mfrc.HaltTag(); //envoie la commande halt au capteur RFID pour éviter qu'il le lise pleins de fois
                if(mfrc.IsTagPresent()) //Verifie si il y a une carte devant le capteur
                {
                    i++;
                    led.Write(true);
                    Uid idCarte; // pour récupérer la valeur retour avec la méthode ReadUid()

                    //int id = idCarte.GetHashCode();
                    //int taille = id.ToString().Length;
                    if (i == 1)
                    {
                        idCarte = mfrc.ReadUid();
                        mfrc.HaltTag();
                        if (idCarte.GetHashCode().ToString().Length == 9)
                        {
                            mfrc.HaltTag();
                            Debug.Print(idCarte.GetHashCode().ToString());
                            mfrc.HaltTag();
                        }
                        mfrc.HaltTag();
                        i = 0;
                        mfrc.HaltTag();

                    }
                    mfrc.HaltTag();
                }
                else
                {
                    mfrc.HaltTag();
                    led.Write(false);
                    mfrc.HaltTag();
                }
                mfrc.HaltTag();
            }

        }

    }
}
