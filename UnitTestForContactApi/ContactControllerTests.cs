using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using TodoApi.DataAccessLayer;
using TodoApi.Models;
using TodoApi.Controllers;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace UnitTestForTodoApi
{
    [TestClass]
    public class ContactControllerTests
    {
        [TestInitialize()]
        public void Startup()
        {

        }

        [TestMethod]
        public void Should_Return_Correct_Contact()
        {
            //arrange
            var contactrepository = A.Fake<IContactRepository>();
            var expected = new Contact() { FirstName = "Brijesh" };

            A.CallTo(() => contactrepository.GetContactByID(A<long>.Ignored)).Returns(expected);

            //act
            ContactController ctrl = new ContactController(contactrepository);
            var actual = ctrl.GetById(1);

            //assert
            Assert.AreEqual(actual.Value.FirstName, expected.FirstName);
        }


        [TestMethod]
        public void Should_Return_NotFound_If_Contact_Not_Present()
        {
            //arrange
            var contactrepository = A.Fake<IContactRepository>();

            A.CallTo(() => contactrepository.GetContactByID(0)).Returns(null);

            //act
            ContactController ctrl = new ContactController(contactrepository);

            var notFound = ctrl.GetById(0);

            //assert

            Assert.IsInstanceOfType(notFound.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public void Should_Call_Repository_To_Update()
        {
            //arrange
            var contactrepository = A.Fake<IContactRepository>();
            A.CallTo(() => contactrepository.GetContactByID(1)).Returns(new Contact() { FirstName = "Brijesh" });
            //act
            ContactController ctrl = new ContactController(contactrepository);
            var contact = new Contact() { FirstName = "Brijeshdada" };
            var noContent = ctrl.Update(1, contact);

            //assert
            A.CallTo(() => contactrepository.UpdateContact(contact)).MustHaveHappenedOnceExactly();
        }


        [TestMethod]
        public void Should_Call_Repository_To_Delete()
        {
            //arrange
            var contactrepository = A.Fake<IContactRepository>();
            A.CallTo(() => contactrepository.GetContactByID(1)).Returns(new Contact() { FirstName = "Brijesh" });
            //act
            ContactController ctrl = new ContactController(contactrepository);

            var noContent = ctrl.Delete(1);

            //assert
            A.CallTo(() => contactrepository.DeleteContact(1)).MustHaveHappenedOnceExactly();
        }


        [TestMethod]
        public void Should_Return_BadRequest_for_invalid_Contact_Save()
        { 
            //arrange
            var contactrepository = A.Fake<IContactRepository>();
          
            //act
            ContactController ctrl = new ContactController(contactrepository);
            ctrl.ModelState.AddModelError("Email", "Invalid Email");
            var badRequest = ctrl.Create(new Contact() {Email="55" });

            //assert
            Assert.IsInstanceOfType(badRequest, typeof(BadRequestObjectResult));
        }

    }
}
