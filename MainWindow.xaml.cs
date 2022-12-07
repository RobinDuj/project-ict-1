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
using System.Diagnostics;
using System.Timers;
using static System.Formats.Asn1.AsnWriter;
using System.Threading;

namespace project_ict_thomas_is_git
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort serialPort = new SerialPort();
        //char dataToets;
        bool i = true;
        bool spelBezig = false;
        char letter;
        int tijd = 0;
        int tijdTimer;
        int timerSpel;
        Score score = new Score();
        RandomLetters randomLetters = new RandomLetters();
        System.Timers.Timer aTimer = new System.Timers.Timer();
        public MainWindow()
        {
            InitializeComponent();
            cbxPortName.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
            cbxPortName.Items.Add(s);
            tijdTimer = 400;
            aTimer.Elapsed += new ElapsedEventHandler(Timer_Tick);
            aTimer.Interval = tijdTimer;
            //aTimer.Enabled = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            serialPort.WriteLine("Daaaag");
            var milliseconds = 200;
            Thread.Sleep(milliseconds);
            serialPort.WriteLine("aaaag");
            Thread.Sleep(milliseconds);
            serialPort.WriteLine("aaag");
            Thread.Sleep(milliseconds);
            serialPort.WriteLine("aaag");
            Thread.Sleep(milliseconds);
            serialPort.WriteLine("aag");
            Thread.Sleep(milliseconds);
            serialPort.WriteLine("ag");
            Thread.Sleep(milliseconds);
            serialPort.WriteLine("g");
            Thread.Sleep(milliseconds);
            serialPort.WriteLine("");
            serialPort.Dispose();
        }

        private void cbxPortName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (serialPort != null)
            {
                if (serialPort.IsOpen)
                    serialPort.Close();

                if (cbxPortName.SelectedItem.ToString() != "None")
                {
                    serialPort.PortName = cbxPortName.SelectedItem.ToString();
                    serialPort.Open();
                }
                else
                {
                    MessageBox.Show("Kies een COM-poort.", "Fout",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if ((i) && (serialPort != null) && (serialPort.IsOpen))
            {
                serialPort.WriteLine("Welkom");
                score.Null();
                letter = randomLetters.GetLetter();
                lblletter.Content = $"Letter: {letter}";
                serialPort.WriteLine($"{letter}");
                i = false;
                spelBezig = true;
                tijdTimer = 400;
                aTimer.Enabled = true;
                lblletter.Content = $"Letter: {letter}";
                lblScore.Content = "Score:";
            }
            
        }        
        private void Timer_Tick(object sender, EventArgs e)
        {
           if(spelBezig)
            inlezenCijfers();
            //if (spelBezig)
            //    timerSpel++;
        }
        char textInput;
        private void inlezenCijfers()
        {
            if (textInput == letter)
            {
                score.Verhogen();
                VolgendCijferAsync();
            }
            else if (tijd == 1)  //1
            {
                serialPort.WriteLine($" {letter}");
            }
            else if (tijd == 2)   //2
            {
                serialPort.WriteLine($"  {letter}");
            }
            else if (tijd == 3)   //3
            {
                serialPort.WriteLine($"  {letter}");
            }
            else if (tijd == 4)   //4
            {
                serialPort.WriteLine($"   {letter}");
            }
            else if (tijd == 5)  //5
            {
                serialPort.WriteLine($"     {letter}");
            }
            else if (tijd == 6)  //6
            {
                serialPort.WriteLine($"      {letter}");
            }
            else if (tijd == 7)  //7
            {
                serialPort.WriteLine($"       {letter}");
            }
            else if (tijd == 8)  //8
            {
                serialPort.WriteLine($"        {letter}");
            }
            else if (tijd == 9)  //9
            {
                serialPort.WriteLine($"         {letter}");
            }
            else if (tijd == 10)  //10
            {
                serialPort.WriteLine($"          {letter}");
            }
            else if (tijd == 11)  //11
            {
                serialPort.WriteLine($"           {letter}");
            }
            else if (tijd == 12)  //12
            {
                serialPort.WriteLine($"            {letter}");
            }
            else if (tijd == 13)  //13
            {
                serialPort.WriteLine($"             {letter}");
            }
            else if (tijd == 14)  //14
            {
                serialPort.WriteLine($"              {letter}");
            }
            else if (tijd == 15)  //15
            {
                serialPort.WriteLine($"               {letter}");
            }
            else if (tijd > 15)
            {
                gameOverAsync();
            }
            tijd++;
        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if ((serialPort != null) && (serialPort.IsOpen))
            {
                serialPort.WriteLine("Gestopt");
                i = true;
                spelBezig = false;
                tijd = 0;
                lblletter.Content = "Letter:";
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            textInput = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            textInput = char.ToLower(textInput);

            if ((textInput != letter) && (tijd > 0))
            {
                gameOverAsync();
            }
            inlezenCijfers();
        }
        private async Task gameOverAsync()
        {
            serialPort.WriteLine($"Score: {score.Show()}");
            i = true;
            spelBezig = false;
            tijd = 0;
            await Task.Run(() => this.Dispatcher.Invoke(() => { lblletter.Content = "Game Over"; }));
            tijdTimer = 400;
        }
        private async Task VolgendCijferAsync()
        {
            if(tijdTimer <= 20)
            {
                tijdTimer = 20;
                aTimer.Interval = tijdTimer;
            }
            else
            {
                tijdTimer = tijdTimer - 10;
                aTimer.Interval = tijdTimer;
            }
            tijd = 0;
            letter = randomLetters.GetLetter();
            await Task.Run(() => this.Dispatcher.Invoke(() => { lblScore.Content = $"Score: {score.Show()}"; }));
            //lblScore.Content = $"Score: {score.Show()}";
            lblletter.Content = $"Letter: {letter}";
            serialPort.WriteLine($"{letter}");
        }
    }
}
