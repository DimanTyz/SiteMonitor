using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace MonitorService
{
    public partial class Service : ServiceBase
    {
        FileInfo file;
        StreamWriter writer;

        public Service()
        {
            InitializeComponent();

            file = new FileInfo(@"D:/log.txt");
            writer = file.CreateText();
        }

        protected override void OnStart(string[] args)
        {
            writer.WriteLine("Microsoft status is " + SiteChecker.Check("https://www.microsoft.com")); 
            writer.WriteLine("Apple Status is " + SiteChecker.Check("https://www.apple.com"));
            writer.WriteLine("Google status is " + SiteChecker.Check("https://www.google.com"));
            writer.Flush();
                
            Task.Factory.StartNew(CheckGoogle);
            Task.Factory.StartNew(CheckApple);
            Task.Factory.StartNew(DelayForMIcrosoft);

        }

        protected override void OnStop()
        {
            writer.Close();
        }




        public void CheckGoogle()
        {
            TimeSpan span = new TimeSpan(0, 2, 0);
            while (true)
            {
                writer.WriteLineAsync("Google status is " + SiteChecker.Check("https://www.google.com"));
                Thread.Sleep(span);
                writer.Flush();
            }
        }
        public void CheckApple()
        {
            TimeSpan span = new TimeSpan(0, 5, 0);
            while (true)
            {
                writer.WriteLineAsync("Apple Status is " + SiteChecker.Check("https://www.apple.com"));
                Thread.Sleep(span);
                writer.Flush();
            }
        }

        private void DelayForMIcrosoft()
        {
            bool _ran = false;
            DateTime date = DateTime.Now;

            while (true)
            {
                if (DateTime.Now.Hour == 22 && DateTime.Now.Minute == 15 && _ran == false && DateTime.Now.Subtract(date).Days > 2)//проверка через два дня(пн проверить, вт не проверить, ср не проверить, чт проверить)
                {
                    date = DateTime.Now.Date;
                    _ran = true;
                    writer.WriteLineAsync("MIcrosoft status is " + SiteChecker.Check("https://www.microsoft.com"));
                    writer.Flush();
                }

                if (DateTime.Now.Hour != 22 && DateTime.Now.Minute != 15 && _ran == true)
                {
                    _ran = false;
                }
            }

        }
    }
}
