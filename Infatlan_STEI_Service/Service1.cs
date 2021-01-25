using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Infatlan_STEI_Service.Classes;
using System.Timers;


namespace Infatlan_STEI_Service
{
    public partial class Service1 : ServiceBase
    {
        Timer vTimer = new Timer();
        public Service1(){
            InitializeComponent();
        }

        protected override void OnStart(string[] args){
            try{
                WriteToFile("Starting Service {0}");

                vTimer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
                vTimer.Interval = 300000;
                vTimer.Enabled = true;
            }catch (Exception ex){
                throw new Exception(ex.Message);
            }
        }

        protected override void OnStop(){
            vTimer.Enabled = false;
            this.WriteToFile("Stopping Service {0}");
        }

        private void WriteToFile(string text){
            string path = "C:\\Services\\WServiceLog" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            using (StreamWriter writer = new StreamWriter(path, true)){
                writer.WriteLine(string.Format("{0} " + text , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                writer.Close();
            }
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e){
            if (DateTime.Now.Hour == 4 && DateTime.Now.Minute < 10){
                SapConnector vSap = new SapConnector();
                String vFechaInicio = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"), vFechaFin = DateTime.Today.ToString("yyyy-MM-dd");
                String vResultado = vSap.getInformacion(vFechaInicio, vFechaFin);
            }
        }
    }
}
