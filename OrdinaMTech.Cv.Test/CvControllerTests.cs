using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OrdinaMTech.Cv.Api.Controllers;
using OrdinaMTech.Cv.WebApi.Services;
using System.Net;
using OrdinaMTech.Cv.Data.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace OrdinaMTech.Cv.Test
{
    [TestClass]
    public class CvControllerTests
    {
        [TestMethod]
        public void GettingBasePageReturnsHttpStatusOk()
        {
            // Arrange
            var mockedCvService = new Mock<ICvService>();
            mockedCvService.Setup(s => s.GetCv()).Returns(new Data.Models.Cv());

            var controller = new CvController(mockedCvService.Object);

            // Act
            var response = controller.Get();

            // Assert
            response.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [TestMethod]
        public void UploadingOversizedImageReturnsUnprocessableEntity()
        {
            // Arrange
            var mockedCvService = new Mock<ICvService>();
            var controller = new CvController(mockedCvService.Object);
            
            var file = new Mock<IFormFile>();
            file.Setup(f => f.Length).Returns(1024 * 3000); // 3000 KB (exceeds 2000 KB limit)
            
            var fileModel = new FileUploadModel { File = file.Object };

            // Act
            var response = controller.Upload(fileModel);

            // Assert
            response.Should().BeOfType<UnprocessableEntityObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.UnprocessableEntity);
        }

        [TestMethod]
        public void ResettingCvReturnsHttpStatusOk()
        {
            // Arrange
            var mockedCvService = new Mock<ICvService>();
            mockedCvService.Setup(s => s.GetCv()).Returns(new Data.Models.Cv());
            
            var controller = new CvController(mockedCvService.Object);

            // Act
            var response = controller.Reset();

            // Assert
            response.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
            
            mockedCvService.Verify(s => s.Reset(), Times.Once);
        }

    }
}
