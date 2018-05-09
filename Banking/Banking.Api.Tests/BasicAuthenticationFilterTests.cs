using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Banking.Api.Authentication;
using Moq;
using Xunit;

namespace Banking.Api.Tests
{
    public class BasicAuthenticationFilterTests
    {
        [Fact]
        public async Task AuthenticateAsync_AuthorizationNull_ErrorResultNull()
        {
            //Arrange
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            var authorisationParameterParserMock = new Mock<IAuthorisationParameterParser>();
            var sut = new BasicAuthenticationFilter(authenticationServiceMock.Object,
                authorisationParameterParserMock.Object);
            var context = GetHttpAuthenticationContext();

            //Act
            await
                sut.AuthenticateAsync(context,
                    CancellationToken.None);

            //Assert
            Assert.Null(context.ErrorResult);
        }

        [Fact]
        public async Task AuthenticateAsync_AuthorizationNull_PrincipalNull()
        {
            //Arrange
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            var authorisationParameterParserMock = new Mock<IAuthorisationParameterParser>();
            var sut = new BasicAuthenticationFilter(authenticationServiceMock.Object,
                authorisationParameterParserMock.Object);
            var context = GetHttpAuthenticationContext();

            //Act
            await
                sut.AuthenticateAsync(context,
                    CancellationToken.None);

            //Assert
            Assert.Null(context.Principal);
        }

        [Fact]
        public async Task AuthenticateAsync_AuthorizationNotBasic_ErrorResultNull()
        {
            //Arrange
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            var authorisationParameterParserMock = new Mock<IAuthorisationParameterParser>();
            var sut = new BasicAuthenticationFilter(authenticationServiceMock.Object,
                authorisationParameterParserMock.Object);
            var context = GetHttpAuthenticationContext("hjbsjhbdshj");

            //Act
            await
                sut.AuthenticateAsync(context,
                    CancellationToken.None);

            //Assert
            Assert.Null(context.ErrorResult);
        }

        [Fact]
        public async Task AuthenticateAsync_AuthorizationNotBasic_PrincipalNull()
        {
            //Arrange
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            var authorisationParameterParserMock = new Mock<IAuthorisationParameterParser>();
            var sut = new BasicAuthenticationFilter(authenticationServiceMock.Object,
                authorisationParameterParserMock.Object);
            var context = GetHttpAuthenticationContext("hjbsjhbdshj");

            //Act
            await
                sut.AuthenticateAsync(context,
                    CancellationToken.None);

            //Assert
            Assert.Null(context.Principal);
        }

        [Fact]
        public async Task AuthenticateAsync_AuthorizationBasic_AuthorizationParameterNull_ErrorResultSet()
        {
            //Arrange
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            var authorisationParameterParserMock = new Mock<IAuthorisationParameterParser>();
            var sut = new BasicAuthenticationFilter(authenticationServiceMock.Object,
                authorisationParameterParserMock.Object);
            var context = GetHttpAuthenticationContext("Basic");

            //Act
            await
                sut.AuthenticateAsync(context,
                    CancellationToken.None);

            //Assert
            Assert.Equal("Missing credentials", ((AuthenticationFailureResult)context.ErrorResult).ReasonPhrase);
        }

        [Fact]
        public async Task AuthenticateAsync_AuthorizationBasic_AuthorizationParameterNull_PrincipalNull()
        {
            //Arrange
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            var authorisationParameterParserMock = new Mock<IAuthorisationParameterParser>();
            var sut = new BasicAuthenticationFilter(authenticationServiceMock.Object,
                authorisationParameterParserMock.Object);
            var context = GetHttpAuthenticationContext("Basic");

            //Act
            await
                sut.AuthenticateAsync(context,
                    CancellationToken.None);

            //Assert
            Assert.Null(context.Principal);
        }

        [Fact]
        public async Task
            AuthenticateAsync_AuthorizationBasic_AuthorizationParameterSet_LoginDetailsNotParsed_ErrorResultSet()
        {
            //Arrange
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            var authorisationParameterParserMock = new Mock<IAuthorisationParameterParser>();
            var sut = new BasicAuthenticationFilter(authenticationServiceMock.Object,
                authorisationParameterParserMock.Object);
            var authorizationParameter = "gdzfdhhtgfhf";
            var context = GetHttpAuthenticationContext("Basic", authorizationParameter);
            authorisationParameterParserMock.Setup(x => x.Parse(authorizationParameter, It.IsAny<char>()))
                .Returns((LoginDetails)null);

            //Act
            await
                sut.AuthenticateAsync(context,
                    CancellationToken.None);

            //Assert
            Assert.Equal("Invalid credentials", ((AuthenticationFailureResult)context.ErrorResult).ReasonPhrase);
        }

        [Fact]
        public async Task
            AuthenticateAsync_AuthorizationBasic_AuthorizationParameterSet_LoginDetailsNotParsed_PrincipalNull()
        {
            //Arrange
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            var authorisationParameterParserMock = new Mock<IAuthorisationParameterParser>();
            var sut = new BasicAuthenticationFilter(authenticationServiceMock.Object,
                authorisationParameterParserMock.Object);
            var authorizationParameter = "gdzfdhhtgfhf";
            var context = GetHttpAuthenticationContext("Basic", authorizationParameter);
            authorisationParameterParserMock.Setup(x => x.Parse(authorizationParameter, It.IsAny<char>()))
                .Returns((LoginDetails)null);

            //Act
            await
                sut.AuthenticateAsync(context,
                    CancellationToken.None);

            //Assert
            Assert.Null(context.Principal);
        }

        [Fact]
        public async Task
            AuthenticateAsync_AuthorizationBasic_AuthorizationParameterSet_LoginDetailsParsed_NotAuthenticated_ErrorResultSet
            ()
        {
            //Arrange
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            var authorisationParameterParserMock = new Mock<IAuthorisationParameterParser>();
            var sut = new BasicAuthenticationFilter(authenticationServiceMock.Object,
                authorisationParameterParserMock.Object);
            var authorizationParameter = "gdzfdhhtgfhf";
            var context = GetHttpAuthenticationContext("Basic", authorizationParameter);
            var loginDetails = new LoginDetails("a", "b");
            authorisationParameterParserMock.Setup(x => x.Parse(authorizationParameter, It.IsAny<char>()))
                .Returns(loginDetails);
            authenticationServiceMock.Setup(
                    x =>
                        x.AuthenticateAsync(loginDetails.Username, loginDetails.Password,
                            It.IsAny<CancellationToken>()))
                .ReturnsAsync((IPrincipal)null);

            //Act
            await
                sut.AuthenticateAsync(context,
                    CancellationToken.None);

            //Assert
            Assert.Equal("Invalid username or password",
                ((AuthenticationFailureResult)context.ErrorResult).ReasonPhrase);
        }

        [Fact]
        public async Task
            AuthenticateAsync_AuthorizationBasic_AuthorizationParameterSet_LoginDetailsParsed_NotAuthenticated_PrincipalNull
            ()
        {
            //Arrange
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            var authorisationParameterParserMock = new Mock<IAuthorisationParameterParser>();
            var sut = new BasicAuthenticationFilter(authenticationServiceMock.Object,
                authorisationParameterParserMock.Object);
            var authorizationParameter = "gdzfdhhtgfhf";
            var context = GetHttpAuthenticationContext("Basic", authorizationParameter);
            var loginDetails = new LoginDetails("a", "b");
            authorisationParameterParserMock.Setup(x => x.Parse(authorizationParameter, It.IsAny<char>()))
                .Returns(loginDetails);
            authenticationServiceMock.Setup(
                    x =>
                        x.AuthenticateAsync(loginDetails.Username, loginDetails.Password,
                            It.IsAny<CancellationToken>()))
                .ReturnsAsync((IPrincipal)null);

            //Act
            await
                sut.AuthenticateAsync(context,
                    CancellationToken.None);

            //Assert
            Assert.Null(context.Principal);
        }

        [Fact]
        public async Task
            AuthenticateAsync_AuthorizationBasic_AuthorizationParameterSet_LoginDetailsParsed_Authenticated_ErrorResultNull
            ()
        {
            //Arrange
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            var authorisationParameterParserMock = new Mock<IAuthorisationParameterParser>();
            var sut = new BasicAuthenticationFilter(authenticationServiceMock.Object,
                authorisationParameterParserMock.Object);
            var authorizationParameter = "gdzfdhhtgfhf";
            var context = GetHttpAuthenticationContext("Basic", authorizationParameter);
            var loginDetails = new LoginDetails("a", "b");
            authorisationParameterParserMock.Setup(x => x.Parse(authorizationParameter, It.IsAny<char>()))
                .Returns(loginDetails);
            authenticationServiceMock.Setup(
                    x =>
                        x.AuthenticateAsync(loginDetails.Username, loginDetails.Password,
                            It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Mock<IPrincipal>().Object);

            //Act
            await
                sut.AuthenticateAsync(context,
                    CancellationToken.None);

            //Assert
            Assert.Null(context.ErrorResult);
        }

        [Fact]
        public async Task
            AuthenticateAsync_AuthorizationBasic_AuthorizationParameterSet_LoginDetailsParsed_Authenticated_PrincipalSet
            ()
        {
            //Arrange
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            var authorisationParameterParserMock = new Mock<IAuthorisationParameterParser>();
            var sut = new BasicAuthenticationFilter(authenticationServiceMock.Object,
                authorisationParameterParserMock.Object);
            var authorizationParameter = "gdzfdhhtgfhf";
            var context = GetHttpAuthenticationContext("Basic", authorizationParameter);
            var loginDetails = new LoginDetails("a", "b");
            authorisationParameterParserMock.Setup(x => x.Parse(authorizationParameter, It.IsAny<char>()))
                .Returns(loginDetails);
            var principal = new Mock<IPrincipal>().Object;
            authenticationServiceMock.Setup(
                    x =>
                        x.AuthenticateAsync(loginDetails.Username, loginDetails.Password,
                            It.IsAny<CancellationToken>()))
                .ReturnsAsync(principal);

            //Act
            await
                sut.AuthenticateAsync(context,
                    CancellationToken.None);

            //Assert
            Assert.Equal(principal, context.Principal);
        }

        private static HttpAuthenticationContext GetHttpAuthenticationContext(string scheme = null,
            string parameter = null)
        {
            var request = new HttpRequestMessage();
            var headers = request.Headers;
            headers.Authorization = scheme != null ? new AuthenticationHeaderValue(scheme, parameter) : null;
            var controllerContext = new HttpControllerContext { Request = request };
            var context = new HttpActionContext { ControllerContext = controllerContext };
            var httpAuthenticationContext = new HttpAuthenticationContext(context, null);
            return httpAuthenticationContext;
        }
    }
}
