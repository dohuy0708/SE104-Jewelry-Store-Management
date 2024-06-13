using Jewelry_store_management.VIEW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewelry_store_management.VIEWMODEL
{
   
    public class StartViewModel: BaseViewModel
    {
        public SignInViewModel SignInView;
        public SignUpViewModel SignUpView;
        
        public ForgetPasswordViewModel ForgetPasswordView;
        public GetNewPassWordViewModel GetNewPassWordView;
        public VerifyCodeViewModel VerifyCodeView;

        private int _nview=0;
        public int NView {
            get { return _nview; }
            set 
            {
                _nview = value;
                UpdateCurrentWindow();
                OnPropertyChanged(nameof(_nview));
            } 
        }
     

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        private void UpdateCurrentWindow()
        {
            switch (NView)
            {
                case 0:
                    CurrentView= SignInView;break;
                case 1:
                    CurrentView = SignUpView;break;
                case 2:
                    CurrentView = ForgetPasswordView;break;
                case 3:
                    CurrentView = VerifyCodeView;break;
                case 4:
                    CurrentView = GetNewPassWordView;break;
                default:
                    break;
            }
        }
        public StartViewModel() {
            SignInView = new SignInViewModel();
            SignUpView = new SignUpViewModel();
            ForgetPasswordView = new ForgetPasswordViewModel();
            GetNewPassWordView = new GetNewPassWordViewModel();
            VerifyCodeView = new VerifyCodeViewModel();
            CurrentView = SignInView;
        }
    }
}
