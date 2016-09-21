
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;
using System.Threading.Tasks;

namespace DrawIt
{
    public class KarokeMachine
    {
        public event EventHandler<EventArgs> Stopped;
        public bool IsStopped = true;
        public bool IsLoaded = false;

        MediaPlayer player = null;
        MediaRecorder recorder = null;
        string filePath = "/data/data/DrawIt.DrawIt/files/";
        string fileExt = ".mp4";
        string currentFilePath = "";
        string storyPath = "";

        public KarokeMachine(Guid storyID, int pageNumber)
        {
            storyPath = filePath + storyID.ToString();    
            currentFilePath = filePath + storyID.ToString() + "/" + pageNumber + fileExt;
        }

        public void Record()
        {
            try
            {
                //Java.IO.File sdDir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMusic);
                //filePath = sdDir + "/" + "testAudio.mp3";
                if (!Directory.Exists(storyPath))
                    Directory.CreateDirectory(storyPath);

                if (File.Exists(currentFilePath))
                    File.Delete(currentFilePath);

                //Java.IO.File myFile = new Java.IO.File(filePath);
                //myFile.CreateNewFile();

                if (recorder == null)
                    recorder = new MediaRecorder(); // Initial state.
                else
                    recorder.Reset();

                recorder.SetAudioSource(AudioSource.Mic);
                recorder.SetOutputFormat(OutputFormat.Mpeg4);
                recorder.SetAudioEncoder(AudioEncoder.AmrNb); // Initialized state.
                recorder.SetOutputFile(currentFilePath); // DataSourceConfigured state.
                recorder.Prepare(); // Prepared state
                recorder.Start(); // Recording state.
                IsStopped = false;

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
            }
        }
        
        public void Stop()
        {
            IsStopped = true;
            if (recorder != null)
            {
                recorder.Stop();
                recorder.Release();
                recorder = null;
            }

            if ((player != null))
            {
                if (player.IsPlaying)
                {
                    player.Stop();
                }
                player.Release();
                player = null;
            }
        }

        public async void Play()
        {
            try
            {
                if (player == null)
                {
                    player = new MediaPlayer();
                }
                else
                {
                    player.Reset();
                }

                // This method works better than setting the file path in SetDataSource. Don't know why.
                Java.IO.File file = new Java.IO.File(currentFilePath);
                Java.IO.FileInputStream fis = new Java.IO.FileInputStream(file);
                await player.SetDataSourceAsync(fis.FD);

                //player.SetDataSource(filePath);
                player.Prepare();
                player.Start();
                player.Completion += Player_Completion;
                IsStopped = false;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
            }
        }

        private void Player_Completion(object sender, EventArgs e)
        {
            Stopped?.Invoke(this, e);           
        }
    }
}