using MyProjectDAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProjectBLL.Repository
{

    public interface IRepository
    {
        Person GetByName(string name);

        Person AddPerson(Person person);

        IEnumerable<Person> GetAll();
    }
}
