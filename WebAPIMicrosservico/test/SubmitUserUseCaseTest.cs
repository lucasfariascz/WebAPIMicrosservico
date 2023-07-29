using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebAPIMicrosservico.Features.User.Controllers.dto;
using WebAPIMicrosservico.Features.User.Domain.Models;
using WebAPIMicrosservico.Features.User.Domain.Repository;

namespace WebAPIMicrosservico.Features.User.Domain.UseCases
{
    [TestClass]
    public class SubmitUserUseCaseTest
    {
        [TestMethod]
        public async void SubmitUserUseCase_ShouldReturnMessageSendObjectSuccess()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var submitUserDTO = new SubmitUserDTO
            {
                Id = "1",
                Name = "João Silva",
                Email = "joao@example.com",
                Message = "Olá, João"
            };

            // Configura o método SubmitUser do IUserRepository para retornar uma mensagem simulada
            var userRepositoryResultMessage = "Objeto enviado com sucesso.";
            userRepositoryMock.Setup(repo => repo.SubmitUser(It.IsAny<UserModel>())).ReturnsAsync(userRepositoryResultMessage);

            // Cria uma instância do SubmitUserUseCase
            var submitUserUseCase = new SubmitUserUseCase(userRepositoryMock.Object);

            // Act
            // Chama o método SubmitUser no SubmitUserUseCase com o SubmitUserDTO preparado
            var result = await submitUserUseCase.SubmitUser(submitUserDTO);

            // Assert
            // Verifica se o método SubmitUser de IUserRepository foi chamado exatamente uma vez
            userRepositoryMock.Verify(repo => repo.SubmitUser(It.IsAny<UserModel>()), Times.Once);

            // Compara a mensagem retornada por SubmitUserUseCase com a mensagem simulada configurada no mock de IUserRepository
            Assert.AreEqual(userRepositoryResultMessage, result);

        }

        public async void SubmitUserUseCase_ShouldReturnMessageNotSendObject()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var submitUserDTO = new SubmitUserDTO
            {
                Id = "1",
                Name = "João Silva",
                Email = "joao@example.com",
                Message = "Olá, João"
            };

            // Configura o método SubmitUser do IUserRepository para retornar uma mensagem simulada
            var userRepositoryResultMessage = "Objeto não enviado.";
            userRepositoryMock.Setup(repo => repo.SubmitUser(It.IsAny<UserModel>())).ReturnsAsync(userRepositoryResultMessage);

            // Cria uma instância do SubmitUserUseCase
            var submitUserUseCase = new SubmitUserUseCase(userRepositoryMock.Object);

            // Act
            // Chama o método SubmitUser no SubmitUserUseCase com o SubmitUserDTO preparado
            var result = await submitUserUseCase.SubmitUser(submitUserDTO);

            // Assert
            // Verifica se o método SubmitUser de IUserRepository foi chamado exatamente uma vez
            userRepositoryMock.Verify(repo => repo.SubmitUser(It.IsAny<UserModel>()), Times.Once);

            // Compara a mensagem retornada por SubmitUserUseCase com a mensagem simulada configurada no mock de IUserRepository
            Assert.AreEqual(userRepositoryResultMessage, result);

        }
    }
}
