using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhanVien
{
    class Employee
    {
        private int _id;
        private string _name;
        private string _sex;
        private DateTime _dateOfBirth;
        private string _address;
        private string _phonenumbe;
        private string _image;

        public Employee()
        {
        }

        public Employee(int id, string name, string sex, DateTime dateOfBirth, string address, string phonenumbe, string image)
        {
            _id = id;
            _name = name;
            _sex = sex;
            _dateOfBirth = dateOfBirth;
            _address = address;
            _phonenumbe = phonenumbe;
            _image = image;
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Sex { get => _sex; set => _sex = value; }
        public DateTime DateOfBirth { get => _dateOfBirth; set => _dateOfBirth = value; }
        public string Address { get => _address; set => _address = value; }
        public string Phonenumbe { get => _phonenumbe; set => _phonenumbe = value; }
        public string Image { get => _image; set => _image = value; }
    }
}
