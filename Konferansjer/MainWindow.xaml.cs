using MiscUtil.IO;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Konferansjer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<DataModel> commandlist = new ObservableCollection<DataModel>();
        /* 
         * Import funkcji PostMessage, potrzebnej do emulowania wciśniecia klawiszy w OBS 
         * Funkcja użyta do symulowania WM_KEYDOWN (eventu wciśnięcią klawisza klawiatury)
         * 
         * Niżej trochę constów które są w WinAPI i będa potrzebne
         */
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        const UInt32 WM_KEYDOWN = 0x0100;
        const int VK_F5 = 0x74;
        const int VK_F6 = 0x75;
        const int VK_F7 = 0x76;
        const int VK_F8 = 0x77;
        const int VK_F9 = 0x78;
        const int VK_F10 = 0x79;
        const int VK_F11 = 0x7A;
        const int VK_F12 = 0x7B;
 
        public string pressedButton;
        public bool slideshowMode = false;
        public MainWindow()
        {
            InitializeComponent();
            WebBrowserControlSample();
            CreateIfMissing("c:\\obs");
            // ChangeBecauseOfRadioButton();
            DisplayBar.IsChecked = true;
            LoadFileFromDisk();
    
        }

        public static String GetTimestamp(DateTime value)
        {
            //return value.ToString("yyyyMMddHHmmssffff");
            return ((Int32)(value.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();

        }

        private void commandsview_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Save_Click(sender, e);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public void WebBrowserControlSample()
        {
            InitializeComponent();
        }

        public void ChangeBecauseOfRadioButton(string name)
        {
            Dynamo.Children.Clear();
            System.Windows.Controls.Button newBtn = new Button();

            newBtn.Content = name;
            newBtn.Name = "Button";

            Dynamo.Children.Add(newBtn);
        }
        public void DisplayBarCreate() {
            Dynamo.Children.Clear();
            System.Windows.Controls.Label lbl = new Label();
            lbl.Content= "title";
            
            System.Windows.Controls.TextBox title = new TextBox();
            title.Text = "text";
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // ... Get RadioButton reference.
            var button = sender as RadioButton;

            // ... Display button content as title.
            pressedButton = button.Name.ToString();
            switch (pressedButton) { 
                case "DisplayBar":
                    n1.Content = "Title";
                    n2.Content = "Author";
                    n3.Content = "Photo";
                    n4.Content = "Delay 1";
                    n5.Content = "Delay 2";
                    
                    m1.Content = "";
                    m2.Content = "";
                    m3.Content = "";
                    m4.Content = "";
                    m5.Content = "";

                    a1.IsEnabled = true;
                    a2.IsEnabled = true;
                    a3.IsEnabled = true;
                    a4.IsEnabled = true;
                    a5.IsEnabled = true;

                    b1.IsEnabled = false;
                    b2.IsEnabled = false;
                    b3.IsEnabled = false;
                    b4.IsEnabled = false;
                    b5.IsEnabled = false;

                    p1.IsEnabled = false;
                    p2.IsEnabled = false;
                    p3.IsEnabled = true;
                    p4.IsEnabled = false;
                    p5.IsEnabled = false;

                    break;
                case "ShowFullScreenBanner":
                    n1.Content = "Banner";
                    n2.Content = "";
                    n3.Content = "";
                    n4.Content = "";
                    n5.Content = "";
                    
                    m1.Content = "Delay 1";
                    m2.Content = "Delay 2";
                    m3.Content = "";
                    m4.Content = "";
                    m5.Content = "";

                    a1.IsEnabled = true;
                    a2.IsEnabled = false;
                    a3.IsEnabled = false;
                    a4.IsEnabled = false;
                    a5.IsEnabled = false;

                    b1.IsEnabled = true;
                    b2.IsEnabled = true;
                    b3.IsEnabled = false;
                    b4.IsEnabled = false;
                    b5.IsEnabled = false;

                    p1.IsEnabled = true;
                    p2.IsEnabled = false;
                    p3.IsEnabled = false;
                    p4.IsEnabled = false;
                    p5.IsEnabled = false;
                    break;
                case "ShowFullScreenSlider":
                    n1.Content = "1";
                    n2.Content = "2";
                    n3.Content = "3";
                    n4.Content = "4";
                    n5.Content = "5";
                    
                    m1.Content = "Delay 1";
                    m2.Content = "Delay 2";
                    m3.Content = "Delay 3";
                    m4.Content = "Delay 4";
                    m5.Content = "Delay 5";

                    a1.IsEnabled = true;
                    a2.IsEnabled = true;
                    a3.IsEnabled = true;
                    a4.IsEnabled = true;
                    a5.IsEnabled = true;

                    b1.IsEnabled = true;
                    b2.IsEnabled = true;
                    b3.IsEnabled = true;
                    b4.IsEnabled = true;
                    b5.IsEnabled = true;

                    p1.IsEnabled = true;
                    p2.IsEnabled = true;
                    p3.IsEnabled = true;
                    p4.IsEnabled = true;
                    p5.IsEnabled = true;
                    break;
                case "StopFullScreen":
                    n1.Content = "Delay";
                    n2.Content = "";
                    n3.Content = "";
                    n4.Content = "";
                    n5.Content = "";
                    
                    m1.Content = "";
                    m2.Content = "";
                    m3.Content = "";
                    m4.Content = "";
                    m5.Content = "";

                    a1.IsEnabled = true;
                    a2.IsEnabled = false;
                    a3.IsEnabled = false;
                    a4.IsEnabled = false;
                    a5.IsEnabled = false;

                    b1.IsEnabled = false;
                    b2.IsEnabled = false;
                    b3.IsEnabled = false;
                    b4.IsEnabled = false;
                    b5.IsEnabled = false;

                    p1.IsEnabled = false;
                    p2.IsEnabled = false;
                    p3.IsEnabled = false;
                    p4.IsEnabled = false;
                    p5.IsEnabled = false;
                    break;
                default:
                    break;
            }
        }

        private void addphoto_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                var button = sender as Button;
                string pressedButton = button.Name.ToString();
                switch (pressedButton)
                {
                    case "p1":
                        a1.Text = filename;
                        break;
                    case "p2":
                        a2.Text = filename;
                        break;
                    case "p3":
                        a3.Text = filename;
                        break;
                    case "p4":
                        a4.Text = filename;
                        break;
                    case "p5":
                        a5.Text = filename;
                        break;
                    default:
                        break;
                }
                
                
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            bool did = false;
            //System.IO.File.WriteAllText(@"G:\here\wat.txt", "japierdole");
            foreach (DataModel datm in commandlist) {
                if (datm.title == t1.Text&&did==false){
                    datm.title = t1.Text;
                    datm.s1 = a1.Text;
                    datm.s2 = a2.Text;
                    datm.s3 = a3.Text;
                    datm.s4 = a4.Text;
                    datm.s5 = a5.Text;

                    datm.d1 = b1.Text;
                    datm.d2 = b2.Text;
                    datm.d3 = b3.Text;
                    datm.d4 = b4.Text;
                    datm.d5 = b5.Text;
                    did = true;
                    break;
                }
            }
            if (did == false)
            {
                DataModel to = new DataModel();
                to.title = t1.Text;
                to.s1 = a1.Text;
                to.s2 = a2.Text;
                to.s3 = a3.Text;
                to.s4 = a4.Text;
                to.s5 = a5.Text;

                to.d1 = b1.Text;
                to.d2 = b2.Text;
                to.d3 = b3.Text;
                to.d4 = b4.Text;
                to.d5 = b5.Text;
                var button = sender as RadioButton;
                to.command = pressedButton;
 
                commandlist.Add(to);
                commandsview.ItemsSource = commandlist;
                prelectionView.ItemsSource = commandlist;

            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            DataModel o = (DataModel)commandsview.SelectedItem;
            foreach (DataModel datm in commandlist)
            {
                if (o.title == datm.title) { 
                commandlist.Remove(datm);
                break;
                }
            }
            commandsview.ItemsSource = commandlist;
            prelectionView.ItemsSource = commandlist;
        }
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            DataModel to = new DataModel();
            object o = commandsview.SelectedItem;
            to = o as DataModel;

            if(to.command != null)
                switch (to.command) {
                    case "DisplayBar":
                        DisplayBar.IsChecked = true;
                        break;
                    case "ShowFullScreenBanner":
                        ShowFullScreenBanner.IsChecked=true;
                        break;
                    case "ShowFullScreenSlider":
                        ShowFullScreenSlider.IsChecked = true;
                        break;
                    default:
                        break;
                }
 
            t1.Text = to.title;
            a1.Text = to.s1;
            a2.Text = to.s2;
            a3.Text = to.s3;
            a4.Text = to.s4;
            a5.Text = to.s5;

            b1.Text = to.d1;
            b2.Text = to.d2;
            b3.Text = to.d3;
            b4.Text = to.d4;
            b5.Text = to.d5;
        }

        private void commandsview_Click(object sender, MouseButtonEventArgs e) { 
            DataModel to = new DataModel();
            object o = commandsview.SelectedItem;
            to = o as DataModel;

            if (to == null)
                return;

            switch (to.command)
            {
                case "DisplayBar":
                    DisplayBar.IsChecked = true;
                    break;
                case "ShowFullScreenBanner":
                    ShowFullScreenBanner.IsChecked = true;
                    break;
                case "ShowFullScreenSlider":
                    ShowFullScreenSlider.IsChecked = true;
                    break;
                default:
                    break;
            }

            t1.Text = to.title;
            a1.Text = to.s1;
            a2.Text = to.s2;
            a3.Text = to.s3;
            a4.Text = to.s4;
            a5.Text = to.s5;

            b1.Text = to.d1;
            b2.Text = to.d2;
            b3.Text = to.d3;
            b4.Text = to.d4;
            b5.Text = to.d5;
        }

        private void Display_Click(object sender, RoutedEventArgs e) {
            //System.IO.File.WriteAllText(@"G:\here\wat.txt", "japierdole");
            var button = sender as RadioButton;
            //pressedButton = button.Name.ToString();
            string serializedResult;
            switch (pressedButton) {
                case "DisplayBar":
                    serializedResult = "{" + "\"id\" :" + GetTimestamp(DateTime.UtcNow) +"," + "\"command\" : \"displayBar\" ," + "\"param1\" : {\"title\": "+  "\"" + a1.Text + "\"" + "," + "\"author\":"+ "\""+ a2.Text+"\"" +"," + "\"photo\":" +"\"" +a3.Text+"\"}," + "\"param2\":" +  a4.Text+ ","+ "\"param3\":" + a5.Text + "}";
                    System.IO.File.WriteAllText(@"C:\obs\command.json", serializedResult);
                    break;
                case "ShowFullScreenBanner":
                    serializedResult = "{" + "\"id\" :" + GetTimestamp(DateTime.UtcNow) + "," + "\"command\" : \"showFullscreenBanner\" ," + "\"param1\":" + "\"" + a1.Text + "\"," + "\"param2\"" + "\"" + b1.Text + "\"," + "\"param3\"" + "\"" + b2.Text + "\"," + "}";
                    System.IO.File.WriteAllText(@"C:\obs\command.json", serializedResult);
                    break;
                case "ShowFullScreenSlider":
                    serializedResult = "{" + "\"id\" :" + GetTimestamp(DateTime.UtcNow) + "," + "\"command\" : \"showFullscreenSlider\" ," + " \"param1\" : [{\"file\": " + "\"" + a1.Text + "\", \"showtime\": " + b1.Text + "}," + "{\"file\": " + "\"" + a2.Text + "\", \"showtime\": " + b2.Text + "}," + "{\"file\": " + "\"" + a3.Text + "\", \"showtime\": " + b3.Text + "}," + "{\"file\": " + "\"" + a4.Text + "\", \"showtime\": " + b4.Text + "}," + "{\"file\": " + "\"" + a5.Text + "\", \"showtime\": " + b5.Text + "}," + "\"param2\" : 1000" + "}";
                    System.IO.File.WriteAllText(@"C:\obs\command.json", serializedResult);
                    break;
                case "StopFullScreen":
                    serializedResult = "{" + "\"id\" :" + GetTimestamp(DateTime.UtcNow) + "," + "\"command\" : \"stopFullscreen\"," + "\"param1\"" + a1.Text + "}";
                    System.IO.File.WriteAllText(@"C:\obs\command.json", serializedResult);
                    
                    break;
                default:
                    break;
            }
          
        }

        private void Button_Display_Title_Click(object sender, RoutedEventArgs e)
        {
            DataModel data = (DataModel)prelectionView.SelectedValue;
            if (data != null)
            {
                if (slideshowMode)
                    slideshowMode = false;
                string serializedResult = "{" + "\"id\" :" + GetTimestamp(DateTime.UtcNow) +"," + "\"command\" : \"displayBar\" ," + "\"param1\" : {\"title\": " + "\"" + data.s1 + "\"" + "," + "\"author\":" + "\"" + data.s2 + "\"" + "," + "\"photo\":" + "\"" + data.s3 + "\"}," + "\"param2\":" + data.s4 + "," + "\"param3\":" + data.s5 + "}";
                System.IO.File.WriteAllText(@"C:\obs\command.json", serializedResult);
            }
        }

         private void Button_Toggle_Slideshow_Click(object sender, RoutedEventArgs e)
        {
            string command;
            if (!slideshowMode) {
                command = "{\"id\" : " + GetTimestamp(DateTime.UtcNow) + ",\"command\" : \"showFullscreenSlider\",\"param1\" : [{\"file\": \"przerywniki/1.png\", \"showtime\": 5000},{\"file\": \"przerywniki/2.png\", \"showtime\": 5000},{\"file\": \"przerywniki/3.png\", \"showtime\": 5000},{\"file\": \"przerywniki/4.png\", \"showtime\": 7500}],\"param2\" : 1000}";
                slideshowMode = true;
            }
            else
            {
                command = "{\"id\" : " + GetTimestamp(DateTime.UtcNow) + ",\"command\" : \"stopFullscreen\",\"param1\" : 1000}";
                slideshowMode = false;
            }
            System.IO.File.WriteAllText(@"C:\obs\command.json", command);

        }
        
        private void SaveFile(object sender, RoutedEventArgs e)
        {
            string textToSave = "";
            foreach (DataModel dataM in commandlist)
            {
                textToSave += dataM.title + "|" + dataM.command + "|" + dataM.d1 + "|" + dataM.d2 + "|" + dataM.d3 + "|" + dataM.d4 + "|" + dataM.d5 + "|" + dataM.s1 + "|" + dataM.s2 + "|" + dataM.s3 + "|" + dataM.s4 + "|" + dataM.s5 + "|" + Environment.NewLine;
            }
            System.IO.File.WriteAllText(@"C:\obs\savedones.txt", textToSave);
        }

        private void Load_file(object sender, RoutedEventArgs e)
        {
            LoadFileFromDisk();
        }

        protected void LoadFileFromDisk()
        {
            try
            {
                StreamReader streamReader = new StreamReader(@"C:\obs\savedones.txt");
                string text = streamReader.ReadToEnd();
                streamReader.Close();

                foreach (string line in new LineReader(() => new StringReader(text)))
                {

                    string inputS = line;

                    string[] words = inputS.Split('|');
                    int val = 0;
                    DataModel to = new DataModel();
                    foreach (string s in words)
                    {

                        switch (val)
                        {
                            case 0:
                                to.title = s;
                                break;
                            case 1:
                                to.command = s;
                                break;
                            case 2:
                                to.d1 = s;
                                break;
                            case 3:
                                to.d2 = s;
                                break;
                            case 4:
                                to.d3 = s;
                                break;
                            case 5:
                                to.d4 = s;
                                break;
                            case 6:
                                to.d5 = s;
                                break;
                            case 7:
                                to.s1 = s;
                                break;
                            case 8:
                                to.s2 = s;
                                break;
                            case 9:
                                to.s3 = s;
                                break;
                            case 10:
                                to.s4 = s;
                                break;
                            case 11:
                                to.s5 = s;
                                break;
                        }

                        val++;
                    }
                    commandlist.Add(to);

                }
                commandsview.ItemsSource = commandlist;
                prelectionView.ItemsSource = commandlist;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Brak pliku z danymi, zostanie utworzony nowy.", "Informacja");
            }
        }

        private void CreateIfMissing(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SendHotkey(VK_F5);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SendHotkey(VK_F6);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SendHotkey(VK_F7);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SendHotkey(VK_F8);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            SendHotkey(VK_F11);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            SendHotkey(VK_F12);
        }

        private void SendHotkey(int key, String name = "obs64")
        {
            Process[] processes = Process.GetProcessesByName(name);
            foreach (Process proc in processes)
                PostMessage(proc.MainWindowHandle, WM_KEYDOWN, key, 0);
        }
    }    
}
