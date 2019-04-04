using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program();

        }
        SpeechRecognizer sRecognize = new SpeechRecognizer();
        public Program(){
            sRecognize.SpeechRecognized += sRecognize_SpeechRecognized;
        }

        private void sRecognize_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine(e.Result.Text.ToString());
        }
    }
}
