using System.Windows.Forms;

namespace VRController
{
    internal class Program
    {
        /// <summary>
        ///     Starts the main program and the GUI
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            var vr = new VRConnector("145.48.6.10", 6666);
            var vrConnectorGui = new VRConnector_GUI(vr);
            Application.EnableVisualStyles();
            vrConnectorGui.Show();
            vrConnectorGui.Activate();
            vrConnectorGui.Visible = true;
            vrConnectorGui.Show();
            vrConnectorGui.SetDesktopLocation(0, 0);
            Application.Run(vrConnectorGui);
        }
    }
}