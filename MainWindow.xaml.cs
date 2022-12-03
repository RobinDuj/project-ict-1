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
        char dataToets;
        //byte[] dataToets = new byte[1];
        //serialport.writeline();
        bool i = true;
        char letter;
        int tijd = 0;
        Score score = new Score();
        RandomLetters randomLetters = new RandomLetters();
        System.Timers.Timer aTimer = new System.Timers.Timer();
        public MainWindow()
        {
            InitializeComponent();
            cbxPortName.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
            cbxPortName.Items.Add(s);

            aTimer.Elapsed += new ElapsedEventHandler(Timer_Tick);
            aTimer.Interval = 5000;
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
            serialPort.Close();
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

                }
            }

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (i)
            {
                score.Null();
                RandomLetters randomLetters = new RandomLetters();
                letter = randomLetters.GetLetter();
                lblletter.Content = $"Letter: {letter}";
                serialPort.WriteLine($"{letter}");
                i = false;
                txbx1.Clear();
                aTimer.Enabled = true;
                //inlezenCijfers();
            }
            
        }        
        private void Timer_Tick(object sender, EventArgs e)
        {
            inlezenCijfers();
        }
        private void inlezenCijfers()
        {
            char textInput = Convert.ToChar(txbx1.Text);

            if (textInput == letter)
            {
                score.Verhogen();
            }
            else if (tijd == 1)   //1
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
                gameOver();
            }
            tijd++;
        }
        private void gameOver()
        {

        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //string ch = ((Char)e.KeyCode).ToString();

            //if(e.Key == Key.j)
            //{

            //}
        }
    }
}
