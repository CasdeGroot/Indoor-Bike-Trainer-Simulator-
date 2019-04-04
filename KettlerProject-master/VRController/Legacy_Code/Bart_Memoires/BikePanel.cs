/*namespace VRController
{
    class BikePanel:VRConnector
    {
        private String node { get; set; }
        public BikePanel(String node) : base()
        {
            this.node = node;
            clearPane();

 
        }
        public void drawTextonPanel(String text, double x, double y, double size, double r, double g, double b, double a, string font)
        {
            Dictionary<string, object> mainDict = new Dictionary<string, object>();
            mainDict.Add("id", "scene/panel/drawtext");
            Dictionary<string, object> panelCommand = new Dictionary<string, object>();
            panelCommand.Add("id", node);
            panelCommand.Add("text", text);
            panelCommand.Add("position", new double[] {x,y});
            panelCommand.Add("size", size);
            panelCommand.Add("color", new double[] { r,g,b,a });
            panelCommand.Add("font", font);
            mainDict.Add("data", panelCommand);
            sendTunnelData(mainDict);
        }
        public void clearPane()
        {
            Dictionary<string, object> clearPane = new Dictionary<string, object>();
            clearPane.Add("id", "scene/panel/clear");
            Dictionary<string, object> panelCommand = new Dictionary<string, object>();
            panelCommand.Add("id", node);
            clearPane.Add("data", panelCommand);
            sendTunnelData(clearPane);
        }
        public void drawSpeedometer (double speed)
        {
            double hoek = speed / 360;
            drawLine(1, 0, 0, 0, 50, 0, 0, 0, 1);
            drawLine(1, 0, 0, 50, 0, 0, 0, 0, 1);
            drawLine(1, 0, 50, 50, 50, 0, 0, 0, 1);
            drawLine(1, 50, 0, 50, 50, 0, 0, 0, 1);
        }

        public void drawLine(double width, double x1, double y1, double x2, double y2, double r, double g, double b, double a)
        {
            Dictionary<string, object> mainDict = new Dictionary<string, object>();
            mainDict.Add("id", "scene/panel/drawline");
            Dictionary<string, object> panelCommand = new Dictionary<string, object>();
            panelCommand.Add("id", node);
            panelCommand.Add("width", width);
            panelCommand.Add("lines", new double[] { x1, y1,x2,y2,r,g,b,a });
            mainDict.Add("data", panelCommand);
            sendTunnelData(mainDict);
        }

    }
}*/

