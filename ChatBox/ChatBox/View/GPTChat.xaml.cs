﻿using ChatBox.View.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.VisualBasic;
using System.IO;
using NAudio.Wave;
using System.Text.Json;
using Newtonsoft.Json;

namespace ChatBox.View
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class GPTChat : UserControl
    {
        public GPTChat()
        {
            InitializeComponent();
            AttachTextBoxEvents();
        }

        private void AttachTextBoxEvents()
        {
            txtMessage.PreviewKeyDown += txtMessage_PreviewKeyDown;
            txtMessage.PreviewKeyUp += txtMessage_PreviewKeyUp;
        }
        private void InputImage_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void MicButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isRecording)
            {
                try
                {
                    StartRecordingAsync();
                    VoiceStatus.IsOpen = !VoiceStatus.IsOpen;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error starting recording: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    StopRecordingAsync();
                    string transcribeResult = await TranscribeAudioAsync(outputPath);
                    txtMessage.Text = transcribeResult;
                    VoiceStatus.IsOpen = !VoiceStatus.IsOpen;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error stopping recording: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            isRecording = !isRecording;
        }

        private WaveInEvent waveSource;
        private WaveFileWriter waveFileWriter;
        private string outputPath;
        private bool isRecording = false;

        private async Task StartRecordingAsync()
        {
            string currentDirectory = Environment.CurrentDirectory;
            string voicesDirectory = System.IO.Path.Combine(currentDirectory, "Voices");

            if (!Directory.Exists(voicesDirectory))
            {
                Directory.CreateDirectory(voicesDirectory);
            }

            outputPath = System.IO.Path.Combine(voicesDirectory, "recorded.wav");

            waveSource = new WaveInEvent();
            waveSource.WaveFormat = new WaveFormat(44100, 1); // 44.1kHz, 16-bit, mono
            waveSource.DataAvailable += WaveSourceDataAvailable;

            waveFileWriter = new WaveFileWriter(outputPath, waveSource.WaveFormat);

            waveSource.StartRecording();
        }

        private async Task StopRecordingAsync()
        {
            waveSource.StopRecording();
            waveFileWriter.Close();
            waveSource.Dispose();
            waveFileWriter.Dispose();
            waveSource = null;
            waveFileWriter = null;
        }

        private async Task<string> TranscribeAudioAsync(string filePath)
        {
            var payload = File.ReadAllBytes(filePath);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("api-key", "K9knWqlkJYKXMktifrb983EjYjYSZKhF");

                var response = await client.PostAsync("https://api.fpt.ai/hmi/asr/general", new ByteArrayContent(payload));
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Phân tích kết quả JSON
                    var jsonDoc = JsonDocument.Parse(result);
                    var hypotheses = jsonDoc.RootElement.GetProperty("hypotheses");
                    var firstHypothesis = hypotheses.EnumerateArray().FirstOrDefault();

                    if (firstHypothesis.TryGetProperty("utterance", out var utterance))
                    {
                        return utterance.GetString();

                    }
                    else
                    {
                        return ("Không thể tìm thấy thông tin 'utterance' trong kết quả.");

                    }
                }
                else
                {
                    return $"Error: {response.StatusCode}";

                }
            }
        }

        private void WaveSourceDataAvailable(object sender, WaveInEventArgs e)
        {
            if (e.BytesRecorded > 0)
            {
                waveFileWriter.Write(e.Buffer, 0, e.BytesRecorded);
                waveFileWriter.Flush();
            }
        }

        private bool userScrolled = false;
        private DispatcherTimer scrollTimer;

        private void ChatScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            // Tự động cuộn xuống dưới cùng khi Loaded
            ScrollToBottom();
        }

        private void ChatScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            // Kiểm tra xem người dùng đã cuộn chưa
            if (!userScrolled)
            {
                // Tự động cuộn xuống dưới cùng
                ScrollToBottom();
            }

            // Đặt lại giá trị userScrolled sau một khoảng thời gian
            ResetUserScrolledTimer();
        }

        // Hàm để tự động cuộn xuống dưới cùng
        private void ScrollToBottom()
        {
            ChatScrollViewer.ScrollToEnd();
        }

        // Hàm xử lý sự kiện PreviewMouseWheel để đánh dấu là người dùng đã cuộn
        private void ChatScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            userScrolled = true;

            // Đặt lại giá trị userScrolled sau một khoảng thời gian
            ResetUserScrolledTimer();
        }

        // Đặt lại giá trị userScrolled sau một khoảng thời gian
        private void ResetUserScrolledTimer()
        {
            if (scrollTimer == null)
            {
                scrollTimer = new DispatcherTimer();
                scrollTimer.Interval = TimeSpan.FromSeconds(0.2); // Đặt thời gian 
                scrollTimer.Tick += (sender, args) =>
                {
                    userScrolled = false;
                    scrollTimer.Stop();
                };
            }

            scrollTimer.Stop();
            scrollTimer.Start();
        }

        private bool isShiftKeyPressed = false;
        private void txtMessage_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                isShiftKeyPressed = true;
            }
            else if (e.Key == Key.Enter)
            {
                if (!isShiftKeyPressed)
                {
                    e.Handled = true;
                    SendButton_Click(sender, e);
                }
            }
        }

        private void txtMessage_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                isShiftKeyPressed = false;
            }
        }
        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = txtMessage.Text.Trim(); // Loại bỏ các khoảng trắng ở đầu và cuối chuỗi

            // Kiểm tra xem chuỗi có chứa chỉ ký tự trắng hoặc xuống dòng không
            if (string.IsNullOrWhiteSpace(userMessage))
            {
                // Ngăn chặn việc thêm Input nếu dữ liệu người dùng trống
                return;
            }

            Input newInput = new Input
            {
                Message = userMessage,
            };
            ChatPanel.Children.Add(newInput);

            // Xóa nội dung TextBox sau khi gửi
            txtMessage.Text = string.Empty;


            string generatedText = await GetGeneratedTextFromAI(userMessage);

            Output newOutput = new Output
            {
                Message = generatedText,
            };
            
            ChatPanel.Children.Add(newOutput);
        }

        private async Task<string> GetGeneratedTextFromAI(string userInput)
        {
            var client = new HttpClient();
            var baseUrl = "https://api.openai.com/v1/chat/completions";
            //APIKey of GPT Platform
            string api1 = "sk-hXwwVNT8aWTDYLTGiIAHT3Bl";
            string api2 = "bkFJoXgptjRynAfW6vvNIUlG";
            var apiKey = api1 + api2;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            while (true)
            {

                var parameters = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[] { new { role = "user", content = userInput }, },
                    max_tokens = 1024,
                    temperature = 0.2f,
                };

                var response = await client.PostAsync(baseUrl, new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json"));// new StringContent(json));

                // Read the response
                var responseContent = await response.Content.ReadAsStringAsync();

                // Extract the completed text from the response
                dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
                string generatedText = responseObject.choices[0].message.content;

                return generatedText;
            }
        }
    }
}
