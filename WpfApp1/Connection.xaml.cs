using AdminPanel.NetworkMiddleware;
using System.Threading;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

namespace AdminPanel
{
    /// <summary>
    /// Interaction logic for Connection.xaml
    /// </summary>
    public partial class Connection : Window
    {
        private ApplicationMemory.MemoryBuild _memory;
        private Client _networkClient;

        private Timer timer;
        private bool threadStart = false;

        public delegate void StartLoginSession(bool start);
        public event StartLoginSession Start;

        private int AmountSteps = 100;

        public Connection()
        {
            InitializeComponent();
            Start += StartNewLoginSession;
            this._networkClient = new Client();
            this._networkClient.GetExceptionOutput += Client_GetExceptionOutput;

            this._memory = new ApplicationMemory.MemoryBuild();

            if (!threadStart)
            {
                var timerConnected = new Thread(new ThreadStart(TestConnectionHandler));
                timerConnected.SetApartmentState(ApartmentState.STA);
                this.Dispatcher.Invoke(() => timerConnected.Start());
                threadStart = true;
            }
        }

        private void Client_GetExceptionOutput(string message)
        {
            this.Dispatcher.Invoke(() => this.ErrorConnected.Visibility = Visibility.Visible);
            this.Dispatcher.Invoke(() => this.ConnectionStart.Visibility = Visibility.Hidden);
            this.Dispatcher.Invoke(() => this.ReconnectionTimeOut.Visibility = Visibility.Visible); 
        }

        private void StartNewLoginSession(bool start)
        {
            if(start)
            {
                var Login = new LoginPage();
                this._memory.AddToHistory("LoginPage");
                Login.SetMemoryDump(this._memory);
                Login.Show();
                this.Close();
            }

        }

        private void TestConnectionHandler()
        {
            Timer timer = (Timer)this.Resources["localTimer"];

            var minute = 10;

            while (true)
            {
                if(AmountSteps == 0)
                {
                    this.Dispatcher.Invoke(() => this.ReconnectionTimeOut.Visibility = Visibility.Hidden);
                    this.Dispatcher.Invoke(() => this.ConnectionStart.Visibility = Visibility.Hidden);
                    this.Dispatcher.Invoke(() => this.progressBar.Visibility = Visibility.Hidden);
                    this.Dispatcher.Invoke(() => this.ErrorConnected.Visibility = Visibility.Visible);
                    break;

                }
                this.Dispatcher.Invoke(() => this.ReconnectionTimeOut.Visibility = Visibility.Hidden);
                this.Dispatcher.Invoke(() => this.ConnectionStart.Visibility = Visibility.Visible);

                if (this._networkClient.RequestHandle(NetworkMiddleware.NetworkSignal.TestConnection_action.ActionType))
                {
                    break;
                }


                timer.TimeOutP = Timer.prefix + $" {minute}" + Timer.postfix;
                
                this.Dispatcher.Invoke(() => this.ConnectionStart.Visibility = Visibility.Hidden);
                this.Dispatcher.Invoke(() => this.ReconnectionTimeOut.Visibility = Visibility.Visible);

                minute = 10;
                while(minute != 0)
                {
                    Thread.Sleep(1000);
                    minute -= 1;
                    timer.TimeOutP = Timer.prefix + $" {minute}" + Timer.postfix;
                }

                AmountSteps -= 1;
            }

            if (AmountSteps != 0)
            {
                this.Dispatcher.Invoke(() => Start(true));                
            }
            else
            {
                this.Dispatcher.Invoke(() => Start(false));
            }

            this.threadStart = false;

        }

    }
}
