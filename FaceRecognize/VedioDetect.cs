using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Threading;
using System.Windows.Forms;
using static FaceRecognize.RecognizeHelper;

namespace FaceRecognize
{
    public partial class VedioDetect : Form
    {
        VideoCapture _videoCapture;
        RecognizeHelper _recognizeHelper;

        int _currentFaceFlag = 0;

        //点击鼠标时的人脸检测对象
        FaceDetectedObj _currentfdo; 

        System.Windows.Forms.Timer _timer;

        public VedioDetect()
        {
            InitializeComponent();

            try
            {
                _recognizeHelper = new RecognizeHelper(Application.StartupPath + "\\trainedFaces",
                    Application.StartupPath + "\\trainedModels\\haarcascade_frontalface_default.xml");

                CvInvoke.UseOpenCL = false;

                _timer = new System.Windows.Forms.Timer();
                _timer.Enabled = true;
                _timer.Interval = 100; //100毫秒检测一次人脸
                _timer.Tick += new System.EventHandler(this.timer_Tick);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frameProcess(object sender, EventArgs arg)
        {
            try
            {
                Mat mat = new Mat();

                Monitor.Enter(_videoCapture);
                _videoCapture.Retrieve(mat, 0);
                Monitor.Exit(_videoCapture);

                picShow.Image = mat.Bitmap;
            }
            catch (Exception ex) { throw ex; }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_videoCapture == null)
                {
                    _videoCapture = new VideoCapture();
                    _videoCapture.ImageGrabbed += frameProcess; //实时获取图像
                    _videoCapture.Start();
                }

                Monitor.Enter(_videoCapture);
                Mat mat = _videoCapture.QueryFrame();
                Monitor.Exit(_videoCapture);

                if (!mat.IsEmpty)
                {
                    FaceDetectedObj faceobj = _recognizeHelper.faceRecognize(mat);
                    if (!faceobj.originalImg.IsEmpty)
                        picShow.Image = faceobj.originalImg.Bitmap;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                //_videoCapture.Stop();
                //_videoCapture.Dispose();                
            }
        }

        private void SampleBox_Click(object sender, EventArgs e)
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

                Image<Gray, byte> result = _currentfdo.originalImg
                    .ToImage<Gray, byte>()
                    .Copy(_currentfdo.facesRectangle[i])
                    .Resize(100, 100, Emgu.CV.CvEnum.Inter.Cubic);

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
