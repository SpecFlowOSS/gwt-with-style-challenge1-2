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
    }
}
