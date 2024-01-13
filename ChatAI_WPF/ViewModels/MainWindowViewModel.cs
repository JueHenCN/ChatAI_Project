using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatAI_WPF.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public ICommand LoadedCommand => new DelegateCommand(() =>
        {

        });
    }
}
