using System;
using System.Collections.Generic;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace UserRepository.Specs
{
    [Binding]
    public class UserRegistrationSteps
    {
        private readonly UserRepositoryDriver _driver;

        public UserRegistrationSteps(UserRepositoryDriver driver)
        {
            _driver = driver;
        }

        [Given(@"a user repository with the following users:")]
        public void GivenAUserRepositoryWithTheFollowingUsers(Table table)
        {
            var users = table.CreateSet<User>();

            _driver.CreateUsers(users);
        }

        [When(@"(.*) attempts to register with the username ""(.*)"" and email ""(.*)""")]
        public void WhenAttemptsToRegisterWithTheUsernameAndEmail(string personalName, string userName, string email)
        {
            _driver.RegisterUser(personalName, userName, email);
        }

        [Then(@"the user repository should contain the following users:")]
        public void ThenTheUserRepositoryWillContainTheFollowingUsers(Table table)
        {
            var expectedUsers = table.CreateSet<User>();

            var actualUsers = _driver.GetAllUsers();


            Assert.That(actualUsers, Is.EquivalentTo(expectedUsers).Using(new UserEqualityComparer()));
        }

        [Then(@"the registration should fail with ""(.*)""")]
        public void ThenTheRegistrationShouldFailWith(string expectedErrorMessage)
        {
            Assert.That(_driver.RegistrationErrorMessage, Is.Not.Null, () => "Registration did not fail");
            Assert.That(_driver.RegistrationErrorMessage, Is.EqualTo(expectedErrorMessage), () => "Registration failed with wrong error message");
        }


        public class UserEqualityComparer : IEqualityComparer<User>
        {
            public bool Equals(User x, User y)
            {
                return
                    string.Equals(x.PersonalName, y.PersonalName, StringComparison.Ordinal) &&
                    string.Equals(x.UserName, y.UserName, StringComparison.Ordinal) &&
                    string.Equals(x.Email, y.Email, StringComparison.Ordinal);
            }

            public int GetHashCode(User obj)
            {
                return obj.PersonalName.GetHashCode() ^ obj.UserName.GetHashCode() ^ obj.Email.GetHashCode();
            }
        }
    }
}
