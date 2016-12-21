using MyProjectBLL.Persons;
using MyProjectDAL.Entity;
using System;
using System.Diagnostics.Contracts;
using System.Web.Http;

namespace MyProject.Controllers
{
    public class PetDataController : ApiController
    {
        [HttpGet]
        public Person Info(string name)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(name));
            var pm = new PersonManagement();
            var pet = pm.GetByName(name);
            return pet;
        }
    }
}
