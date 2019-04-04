using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NetworkConnector
{
    public partial class ServerClientGUI : Form
    {
        private readonly ServerClient client;
        private List<ClientIdentifier> identifiers = new List<ClientIdentifier>();

        public ServerClientGUI(ServerClient client)
        {
            InitializeComponent();
            this.client = client;
            client.errorOccured += errorHappened;
            client.ConnectionsUpdate += refreshConnections;
            client.sendData(Commands.NOTIFYONNEWCONNECTED);
            client.connectionCloseNotifier += closeWindow;
            client.sendData(Commands.ALLCONNECTED);
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        public void refreshConnections(List<ClientIdentifier> connected)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => refreshConnections(connected)));
            }
            else
            {
                identifiers = connected;
                connectedView.Nodes.Clear();

                foreach (var connect in connected)
                    connectedView.Nodes.Add(new TreeNode(connect.ToString()));
            }
        }

        public void errorHappened(string error)
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(() => errorHappened(error)));
            else
                errorList.Items.Add(error);
        }

        public void closeWindow()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(closeWindow));
            else
                Hide();
        }

        private void ServerClientGUI_Load(object sender, EventArgs e)
        {
        }

        public ClientIdentifier searchByName(string name)
        {
            foreach (var ident in identifiers)
                if (ident.ToString() == name)
                    return ident;
            return null;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //if (connectedView.SelectedNode == null) return;
            var identifier = searchByName(connectedView.SelectedNode.Text);
            client.sendData(new Message(Commands.DISCONNECT, identifier));
        }
    }
}