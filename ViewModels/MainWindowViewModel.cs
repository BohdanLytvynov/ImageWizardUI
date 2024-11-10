
using System.Windows.Input;
using ViewModelBase.Commands;

namespace ImageWizardUI.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase.VM.ViewModelBase
    {
        #region Fields
        private string m_title;

        private string m_path_to_images;
        #endregion

        #region Properties
        public string Title { get => m_title; set => Set(ref m_title, value); }

        public string PathToImgs { get=> m_path_to_images; set => Set(ref m_path_to_images, value); }
        #endregion

        #region Commands

        public ICommand OnOpenButtonPressed { get; }

        #endregion

        #region Ctor
        public MainWindowViewModel()
        {
            #region Init Fields

            m_title = "Image Wizard";

            m_path_to_images = string.Empty;

            #endregion

            #region Init Commands

            OnOpenButtonPressed = new Command
                (
                    OnOpenButtonPressedExecute,
                    CanOnOpenButtonPressedExecute                    
                );

            #endregion
        }
        #endregion

        #region Methods

        #region On Open Button pressed

        private bool CanOnOpenButtonPressedExecute(object p) => true;

        private void OnOpenButtonPressedExecute(object p)
        { 
            
        }

        #endregion

        #endregion
    }
}
