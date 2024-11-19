
using ImageWizardUI.ViewModels.DataViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using IOU = Domain.Utilities.IOUtility;
using VU = Domain.Utilities.ValidationUtility;
using ViewModelBase.Commands;
using Aspose.Core;

namespace ImageWizardUI.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase.VM.ViewModelBase
    {
        #region Fields

        private ImageWizard m_imgWizard;

        private string m_title;

        private string m_path_to_images;

        private string m_path_to_output;

        private string m_Columns;

        private string m_Rows;

        private bool m_useResize;

        private string m_newImgWidth;

        private string m_newImgHeight;

        private bool m_useOffsets;

        private Visibility m_resizeGridVisibility;

        private Visibility m_offsetsGridVisibility;

        private Visibility m_ProgressGridVisibility;

        private ObservableCollection<FileViewModel> m_Files;

        private int m_selectedFileIndex;

        private string m_outputFileName;

        private string m_VerticalOffset;

        private string m_HorizontalOffset;

        private float m_CurrentValue;
        #endregion

        #region Properties
        public string Title { get => m_title; set => Set(ref m_title, value); }

        public string PathToImgs 
        {
            get=> m_path_to_images;
            set=> Set(ref m_path_to_images, value);                              
        }

        public string PathToOutput { get => m_path_to_output; set => Set(ref m_path_to_output, value); }

        public string Columns { get => m_Columns; set => Set(ref m_Columns, value); }

        public string Rows { get => m_Rows; set => Set(ref m_Rows, value); }

        public bool UseResize { get => m_useResize; 
            set 
            { 
                Set(ref m_useResize, value);

                ChangeVisibilityOfTheResizeGrid(UseResize);
            }         
        }

        public string NewImgWidth { get => m_newImgWidth; set => Set(ref m_newImgWidth, value); }

        public string NewImgHeight { get => m_newImgHeight; set => Set(ref m_newImgHeight, value); }

        public bool UseOffsets 
        {
            get => m_useOffsets;
            set
            {
                Set(ref m_useOffsets, value);

                ChangeVisibilityOfTheOffsetGrid(UseOffsets);
            }
        
        }

        public Visibility ResizeGridVisibility { get => m_resizeGridVisibility; set => Set(ref m_resizeGridVisibility, value); }

        public Visibility OffsetsGridVisibility { get => m_offsetsGridVisibility; set => Set(ref m_offsetsGridVisibility, value); }

        public Visibility ProgressGridVisibility { get => m_ProgressGridVisibility; set => Set(ref m_ProgressGridVisibility, value); }

        public ObservableCollection<FileViewModel> Files { get => m_Files; set => m_Files = value; }

        public int SelectedFileIndex { get => m_selectedFileIndex; set => Set(ref m_selectedFileIndex, value); }

        public string OutputFileName { get=> m_outputFileName; set=> Set(ref m_outputFileName, value); }

        public string VerticalOffset { get => m_VerticalOffset; set => Set(ref m_VerticalOffset, value); }

        public string HorizontalOffset { get => m_HorizontalOffset; set => Set(ref m_HorizontalOffset, value); }

        public float CurrentValue { get => m_CurrentValue; set => Set(ref m_CurrentValue, value); }
        #endregion

        #region DataValidation Error

        public override string this[string columnName]
        {
            get
            {
                string error = string.Empty;

                bool value = false;

                switch (columnName)
                {
                    case nameof(PathToImgs):
                        value = VU.ValidateTextIsNotEmpty(PathToImgs, out error);// 0
                        base.SetValidationArrayValue(0, value);
                        break;
                    case nameof(PathToOutput):
                        value = VU.ValidateTextIsNotEmpty(PathToOutput, out error);// 1
                        base.SetValidationArrayValue(1, value);
                        break;
                    case nameof(Columns):
                        value = VU.ValidateDigits(Columns, out error, IntGreaterThenZero);// 2
                        base.SetValidationArrayValue(2, value);
                        break;
                    case nameof(Rows):
                        value = VU.ValidateDigits(Rows, out error, IntGreaterThenZero);// 3
                        base.SetValidationArrayValue(3, value);
                        break;
                    case nameof(NewImgWidth):
                        value = VU.ValidateDigits(NewImgWidth, out error, IntGreaterThenZero);// 4
                        base.SetValidationArrayValue(4, value);
                        break;
                    case nameof(NewImgHeight):
                        value = VU.ValidateDigits(NewImgHeight, out error, IntGreaterThenZero); // 5
                        base.SetValidationArrayValue(5, value);
                        break;
                    case nameof(VerticalOffset):
                        value = VU.ValidateDigits(VerticalOffset, out error, FloatGreaterThenZero); // 6
                        base.SetValidationArrayValue(6, value);
                        break;
                    case nameof(HorizontalOffset):
                        value = VU.ValidateDigits(HorizontalOffset, out error, FloatGreaterThenZero); // 7
                        base.SetValidationArrayValue(7, value);
                        break;
                }

                return error;
            }
        }

        #endregion

        #region Commands

        public ICommand OnOpenButtonPressed { get; }

        public ICommand OnOpen2ButtonPressed { get; }

        public ICommand OnStartProcessingButtonPressed { get; }

        #endregion

        #region Ctor
        public MainWindowViewModel()
        {
            #region Init Fields

            m_imgWizard = new ImageWizard();

            m_imgWizard.OnImageProcessed += OnImgProcessed;

            m_title = "Image Wizard Version 1";

            m_path_to_images = string.Empty;

            m_path_to_output = string.Empty;

            m_outputFileName = string.Empty;

            m_Columns = "0";

            m_Rows = "0";

            m_newImgHeight = "0"; 

            m_newImgWidth = "0";

            m_useResize = false;

            m_useOffsets = false;

            m_VerticalOffset = "0";

            m_HorizontalOffset = "0";

            m_resizeGridVisibility = Visibility.Collapsed;

            m_offsetsGridVisibility = Visibility.Collapsed;

            m_ProgressGridVisibility = Visibility.Collapsed;

            m_Files = new ObservableCollection<FileViewModel>();

            m_selectedFileIndex = -1;

            m_CurrentValue = 0f;

            #endregion

            #region Init Commands

            OnOpenButtonPressed = new Command
                (
                    OnOpenButtonPressedExecute,
                    CanOnOpenButtonPressedExecute                    
                );

            OnOpen2ButtonPressed = new Command
                (
                    OnOpen2ButtonPressedExecute, 
                    CanOnOpen2ButtonPressedExecute
                );

            OnStartProcessingButtonPressed = new Command
                (
                    OnStartProcessingButtonPressedExecute,
                    CanOnStartProcessingButtonPressedExecute
                );

            #endregion

            #region Init Validation Array

            base.InitializeValidationArray(8);

            #endregion
        }
        #endregion

        #region Methods

        #region On Open Button pressed

        private void ChangeVisibilityOfTheResizeGrid(bool useResizeGrid)
        {
            if (useResizeGrid)
            {
                ResizeGridVisibility = Visibility.Visible;
            }
            else
            {
                ResizeGridVisibility = Visibility.Collapsed;
            }
        }

        private void ChangeVisibilityOfTheOffsetGrid(bool useOffsetGrid)
        {
            if (useOffsetGrid)
            {
                OffsetsGridVisibility = Visibility.Visible;
            }
            else
            { 
                OffsetsGridVisibility= Visibility.Collapsed;
            }
        }

        private bool CanOnOpenButtonPressedExecute(object p) => true;

        private void OnOpenButtonPressedExecute(object p)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            
            dialog.ShowNewFolderButton = true;
            dialog.ShowHiddenFiles = true;
            dialog.ShowPinnedPlaces = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PathToImgs = dialog.SelectedPath;

                var files = IOU.GetFiles(PathToImgs);

                if (Files.Count > 0)
                    Files.Clear(); 

                for (int i = 0; i < files.Length; i++)
                {
                    var arr = files[i].Split(System.IO.Path.DirectorySeparatorChar);

                    Files.Add(new FileViewModel(i + 1, arr[arr.Length - 1], files[i]));
                }
            }
        }

        #endregion

        #region On Open2 Button Pressed

        private bool CanOnOpen2ButtonPressedExecute(object p) => true;

        private void OnOpen2ButtonPressedExecute(object p)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PathToOutput = dialog.SelectedPath;
            }
        }

        #endregion

        #region On Start Processing Button Pressed

        private bool CanOnStartProcessingButtonPressedExecute(object p)
        {
            var res = base.CheckValidationArray(0, 3);//Validate Everything Except Resizing Grid and Offset Grid

            if (m_useOffsets || m_useResize)
            {
                if (m_useResize && !m_useOffsets)//Validate only Resizing Grid
                {
                    var resizer = base.CheckValidationArray(4, 5);

                    return resizer && res;
                }
                else if (!m_useResize && m_useOffsets)//Validate only Offset Grid
                {
                    var offset = base.CheckValidationArray(6, 7);

                    return offset && res;
                }
                else //Validate Both Resizing and Offset Grid
                {
                    var resizer = base.CheckValidationArray(4, 5);
                    var offset = base.CheckValidationArray(6, 7);

                    return resizer && offset && res;
                }
            }
            
            return res;
        }

        private void OnStartProcessingButtonPressedExecute(object p)
        {
            Aspose.Imaging.Size CanvasSize = new()
            {
                Width = int.Parse(Columns),
                Height = int.Parse(Rows)
            };

            Aspose.Imaging.Size Offset = new()
            { 
                Width = int.Parse(HorizontalOffset),
                Height = int.Parse(VerticalOffset)
            };

            Aspose.Imaging.Size newImgSize = new()
            { 
                Width = int.Parse(NewImgWidth),
                Height = int.Parse(NewImgHeight)
            };

            var pathes = Files.Where(x => true).Select(x => x.FilePath).ToArray();

            ProgressGridVisibility = Visibility.Visible;

            var t = Task.Run(() =>
            {
                m_imgWizard.CreateSpriteSheet(
                    CanvasSize,
                    Offset,
                    pathes,
                    PathToOutput,
                    OutputFileName,
                    UseResize,
                    newImgSize);
            });

            t.ContinueWith(t => 
            {
                this.Dispatcher.Invoke(() =>
                {
                    ProgressGridVisibility = Visibility.Hidden;

                    CurrentValue = 0f;
                });
            });            
        }

        private void OnImgProcessed(int imgIndex)
        {
            this.Dispatcher?.Invoke(() =>
            {               
                CurrentValue = (float)imgIndex / (float)Files.Count;
            });            
        }

        private bool IntGreaterThenZero(string value)
        { 
            int v = int.Parse(value);

            return v > 0;
        }

        private bool FloatGreaterThenZero(string value)
        {
            float v = float.Parse(value);

            return v > 0;
        }

        #endregion

        #endregion
    }
}
