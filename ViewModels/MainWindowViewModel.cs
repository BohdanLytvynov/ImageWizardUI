
using ImageWizardUI.ViewModels.DataViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using IOU = Domain.Utilities.IOUtility;
using ViewModelBase.Commands;

namespace ImageWizardUI.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase.VM.ViewModelBase
    {
        #region Fields
        private string m_title;

        private string m_path_to_images;

        private string m_path_to_output;

        private int m_CanvasWidth;

        private int m_CanvasHeight;

        private bool m_useResize;

        private Visibility m_resizeGridVisibility;

        private ObservableCollection<FileViewModel> m_Files;

        private int m_selectedFileIndex;

        private string m_outputFileName;
        #endregion

        #region Properties
        public string Title { get => m_title; set => Set(ref m_title, value); }

        public string PathToImgs 
        {
            get=> m_path_to_images;
            set=> Set(ref m_path_to_images, value);                 
             
        }

        public string PathToOutput { get => m_path_to_output; set => Set(ref m_path_to_output, value); }

        public int CanvasWidth { get => m_CanvasWidth; set => Set(ref m_CanvasWidth, value); }

        public int CanvasHeight { get => m_CanvasHeight; set => Set(ref m_CanvasHeight, value); }

        public bool UseResize { get => m_useResize; 
            set 
            { 
                Set(ref m_useResize, value);

                ChangeVisibilityOfTheResizeGrid(UseResize);
            } 
        
        }

        public Visibility ResizeGridVisibility { get => m_resizeGridVisibility; set => Set(ref m_resizeGridVisibility, value); }

        public ObservableCollection<FileViewModel> Files { get => m_Files; set => m_Files = value; }

        public int SelectedFileIndex { get => m_selectedFileIndex; set => Set(ref m_selectedFileIndex, value); }

        public string OutputFileName { get=> m_outputFileName; set=> Set(ref m_outputFileName, value); }
        #endregion

        #region Commands

        public ICommand OnOpenButtonPressed { get; }

        public ICommand OnOpen2ButtonPressed { get; }

        #endregion

        #region Ctor
        public MainWindowViewModel()
        {
            #region Init Fields

            m_title = "Image Wizard Version 1";

            m_path_to_images = string.Empty;

            m_path_to_output = string.Empty;

            m_outputFileName = string.Empty;

            m_CanvasWidth = 0;

            m_CanvasHeight = 0;

            m_useResize = false;

            m_resizeGridVisibility = Visibility.Collapsed;

            m_Files = new ObservableCollection<FileViewModel>();

            m_selectedFileIndex = -1;

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

        private bool CanOnOpenButtonPressedExecute(object p) => true;

        private void OnOpenButtonPressedExecute(object p)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PathToImgs = dialog.SelectedPath;

                var files = IOU.GetFiles(PathToImgs);

                if (Files.Count > 0)
                    Files.Clear(); 

                for (int i = 0; i < files.Length; i++)
                {
                    var arr = files[i].Split(System.IO.Path.DirectorySeparatorChar);

                    Files.Add(new FileViewModel(i + 1, arr[arr.Length - 1]));
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

        #endregion
    }
}
