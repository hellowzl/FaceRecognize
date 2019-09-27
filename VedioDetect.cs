using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Windows.Forms;

namespace FaceRecognize
{
    public partial class VedioDetect : Form
    {
        Mat _mat;//摄像头图像
        VideoCapture _videoCapture;//摄像头对象
        RecognizeHelper _recognizeHelper;

        int _currentFaceFlag = 0;
        RecognizeHelper.faceDetectedObj _currentfdo; //点击鼠标时的人脸检测对象

        Timer _timer;

        public VedioDetect()
        {
            InitializeComponent();

            try
            {                
                _recognizeHelper = new RecognizeHelper(Application.StartupPath + "\\trainedFaces", 
                    Application.StartupPath + "\\trainedModels\\haarcascade_frontalface_default.xml");

                _videoCapture = new VideoCapture();
                _videoCapture.Start(); //摄像头开始工作
                _videoCapture.ImageGrabbed += frameProcess; //实时获取图像

                CvInvoke.UseOpenCL = false;

                _timer = new Timer();
                _timer.Enabled = true;
                _timer.Interval = 100;
                _timer.Tick += new System.EventHandler(this.timer_Tick);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frameProcess(object sender, EventArgs arg)
        {
            _mat = new Mat();
            _videoCapture.Retrieve(_mat, 0);
            picShow.Image = _mat.Bitmap;
        }

        private void timer_Tick(object sender, EventArgs e)
        {            
            try
            {
                //100毫秒检测一次人脸
                picShow.Image = _recognizeHelper.faceRecognize(_videoCapture.QueryFrame()).originalImg.Bitmap;
            }
            catch { }
        }
        
        private void sampleBox_Click(object sender, EventArgs e)
        {
            _currentfdo = _recognizeHelper.GetFaceRectangle(_videoCapture.QueryFrame());
            _currentFaceFlag = 0;
            getCurrentFaceSample(0);
        }

        private void getCurrentFaceSample(int i)
        {
            try
            {
                fullname.Text = "";
                Image<Gray, byte> result = _currentfdo.originalImg.ToImage<Gray, byte>().Copy(_currentfdo.facesRectangle[i]).Resize(100, 100, Emgu.CV.CvEnum.Inter.Cubic);
                result._EqualizeHist();//灰度直方图均衡化
                sampleBox.Image = result.Bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show("没有检测到人脸");
            }
        }

        private void PreImage_Click(object sender, EventArgs e)
        {
            if (_currentFaceFlag == 0)
            {
                MessageBox.Show("已经是第一张");
            }
            else
            {
                _currentFaceFlag--;

                getCurrentFaceSample(_currentFaceFlag);
            }
        }

        private void NextImage_Click(object sender, EventArgs e)
        {
            if (_currentFaceFlag == _currentfdo.facesRectangle.Count - 1)
            {
                MessageBox.Show("已经是最后一张");
            }
            else
            {
                _currentFaceFlag++;
                getCurrentFaceSample(_currentFaceFlag);
            }
        }

        private void SetClass_Click(object sender, EventArgs e)
        {
            switch (recognizerType.Text)
            {
                case "EigenFaceRecognizer":
                    _recognizeHelper.SetTrainedFaceRecognizer(RecognizeHelper.FaceRecognizerType.EigenFaceRecognizer);
                    break;
                case "FisherFaceRecognizer":
                    _recognizeHelper.SetTrainedFaceRecognizer(RecognizeHelper.FaceRecognizerType.FisherFaceRecognizer);
                    break;
                case "LBPHFaceRecognizer":
                    _recognizeHelper.SetTrainedFaceRecognizer(RecognizeHelper.FaceRecognizerType.LBPHFaceRecognizer);
                    break;
            }
        }

        private void SaveSampleImage_Click(object sender, EventArgs e)
        {
            if (fullname.Text == "")
            {
                MessageBox.Show("请输入样本姓名。");
            }
            else
            {
                try
                {
                    string filePath = Application.StartupPath + "\\trainedFaces\\" + fullname.Text + "_" + System.Guid.NewGuid().ToString() + ".jpg";
                    sampleBox.Image.Save(filePath);
                    MessageBox.Show("样本保存完毕。");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("操作失败！" + ex.ToString());
                }
            }
        }
    }
}
