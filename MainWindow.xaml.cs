using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO.Ports;

namespace project_ict_thomas_is_git
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //SerialPort _serialPort;
        //byte[] _data;
        //const int START_ADDRESS = 80;
        //const int NUMBER_OF_DMX_BYTES = 513;
        //DispatcherTimer _dispatcherTimer;
        public MainWindow()
        {
            InitializeComponent();

            //cbxPortName.Items.Add("None");
            //foreach (string s in SerialPort.GetPortNames())
            //    cbxPortName.Items.Add(s);

            //_serialPort = new SerialPort();
            //_serialPort.BaudRate = 250000;
            //_serialPort.StopBits = StopBits.Two;

            //_data = new byte[NUMBER_OF_DMX_BYTES];

            //_dispatcherTimer = new DispatcherTimer();
            //_dispatcherTimer.Interval = TimeSpan.FromSeconds(0.1);
            //_dispatcherTimer.Tick += _dispatcherTimer_Tick;
            //_dispatcherTimer.Start();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void cbxPortName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            RandomLetters randomLetters = new RandomLetters();
            lblletter.Content = $"Letter: {randomLetters.GetLetter()}";

        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
