using System;
using System.Windows.Forms;
using KettlerReader;

namespace VRController
{
    public partial class VR_GUI : Form
    {
        private readonly string[] notDelete = {"Sunlight", "Camera", "lefthand"};

        private readonly VRConnector vr;

        private Bike bike;

        private VRNode node;

        private VRPanel panel;


        private VRRoute route;

        public VRTime time;

        public VR_GUI(VRConnector vr, string user, string host, string session, Bike bike = null)
        {
            InitializeComponent();
            ConnLabel.Text = string.Format($"Connected to {user}@{host} with session {session}");
            this.vr = vr;
            this.bike = bike;
            panel = new VRPanel(vr);
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            loadNodes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //new VRPanel(this.vr).createPanel("Panel");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadNodes();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            route = new VRRoute(vr);
            route.createRoute(null);
            loadNodes();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (route == null) return;
            route.createRoad();
            loadNodes();
        }

        private void deleteGround_Click(object sender, EventArgs e)
        {
            node = new VRNode(vr);
            node.deleteNode("GroundPlane");
        }


        private void loadNodes()
        {
            var nodes = new VRNode(vr).parseAllnodes();
            this.nodes.Nodes.Clear();
            foreach (var stri in nodes) this.nodes.Nodes.Add(stri);
        }

        private void Routebtn_Click(object sender, EventArgs e)
        {
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            time = new VRTime(vr);
            time.setTime(trackBar1.Value);
        }

        private void VR_GUI_Load(object sender, EventArgs e)
        {
        }

        private void deleteNode_Click(object sender, EventArgs e)

        {
            if (nodes.SelectedNode == null) return;
            var selected = nodes.SelectedNode.Text;
            foreach (var exc in notDelete)
                if (exc.ToLower() == selected.ToLower())
                    return;
            new VRNode(vr).deleteNode(selected);
            loadNodes();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (nodes.SelectedNode == null) return;
            var selected = nodes.SelectedNode.Text;
            new VRPanel(vr).drawTextonPanel(new VRNode(vr).getUUID(selected), "TEST", 10.0, 10.0, 20.0, 0, 0, 0, 1,
                "segoeui");
            //new VRPanel(vr).drawLine(new VRNode(vr).getUUID(selected),10,5,1,2,3,0,0,0,1);
        }

        private void DrawTerrain_Click(object sender, EventArgs e)
        {
            var vrt = new VRTerrain(vr, new VRNode(vr), 256, 256,
                @"C:\Users\casde\Desktop\KettlerProject\VRController\scenes\terrain1.png",
                @"C:\NetworkEngine\data\NetworkEngine\textures\tarmac_diffuse.png",
                @"C:\NetworkEngine\data\NetworkEngine\textures\terrain\grass_green_d.jpg", 0, 20);
            vrt.getHeight(5, 5);
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void updatePanel()
        {
        }
    }
}