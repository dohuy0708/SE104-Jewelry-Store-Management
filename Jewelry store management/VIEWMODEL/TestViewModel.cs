using Jewelry_store_management.HELPER;
using Jewelry_store_management.MODELS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace Jewelry_store_management.VIEWMODEL
{
    public class TestViewModel:BaseViewModel
    {
        // khai bao userhelper va khai bao list _users
        private UserHelper _userHelper;
        private ObservableCollection<User> _users;
       
        private string _name;
        private string _email;



        public TestViewModel()
        {
            _userHelper = new UserHelper();
            Users = new ObservableCollection<User>();
          

            AddUserCommand = new RelayCommand<object>(async (parameter) => await AddUser());
            RetrieveUserCommand = new RelayCommand<object>(async (parameter) => await LoadUsers());
        }

        private async Task LoadUsers()
        {
            var users = await _userHelper.GetAllUsers();
            foreach (var user in users)
            {
                Users.Add(user);
            }

           
            if (Users.Count > 0)
            {
                // Assuming the first user in the list should be retrieved
                var firstUser = Users[1];
                Name = firstUser.Name;
                Email = firstUser.Email;
            }
        }

        public async Task AddUser()
        {

            var newUser = new User
            {
                Name = Name,
               
                Uid = GenerateId(),
                 Email = Email
            };
            await _userHelper.AddUser(newUser);
            Users.Add(newUser);

            MessageBox.Show("Add user successed!");
        }

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public ICommand AddUserCommand { get; }
        public ICommand RetrieveUserCommand { get; }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        // public ICommand AddUserCommand => new RelayCommand(async () => await AddUser());

        // ham random ID 

        private static Random random = new Random();
        public static string GenerateId()
        {
            const string prefix = "AD"; // dinh nghia tien to co dinh

            // tao 4 so ngan nhien
            int randomNumber = random.Next(10000);

            // dinh dang thanh chuoi 4 ky tu
            string randomNumberString = randomNumber.ToString("D4");

            // ket hop thanh ID
            string id = prefix + randomNumberString;

            return id;
        }
    }

}
