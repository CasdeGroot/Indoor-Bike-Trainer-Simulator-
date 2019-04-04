using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{

    class BikeConnection
    {
        private String port = "COM3";
        private SerialPort serialPort;
        private int send, received = 0;

        public BikeConnection()
        {
            serialPort = new SerialPort(port, 9600);
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            serialPort.Open();

          
            sendData("RS");
            sendData("ST");
            close();
        }
        public void close()
        {
            while(received != send)
            {

            }
            serialPort.Close();
        }
        public void sendData(String data)
        {
            serialPort.WriteLine(data);
            send++;
        }
        private void DataReceivedHandler(
                       object sender,
                       SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Console.WriteLine("Data Received:");
            Console.Write(indata);
            received++;
        
        }
    }
}
