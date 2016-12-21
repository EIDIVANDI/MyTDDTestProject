using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProject.Controllers;
using System.Web.Mvc;
using Moq;
using MyProjectBLL.Repository;
using MyProjectDAL.Entity;
using System.Web.Routing;
using MyProjectBLL.Persons;

namespace MyProject.Tests.Controllers
{
    
    [TestClass]
    public class PersonControllerTest
    {
        PersonController _Controller;
        [TestMethod]
        public void Index_NoInputs_ReturnsDefaultViewResult()
        {
            // Arrange
            PersonController controller = new PersonController();
            // Act
            ViewResult result = (ViewResult)controller.Index();
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
            Assert.IsNotNull(result.Model);

        }

        [TestMethod]
        public void Display_ExistingPerson_ReturnView()
        {
            // Arrange
            Mock<IRepository> _repository = new Mock<IRepository>();
            _repository.Setup(x => x.GetByName(It.Is<string>(y => y == "Fido")))
            .Returns(new Person { PersonName = "Fido" });
            PersonManagement pm = new PersonManagement(_repository.Object);
            PersonController controller = new PersonController(pm);
            string personName = "Fido";
            RouteData routeData = new RouteData();
            routeData.Values.Add("id", personName);
            ControllerContext context = new ControllerContext { RouteData = routeData };
            controller.ControllerContext = context;

            // Act
            ViewResult result = (ViewResult)controller.Display();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(Person));
            Assert.AreEqual(personName, ((Person)result.Model).PersonName);
        }

        [TestMethod]
        public void Display_NonExistingPerson_ReturnNotFoundView()
        {
            // Arrange
            string personName = "Barney";
            Mock<IRepository> _repository = new Mock<IRepository>();
            _repository.Setup(x => x.GetByName(It.Is<string>(y => y == "Fido")))
            .Returns(new Person { PersonName = "Fido" });
            PersonManagement pm = new PersonManagement(_repository.Object);
            PersonController controller = new PersonController(pm);

            RouteData routeData = new RouteData();
            routeData.Values.Add("id", personName);
            ControllerContext context = new ControllerContext { RouteData = routeData };
            controller.ControllerContext = context;

            // Act
            var result = controller.Display() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual("NotFound", result.RouteValues["action"]);
        }
        
        [TestMethod]
        public void Display_NonExistingPerson_ReturnsHttp404()
        {
            // Arrange
            string personName = "Barney";
            SetControllerContext(personName);

            // Act
            var result = _Controller.NotFoundError() as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
            Assert.AreEqual(404, result.StatusCode);
        }

        private void SetControllerContext(string personName)
        {
 
            Mock<IRepository> _repository = new Mock<IRepository>();
            _repository.Setup(x => x.GetByName(It.Is<string>(y => y == "Fido")))
            .Returns(new Person { PersonName = personName });
            PersonManagement pm = new PersonManagement(_repository.Object);
            _Controller = new PersonController(pm);

        }
    }
}
