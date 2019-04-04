using System;
using Newtonsoft.Json;

namespace VRController
{
    public class VRPanel
    {
        private readonly VRConnector vr;

        public VRPanel(VRConnector vr)
        {
            this.vr = vr;
        }

        /// <summary>
        ///     Clear the panel
        /// </summary>
        /// <param name="id"></param>
        public void clearPanel(string id)
        {
            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data = new {dest = vr.tunnelID, data = new {id = "scene/panel/clear", data = new {id}}}
                };

            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }

        /// <summary>
        ///     Draw a line on the pane
        /// </summary>
        /// <param name="node">string node : UUID of the node</param>
        /// <param name="width">int width : the line width</param>
        /// <param name="x1">int x1 : start posistion x</param>
        /// <param name="y1">int y1 : statt posistion y</param>
        /// <param name="x2">int x2 : end posistion x</param>
        /// <param name="y2">int y2 : end posistion y</param>
        /// <param name="r">int r : RGB color red value[0-256]</param>
        /// <param name="g">int g : RGB color green value[0-256]</param>
        /// <param name="b">int b : RGB color blue value[0-256]</param>
        /// <param name="a">int a : alpha(transparace) value[0-1]</param>
        public void drawLine(
            string node,
            int width,
            int x1,
            int y1,
            int x2,
            int y2,
            int r,
            int g,
            int b,
            int a)
        {
            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data =
                    new
                    {
                        dest = vr.tunnelID,
                        data =
                        new
                        {
                            id = "scene/panel/drawlines",
                            data =
                            new
                            {
                                id = node,
                                width,
                                lines = new[] {x1, y1, x2, y2, r, g, b, a, x1, y1, x2, y2, r, g, b, a}
                            }
                        }
                    }
                };

            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
            swapPanel(node);
        }

        /// <summary>
        ///     draw a speedometer on a node
        /// </summary>
        /// <param name="node">string node : UUID</param>
        /// <param name="speed">double speed</param>
        public void drawSpeedometer(string node, double speed)
        {
            var hoek = speed/360;
            drawLine(node, 1, 0, 0, 0, 50, 0, 0, 0, 1);
            drawLine(node, 1, 0, 0, 50, 0, 0, 0, 0, 1);
            drawLine(node, 1, 0, 50, 50, 50, 0, 0, 0, 1);
            drawLine(node, 1, 50, 0, 50, 50, 0, 0, 0, 1);

            swapPanel(node);
        }

        /// <summary>
        ///     Swap panel to specified panel
        ///     Needs to be done evrytime after soemthing has changes on a panel
        /// </summary>
        /// <param name="nodeID">string node : UUID</param>
        public void swapPanel(string nodeID)
        {
            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data =
                    new
                    {
                        dest = vr.tunnelID,
                        data =
                        new
                        {
                            id = "scene/panel/swap",
                            data =
                            new
                            {
                                id = nodeID
                            }
                        }
                    }
                };
            Console.WriteLine("SWAPPING " + nodeID);
            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }

        /// <summary>
        ///     draw text on pane
        /// </summary>
        /// <param name="nodeName">string nodename</param>
        /// <param name="text">string text</param>
        /// <param name="x">int x : postion of x start</param>
        /// <param name="y">int y : position of y start</param>
        /// <param name="size">int size : text size</param>
        /// <param name="r">int r : RGB color red value[0-256]</param>
        /// <param name="g">int g : RGB color green value[0-256]</param>
        /// <param name="b">int b : RGB color blue value[0-256]</param>
        /// <param name="a">int a : alpha(transparace) value[0-1]</param>
        /// <param name="font">string font</param>
        public void drawTextonPanel(
            string nodeName,
            string text,
            double x,
            double y,
            double size,
            int r,
            int g,
            int b,
            int a,
            string font)
        {
            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data =
                    new
                    {
                        dest = vr.tunnelID,
                        data =
                        new
                        {
                            id = "scene/panel/drawtext",
                            data =
                            new
                            {
                                id = nodeName,
                                text,
                                position = new[] {x, y},
                                size,
                                color = new[] {r, g, b, a},
                                font
                            }
                        }
                    }
                };

            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
            //swapPanel(nodeName);
        }


        /// <summary>
        ///     create a new pannel
        /// </summary>
        /// <param name="name">string name : not UUID</param>
        /// <param name="parent">string parent</param>
        /// <param name="position">int[3] position : x, y, z</param>
        /// <param name="scale">float scale</param>
        /// <param name="rotation">int[3] rotation : x, y, z</param>
        /// <param name="size">int[2] size</param>
        /// <param name="resolution">int[2] resolution ; x, y</param>
        /// <param name="background">int[4] background</param>
        public void addPanel(string name, string parent, int[] position, float scale, int[] rotation, int[] size,
            int[] resolution, int[] background)
        {
            dynamic panelpacket = null;


            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data = new
                    {
                        dest = vr.tunnelID,
                        data = new
                        {
                            id = "scene/node/add",
                            data = new
                            {
                                name,
                                parent,
                                components = new
                                {
                                    transform = new
                                    {
                                        position,
                                        scale,
                                        rotation
                                    },
                                    panel = new
                                    {
                                        size,
                                        resolution,
                                        background
                                    }
                                }
                            }
                        }
                    }
                };
            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }

        /// <summary>
        ///     add an emergency panel
        /// </summary>
        /// <param name="name">string name : not UUID</param>
        /// <param name="position">int[3] position : x, y, z</param>
        /// <param name="scale">float scale</param>
        /// <param name="rotation">int[3] rotation : x, y, z</param>
        /// <param name="size">int[2] size</param>
        /// <param name="resolution">int[2] resolution ; x, y</param>
        /// <param name="background">int[4] background</param>
        public void addEmergencyPanel(string name, int[] position, float scale, int[] rotation, int[] size,
            int[] resolution, int[] background)
        {
            dynamic panelpacket = null;


            dynamic packet =
                new
                {
                    id = "tunnel/send",
                    data = new
                    {
                        dest = vr.tunnelID,
                        data = new
                        {
                            id = "scene/node/add",
                            data = new
                            {
                                name,
                                components = new
                                {
                                    transform = new
                                    {
                                        position,
                                        scale,
                                        rotation
                                    },
                                    panel = new
                                    {
                                        size,
                                        resolution,
                                        background
                                    }
                                }
                            }
                        }
                    }
                };
            string packetString = JsonConvert.SerializeObject(packet);
            vr.sendData(packetString);
            vr.dataChecker();
        }
    }
}