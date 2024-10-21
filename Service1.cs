using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Dota2_Blocker
{
    public partial class Service1 : ServiceBase
    {

        private Timer _timer;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _timer = new Timer(5000); // Check every 5 seconds
            _timer.Elapsed += OnElapsedTime;
            _timer.Start();
        }

        private void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("dota2"))
            {
                try
                {
                    process.Kill();
                    EventLog.WriteEntry("Dota2TerminatorService", "dota2.exe process terminated.", EventLogEntryType.Information);
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry("Dota2TerminatorService", $"Failed to terminate dota2.exe: {ex.Message}", EventLogEntryType.Error);
                }
            }
        }

        protected override void OnStop()
        {
            _timer.Stop();
            _timer.Dispose();
        }

    }
}
