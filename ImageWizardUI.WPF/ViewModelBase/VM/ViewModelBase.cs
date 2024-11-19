using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace ViewModelBase.VM
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        private Dispatcher? m_dispatcher;

        private bool[] m_ValidArray;

        public virtual Dispatcher Dispatcher { set => m_dispatcher = value; get => m_dispatcher; }

        public virtual string Error => throw new NotImplementedException();

        public virtual string this[string columnName] => throw new NotImplementedException();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string PropName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropName));
        }

        protected virtual void InitializeValidationArray(uint count)
        { 
            m_ValidArray = new bool[count];
        }

        protected virtual void SetValidationArrayValue(int index, bool value)
        { 
            m_ValidArray[index] = value;
        }

        protected virtual bool CheckValidationArray(int startIndex, int endIndex)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                if (m_ValidArray[i] == false)
                    return false;
            }

            return true;
        }

        protected virtual bool SetIfNull<T>(ref T field, T value, [CallerMemberName] string PropName = null)
        {
            if (field is not null && field.Equals(value))
            {
                return false;
            }
            else
            {
                field = value;

                OnPropertyChanged(PropName);

                return true;
            }
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropName = null)
        {
            if (field == null)
            {
                throw new ArgumentNullException(string.Format("Property: {0}", PropName));
            }

            if (field.Equals(value))
            {
                return false;
            }
            else
            {
                field = value;

                OnPropertyChanged(PropName);

                return true;
            }
        }

        protected virtual void QueueWorkToDispatcher(Action work)
        {
            if (m_dispatcher is null)
                throw new Exception("Dispatcher is not initialized!");

            m_dispatcher?.Invoke(work);
        }
    }
}
