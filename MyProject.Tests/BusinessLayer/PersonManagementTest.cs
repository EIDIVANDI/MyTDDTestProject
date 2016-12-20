using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProjectBLL.Persons;
using MyProjectDAL.Entity;
using MyProjectBLL.Repository;
using Moq;

namespace MyProject.Tests.BusinessLayer
{
    [TestClass]
    public class PersonManagementTest
    {
        Mock<IRepository> _Repository = new Mock<IRepository>();

        #region OLD TEST METHOD , Before Mock
        /// <summary>
        /// OLD TEST METHOD , Before Mock
        /// </summary>
        //[TestMethod]
        //public void GetByName_ExistingPerson_ReturnsPerson()
        //{
        //    // Arrange
        //    string personName = "Fido";
        //    PersonManagement pm = new PersonManagement();  
        //    // Act
        //    Person result = pm.GetByName(personName);
        //    // Assert
        //    Assert.IsNotNull(result);
        //}

        #endregion

        [TestMethod]

        public void GetByName_ExistingPerson_ReturnsPerson()
        {
            // Arrange
            string personName = "Fido";
            _Repository.Setup(x => x.GetByName(It.Is<string>(y => y == "Fido")))
            .Returns(new Person { PersonName = "Fido" });
            var pm = new PersonManagement(_Repository.Object);
            // Act
            var result = pm.GetByName(personName);
            // Assert
            Assert.IsNotNull(result); 
            Assert.AreEqual(personName, result.PersonName);

        }

    }
}