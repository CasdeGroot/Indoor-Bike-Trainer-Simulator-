using System;
using System.Windows.Forms;

namespace KettlerReader
{
    public partial class SIM_GUI : Form
    {
        private readonly Timer algoritmeTimer;

        private readonly Simulator sim;

        /// <summary>
        ///     Connect gui to simulator
        /// </summary>
        /// <param name="sim"></param>
        public SIM_GUI(Connector sim)
        {
            InitializeComponent();
            this.sim = (Simulator) sim;
            algoritmeTimer = new Timer();
            algoritmeTimer.Interval = 1000;
            algoritmeTimer.Start();
            algoritmeTimer.Tick += algoritme_Event;

            PTcheck.CheckedChanged += stopStart;
            PTcheck.Checked = true;

            HBUP.ValueChanged += updateUp;
            WHUPP.ValueChanged += updateUp;
            DISTUP.ValueChanged += updateUp;
            SPKUP.ValueChanged += updateUp;
            SPUP.ValueChanged += updateUp;
            PWUP.ValueChanged += updateUp;
            EUP.ValueChanged += updateUp;
        }

        /// <summary>
        ///     Code that starts the heartbeat when the timer elapsed
        /// </summary>
        /// <param name="sender">AlgorithmTimer</param>
        /// <param name="e"></param>
        public void algoritme_Event(object sender, EventArgs e)
        {
            sim.setData(Stats.StatName.TIME, sim.getStat(Stats.StatName.TIME) + 1);
            sim.programmed.Clear();
            if (HBcheck.Checked) sim.programmed.Add(Stats.StatName.HEARTBEAT);
            if (VScheck.Checked) sim.programmed.Add(Stats.StatName.RPM);
            if (SPcheck.Checked) sim.programmed.Add(Stats.StatName.SPEED);
            if (PDcheck.Checked) sim.programmed.Add(Stats.StatName.DISTANCE);
            if (PTcheck.Checked) sim.programmed.Add(Stats.StatName.TIME);
            if (PEcheck.Checked) sim.programmed.Add(Stats.StatName.ENERGY);
            if (PCcheck.Checked) sim.programmed.Add(Stats.StatName.WATTAGE);

            HBUP.Enabled = !HBcheck.Checked;
            SPKUP.Enabled = !SPcheck.Checked;
            WHUPP.Enabled = !PCcheck.Checked;
            DISTUP.Enabled = !PDcheck.Checked;
            SPUP.Enabled = !VScheck.Checked;
            EUP.Enabled = !PEcheck.Checked;

            sim.checkPrograms();
            update();
        }

        /// <summary>
        ///     starts/stops the algorithm for selected values
        /// </summary>
        /// <param name="sender">seleted value</param>
        /// <param name="e"></param>
        public void stopStart(object sender, EventArgs e)
        {
            if (PTcheck.Checked) algoritmeTimer.Start();
            else algoritmeTimer.Stop();
        }

        /// <summary>
        ///     Update method when program is count up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void updateUp(object sender, EventArgs e)
        {
            var value = (double) ((NumericUpDown) sender).Value;
            var name = ((NumericUpDown) sender).Name;

            if (name == HBUP.Name) sim.setData(Stats.StatName.HEARTBEAT, value);
            if (name == WHUPP.Name) sim.setData(Stats.StatName.WATTAGE, value);
            if (name == DISTUP.Name) sim.setData(Stats.StatName.DISTANCE, value);
            if ((name == MUP.Name) || (name == HUP.Name))
            {
                var hour = (double) HUP.Value;
                var min = (double) MUP.Value/60.0;
                sim.setData(Stats.StatName.TIME, hour + min);
            }

            if (name == SPKUP.Name) sim.setData(Stats.StatName.SPEED, value);
            if (name == SPUP.Name) sim.setData(Stats.StatName.RPM, value);
            if (name == PWUP.Name) sim.setData(Stats.StatName.PROGRAMWATTAGE, value);
            if (name == EUP.Name) sim.setData(Stats.StatName.ENERGY, value);
        }

        private void HBcurrent_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void PCcheck_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void PDcurrent_Click(object sender, EventArgs e)
        {
        }

        private void PTcurrent_Click(object sender, EventArgs e)
        {
        }

        private void PWcurrent_Click(object sender, EventArgs e)
        {
        }

        private void SIM_GUI_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Gets most recent data for all values
        /// </summary>
        private void update()
        {
            WHUPP.Value = (int) sim.getStat(Stats.StatName.WATTAGE);
            DISTUP.Value = (decimal) sim.getStat(Stats.StatName.DISTANCE);
            HBUP.Value = (int) sim.getStat(Stats.StatName.HEARTBEAT);
            MUP.Value = (int) (sim.getStat(Stats.StatName.TIME)%60);
            HUP.Value = (int) (sim.getStat(Stats.StatName.TIME)/60);
            SPKUP.Value = (int) sim.getStat(Stats.StatName.SPEED);
            SPUP.Value = (int) sim.getStat(Stats.StatName.RPM);
            PWUP.Value = (int) sim.getStat(Stats.StatName.PROGRAMWATTAGE);
            EUP.Value = (int) sim.getStat(Stats.StatName.ENERGY);
        }


        private void SIM_GUI_Load_1(object sender, EventArgs e)
        {
        }
    }
}