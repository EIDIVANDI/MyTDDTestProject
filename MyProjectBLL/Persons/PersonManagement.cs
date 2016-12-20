using MyProjectBLL.Repository;
using MyProjectDAL.Context;
using MyProjectDAL.Entity;
using System.Linq;

namespace MyProjectBLL.Persons
{
    public class PersonManagement
    {
        private IRepository _Repository;
        public PersonManagement() : this(new EFRepository()) { }
        public PersonManagement(IRepository repository)
        {
            _Repository = repository;
        }

        public Person GetByName(string name)
        {
            Person p = _Repository.GetByName(name);
            return p;
        }

    }
}
