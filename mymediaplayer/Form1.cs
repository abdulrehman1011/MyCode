using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace mymediaplayer
{
    public partial class Form1 : Form
    {
        WMPLib.WindowsMediaPlayer Player = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer Player1 = new WMPLib.WindowsMediaPlayer();
        List<string> strArr;
        int len = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "mp3,wav,mp4,mov,wmv,mpg|*.mp3;*.wav;*.mp4;*.mov;*.wmv;*.mpg|all files|*.*";
            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory.ToString();
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                //axWindowsMediaPlayer1.URL = openFileDialog1.FileName;
                Player.URL = openFileDialog1.FileName;
                Player.settings.volume = 70;
                Player.controls.play();
                
            }
                
           
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            Player.settings.volume = trackBar1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }

        private void btnPlayPromt_Click(object sender, EventArgs e)
        {
            try
            {
                
                //C:\Users\Abdul Rehman\Desktop\Apollo\Demo WMV+TTS\mymediaplayer\bin\Debug\WAV files\0.wav
                string fl = txtNo.Text.Trim();
                strArr = new List<string>();
                if(File.Exists(fl))
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + "WAV files\\" + fl + ".wav";
                    Player.URL = path;
                    Player.settings.volume = 90;
                    Player.controls.play();
                }
                else
                {
                    decimal p = 0;
                    p = Math.Truncate(Convert.ToDecimal(fl));
                    decimal d1 = Convert.ToDecimal(fl);
                    p = d1 - p;
                    p = Math.Round(p, 2);
                    long num = Convert.ToInt64(Math.Truncate(Convert.ToDouble(fl)));
                    long i = 10;
                    long i2 = 0;
                    List<string> strArr1 = new List<string>(); ;
                    
                        while (num > i / 10)
                        {
                            i2 = num % i - num % (i / 10);
                            if (i2 > 0)
                            {
                                strArr.Add(i2.ToString());
                            }

                            i *= 10;
                            /*if ((num % i - num % (i / 10)) >= 10 && (num % i - num % (i / 10)) < 20)
                            {
                                strArr.RemoveAt(strArr.Count - 1);
                                if ((num % i - num % (i / 10)) > 0)
                                {
                                    strArr.Add((i2 + (num % i - num % (i / 10))).ToString());
                                }
                            
                                i *= 10;
                            }*/

                        }
                        for (int k = strArr.Count-1; k >=0 ; k--)
                    {
                        if (strArr[k].Length == 6)
                        {
                            int m = Convert.ToInt32(strArr[k]);
                            m = m / 100000;
                            strArr[k] = m.ToString();
                            if (strArr[k - 1].Length == 5 || strArr[k - 1].Length == 4)
                            {
                                strArr.Insert(k, "hundred");
                            }
                            else
                            {
                                strArr.Insert(k, "hundred");
                                strArr.Insert(k, "thousand");
                            }
                            
                            continue;

                        }
                        if (strArr[k].Length == 5)
                        {
                                int m = Convert.ToInt32(strArr[k]);
                                m = m / 10000;
                                strArr[k] = m.ToString();
                                if (k-1 >=0 && strArr[k - 1].Length == 4)
                                {
                                    strArr[k] = strArr[k] + "" + strArr[k - 1].Substring(0, 1);
                                    strArr.RemoveAt(k - 1);
                                    strArr.Insert(k - 1, "thousand");
                                }
                                else
                                {
                                    strArr[k] = strArr[k]+"0";
                                    strArr.Insert(k, "thousand");
                                }
                                
                                continue;

                        }
                        if (strArr[k].Length == 4)
                        {
                            int m = Convert.ToInt32(strArr[k]);
                            m = m / 1000;
                            strArr[k] = m.ToString();
                            strArr.Insert(k, "thousand");
                            continue;
                        }
                        if (strArr[k].Length == 3)
                        {
                            int m = Convert.ToInt32(strArr[k]);
                            m = m / 100;
                            strArr[k] = m.ToString();
                            strArr.Insert(k, "hundred");
                        
                            continue;
                        }
                        if (strArr[k].Length == 2)
                        {
                            if((k-1) >=0 &&strArr[k-1].Length == 1 )
                            {
                                int d = Convert.ToInt32(strArr[k]);
                                d = d / 10;
                                strArr[k] = d.ToString();
                                strArr[k] = strArr[k] + "" + strArr[k - 1];
                                strArr.RemoveAt(k - 1);
                            }
                            
                            continue;
                        }
                    }
                    if(p>0)
                    {
                        string tr = p.ToString("0.00");
                        strArr.Insert(0, "--dollars");
                        strArr.Insert(0, "and");
                        strArr.Insert(0,p.ToString("0.00").Substring(2, 2));
                        strArr.Insert(0,"--cent");
                       
                    }
                    else
                    {
                        Regex regex = new Regex(@"^\d$");
                        try
                        {

                            double f = Convert.ToDouble(strArr[0]);
                            if (strArr.Count > 2)
                            {
                                strArr.Insert(1, "and");
                            }
                            strArr.Insert(0, "--dollars");
                           
                            
                        }
                        catch(Exception ex)
                        {
                            if (strArr.Count > 2)
                            {
                                strArr.Insert(2, "and");
                            }
                            strArr.Insert(0, "--dollars");
                            //strArr.Insert(2, "and");
                        }
                        
                       
                       
                    }
                    
                    var plist = Player1.playlistCollection.newPlaylist("myPL");
                   

                    string pt = AppDomain.CurrentDomain.BaseDirectory + "eng_sayitsmart\\" + strArr[strArr.Count - 1] + ".wav";
                    Player1.URL = pt;
                    strArr.RemoveAt(strArr.Count - 1);
                    Player1.PlayStateChange+= new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(str33);
                    
                    //var mediaItem1 = Player1.newMedia(AppDomain.CurrentDomain.BaseDirectory + "WAV files\\" + strArr[strArr.Count-1] + ".wav");
                    //plist.removeItem(mediaItem1);


                   //not necessary
                    
                    //Player1.currentPlaylist = plist;
                    
                }
                
                
               
            }
            catch(Exception ex)
            {
                MessageBox.Show("Text formate= is wrong");
            }
            
        }

        private void str33(int NewState)
        {
            if (Player1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                if (strArr.Count > 0)
                {
                    string pt = AppDomain.CurrentDomain.BaseDirectory + "eng_sayitsmart\\" + strArr[strArr.Count - 1] + ".wav";
                    Player1 = new WMPLib.WindowsMediaPlayer();
                    Player1.URL = pt;
                    
                    strArr.RemoveAt(strArr.Count - 1);
                    Player1.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(str33);
                }

            }
           
        }

        

        



        
    }
}
