using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Interop;
using System.Windows.Threading;

namespace Operating_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        //GLOBAL VARIABLES
        Scheduling.Process[] _process = new Scheduling.Process[10];
        int _ProcessCounter = 0;
        DispatcherTimer _Timer = new DispatcherTimer();
        SecondaryWindow window = new SecondaryWindow();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _Timer.Tick += new EventHandler(_Timer_Tick);
            _Timer.Interval = new TimeSpan(0, 0, 0, 0, 200);

            SetGraphicsForProcesses();
        }

        private async void _Timer_Tick(object sender, EventArgs e)
        {
            await SetSecondaryWindowPosition();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (tbxArrival.Text == "" || tbxBurst.Text == "" || tbxDeadline.Text == "")
            {
                ShowMessage("Warning", "I Can't Accept Null Values :(");
                return;
            }

            if (_ProcessCounter <= 9)
            {
                if (_ProcessCounter == 0 && tbxArrival.Text != "0")
                {
                    ShowMessage("Warning", "First Process Arrival Should be ZERO !");
                    return;
                }
                _process[_ProcessCounter].Name = comboBox.Text;
                _process[_ProcessCounter].ArrivalTime = Convert.ToInt32(tbxArrival.Text);
                _process[_ProcessCounter].BurstTime = Convert.ToInt32(tbxBurst.Text);
                _process[_ProcessCounter].Deadline = Convert.ToInt32(tbxDeadline.Text);

                AddProcessesToTextBlocks();

                _ProcessCounter++;

                comboBox.SelectedIndex++;
                tbxArrival.Clear();
                tbxBurst.Clear();
                tbxDeadline.Clear();
            }
            else
            {
                ShowMessage("Warning", "Max Processes Already Entered");
            }

            tbxArrival.Focus();

        }

        private async void ShowMessage(string Title, string Message)
        {
            await this.ShowMessageAsync(Title, Message);
        }

        private void AddProcessesToTextBlocks()
        {
            tblock_Name.Text += "\n" + _process[_ProcessCounter].Name;
            tblock_Arrival.Text += "\n" + _process[_ProcessCounter].ArrivalTime.ToString();
            tblock_Burst.Text += "\n" + _process[_ProcessCounter].BurstTime.ToString();
            tblock_Deadline.Text += "\n" + _process[_ProcessCounter].Deadline.ToString();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            _process[0].TotalProcess = _ProcessCounter;

            Scheduling ScheduleHelper = new Scheduling();

            var x = ScheduleHelper.CalculateTimeLine(_process);

            for (int i = 0; i < _process.Length; i++)
            {
                _process[i].isFinished = false;
            }


            if (!window.IsActive)
            {
                window.Show();
            }
            var sg = new ScheduleGraphic();
            sg.DrawScheduleGraphic(window, x, _ProcessCounter);
            _Timer.Start();
            tbxArrival.Focus();

            //_Timer.Start();
            //window.Left = this.Left + this.ActualWidth;
            //window.Top = this.Top;
            //window.Show();
        }

        private void findhandle()
        {
            IntPtr windowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
        }

        private async Task SetSecondaryWindowPosition()
        {
            if (window.Left != this.Left - ((window.Width / 2) - (this.Width / 2)) && window.Top != this.Top + this.Height + 1)
            {
                window.Left = this.Left - ((window.Width / 2) - (this.Width / 2));
                window.Top = this.Top + this.Height;
            }

        }

        private void SetGraphicsForProcesses()
        {
            _process[0].Color = Brushes.LimeGreen;
            _process[1].Color = Brushes.IndianRed;
            _process[2].Color = Brushes.DarkViolet;
            _process[3].Color = Brushes.Brown;
            _process[4].Color = Brushes.HotPink;
            _process[5].Color = Brushes.Teal;
            _process[6].Color = Brushes.Aquamarine;
            _process[7].Color = Brushes.Orange;
            _process[8].Color = Brushes.Yellow;
            _process[9].Color = Brushes.Peru;


        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
