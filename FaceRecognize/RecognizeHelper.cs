using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Speech.Synthesis;

namespace FaceRecognize
{
    public class RecognizeHelper
    {
        public string _sampleImagePath;
        public CascadeClassifier _cascadeClassifier;
        public TrainedFaceRecognizer _trainedFaceRecognizer;
        public string _nameCahce = "";

        public RecognizeHelper(string sampleImagePath, string cascadeClassifierPath)
        {
            _sampleImagePath = sampleImagePath;
            _cascadeClassifier = new CascadeClassifier(cascadeClassifierPath);

            SetTrainedFaceRecognizer(FaceRecognizerType.EigenFaceRecognizer);
        }

        /// <summary>
        /// 获取已保存的所有样本文件
        /// </summary>
        /// <returns></returns>
        public TrainedFileList SetSampleFacesList()
        {
            try
            {
                TrainedFileList trainedFileList = new TrainedFileList();
                DirectoryInfo di = new DirectoryInfo(_sampleImagePath);
                int i = 0;

                int count = di.GetFiles().Length;
                trainedFileList.trainedImages = new Mat[count];
                trainedFileList.trainedLabelOrder = new int[count];
                trainedFileList.trainedFileName = new string[count];

                foreach (FileInfo fi in di.GetFiles())
                {
                    Mat mat = new Mat(fi.FullName);

                    trainedFileList.trainedImages[i] = new Mat(fi.FullName);
                    trainedFileList.trainedLabelOrder[i] = i;
                    trainedFileList.trainedFileName[i] = fi.Name.Split('_')[0];

                    i++;
                }
                return trainedFileList;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 训练人脸识别器
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public TrainedFaceRecognizer SetTrainedFaceRecognizer(FaceRecognizerType type)
        {
            try
            {
                _trainedFaceRecognizer = new TrainedFaceRecognizer();
                _trainedFaceRecognizer.trainedFileList = SetSampleFacesList();

                switch (type)
                {
                    case FaceRecognizerType.EigenFaceRecognizer:
                        _trainedFaceRecognizer.faceRecognizer = new Emgu.CV.Face.EigenFaceRecognizer(80, double.PositiveInfinity);

                        break;
                    case FaceRecognizerType.FisherFaceRecognizer:
                        _trainedFaceRecognizer.faceRecognizer = new Emgu.CV.Face.FisherFaceRecognizer(80, 3500);
                        break;
                    case FaceRecognizerType.LBPHFaceRecognizer:
                        _trainedFaceRecognizer.faceRecognizer = new Emgu.CV.Face.LBPHFaceRecognizer(1, 8, 8, 8, 100);
                        break;
                }

                //根据样例图片训练faceRecognizer
                _trainedFaceRecognizer.faceRecognizer.Train(_trainedFaceRecognizer.trainedFileList.trainedImages,
                    _trainedFaceRecognizer.trainedFileList.trainedLabelOrder);

                return _trainedFaceRecognizer;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 获取制定图片，识别出的人脸矩形框
        /// </summary>
        /// <param name="emguImage"></param>
        /// <returns></returns>
        public FaceDetectedObj GetFaceRectangle(Mat emguImage)
        {
            try
            {
                FaceDetectedObj fdo = new FaceDetectedObj();
                fdo.originalImg = emguImage;
                List<Rectangle> faces = new List<Rectangle>();

                using (UMat ugray = new UMat())
                {
                    CvInvoke.CvtColor(emguImage, ugray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);//灰度化图片
                    CvInvoke.EqualizeHist(ugray, ugray);//均衡化灰度图片

                    Rectangle[] facesDetected = _cascadeClassifier.DetectMultiScale(ugray, 1.1, 10, new Size(20, 20));
                    faces.AddRange(facesDetected);
                }

                fdo.facesRectangle = faces;
                return fdo;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 人脸识别
        /// </summary>
        /// <param name="emguImage"></param>
        /// <returns></returns>
        public FaceDetectedObj faceRecognize(Mat emguImage)
        {
            try
            {
                FaceDetectedObj fdo = GetFaceRectangle(emguImage);
                Image<Gray, byte> tempImg = fdo.originalImg.ToImage<Gray, byte>();

                #region 给识别出的所有人脸画矩形框

                using (Graphics g = Graphics.FromImage(fdo.originalImg.Bitmap))
                {
                    foreach (Rectangle face in fdo.facesRectangle)
                    {
                        g.DrawRectangle(new Pen(Color.Red, 2), face);//给识别出的人脸画矩形框

                        Image<Gray, byte> grayFace = tempImg.Copy(face).Resize(100, 100, Emgu.CV.CvEnum.Inter.Cubic);
                        grayFace._EqualizeHist(); //得到均衡化人脸的灰度图像

                        #region 得到匹配姓名，并画出

                        Emgu.CV.Face.FaceRecognizer.PredictionResult pr = _trainedFaceRecognizer.faceRecognizer.Predict(grayFace);

                        int label = pr.Label;
                        if (label < 0)
                            continue;

                        string recogniseName = _trainedFaceRecognizer.trainedFileList.trainedFileName[pr.Label].ToString();
                        string name = _trainedFaceRecognizer.trainedFileList.trainedFileName[pr.Label].ToString();

                        Font font = new Font("宋体", 16, GraphicsUnit.Pixel);
                        SolidBrush fontLine = new SolidBrush(Color.Yellow);
                        float xPos = face.X + (face.Width / 2 - (name.Length * 14) / 2);
                        float yPos = face.Y - 21;
                        g.DrawString(name, font, fontLine, xPos, yPos);

                        if (_nameCahce != name)
                        {
                            _nameCahce = name;
                            SpeechSynthesizer synth = new SpeechSynthesizer();
                            synth.SetOutputToDefaultAudioDevice();
                            synth.SpeakAsync("欢迎您" + name);
                        }

                        #endregion

                        fdo.names.Add(name);
                    }
                }

                #endregion
                return fdo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 自定义类及访问类型

        public class TrainedFileList
        {
            public Mat[] trainedImages = null;
            public int[] trainedLabelOrder = null;
            public string[] trainedFileName = null;
        }

        public class TrainedFaceRecognizer
        {
            public Emgu.CV.Face.FaceRecognizer faceRecognizer;
            public TrainedFileList trainedFileList;
        }

        public class FaceDetectedObj
        {
            public Mat originalImg;
            public List<Rectangle> facesRectangle;
            public List<string> names = new List<string>();
        }

        public enum FaceRecognizerType
        {
            EigenFaceRecognizer = 0,
            FisherFaceRecognizer = 1,
            LBPHFaceRecognizer = 2,
        };

        #endregion
    }
}