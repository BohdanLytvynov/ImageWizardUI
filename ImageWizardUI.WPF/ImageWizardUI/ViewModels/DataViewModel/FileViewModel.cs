using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase.VM;

namespace ImageWizardUI.ViewModels.DataViewModel
{
    internal class FileViewModel : ViewModelBase.VM.ViewModelBase
    {
        #region Fields
        private int m_number;

        private string m_fileName;

        private string m_path;
        #endregion

        #region Properties
        public int Number { get => m_number; set => Set(ref m_number, value); }

        public string FileName { get => m_fileName; set => Set(ref m_fileName, value); }

        public string FilePath { get => m_path;}
        #endregion

        #region Ctor
        public FileViewModel(int number, string fileName, string Filepath)
        {
            #region Init Fields
            m_fileName = fileName;

            m_number = number;
            m_path = Filepath;
            #endregion
        }

        public FileViewModel(int number) : this(number, string.Empty, string.Empty)
        {
            
        }

        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{Number} | {FileName}";
        }
        #endregion
    }
}
