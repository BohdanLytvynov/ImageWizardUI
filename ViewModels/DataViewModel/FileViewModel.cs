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
        #endregion

        #region Properties
        public int Number { get => m_number; set => Set(ref m_number, value); }

        public string FileName { get => m_fileName; set => Set(ref m_fileName, value); }
        #endregion

        #region Ctor
        public FileViewModel(int number, string filePath)
        {
            #region Init Fields
            m_fileName = filePath;

            m_number = number;
            #endregion
        }

        public FileViewModel(int number) : this(number, string.Empty)
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
