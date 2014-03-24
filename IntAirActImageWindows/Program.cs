using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using IntAirAct;

namespace IntAirActImageWindows
{
    static class Program
    {
        private static TraceSource logger = new TraceSource("IntAirActImageWindows");
        private static IARequest request;
        private static IAIntAirAct intAirAct;
        private static Form1 form;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1();
            Application.Run(form);

            //ReceiverForm r_form = new ReceiverForm();
            //Application.Run(r_form);

            intAirAct = IAIntAirAct.New();
            /*
            intAirAct.Route(new IARoute("PUT", "/image"), delegate(IARequest req, IAResponse res)
            {
                logger.TraceEvent(TraceEventType.Information, 0, "Received request on {0}", req.Route);
                
                String url = req.BodyAsString();
                form.BeginInvoke((Action) delegate ()
                {
                    form.LoadImageFromURL(url);
                });
            });
             */
            intAirAct.Route(new IARoute("PUT", "/dictionary"), delegate(IARequest req, IAResponse res)
            {
                logger.TraceEvent(TraceEventType.Information, 0, "Received request on {0}", req.Route);


                // add to receiver form heres
                //ListBox r_listBox = r_form.getRListBox();
                ListBox r_listBox = form.getListBox2();

                String[] str_arr = req.BodyAs<String []>();

                foreach (string i in str_arr)
                {
                   r_listBox.Items.Add(i);

                }
                //String str = req.BodyAsString();
                Console.WriteLine("RECEIVED" + str_arr[0]);
            });

            
            intAirAct.Start();

            // handle devices that connect
            intAirAct.DeviceFound += new DeviceFoundHandler(intAirAct_DeviceFound);




            intAirAct.Stop();
        }

        static void intAirAct_DeviceFound(IADevice device, bool ownDevice)
        {
            if (ownDevice)
            {
                form.setIntAirAct(intAirAct);
                form.setDevice(device);
               //intAirAct.SendRequest(request, device);  
            }
        }
    }
}
