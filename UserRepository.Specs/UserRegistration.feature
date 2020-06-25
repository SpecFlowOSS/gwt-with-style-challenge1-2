Feature: User registration

Background: 

Given a user repository with the following users:
| username | personal name | email            |
| mike     | Mike Smith    | mike@gmail.com   |
| steve123 | Steve James   | steve@yahoo.com  |


Scenario: Users with unique data should be able to register

When John Michaels attempts to register with the username "john" and email "johnm@gmail.com"
Then the user repository should contain the following users:
| username | personal name | email            |
| mike     | Mike Smith    | mike@gmail.com   |
| steve123 | Steve James   | steve@yahoo.com  |
| john     | John Michaels | johnm@gmail.com  |


Scenario: Personal names do not have to be unique

When Mike Smith attempts to register with the username "john" and email "johnm@gmail.com"
Then the user repository should contain the following users:
| username | personal name | email            |
| mike     | Mike Smith    | mike@gmail.com   |
| steve123 | Steve James   | steve@yahoo.com  |
| john     | Mike Smith    | johnm@gmail.com  |


Scenario Outline: Usernames should be unique

  Detecting simple duplication is not enough, since usernames that are visually
  similar may lead to support problems and security issues. See 
  https://engineering.atspotify.com/2013/06/18/creative-usernames/ for more information.

When Steve James attempts to register with the username "<requested username>" and email "steve5@gmail.com"
Then the registration should fail with "Username taken"
And the user repository should contain the following users:
| username | personal name | email            |
| mike     | Mike Smith    | mike@gmail.com   |
| steve123 | Steve James   | steve@yahoo.com  |

Examples:

| comment                     | requested username | 
| identical username          | steve123           |
| minor spelling difference   | Steve123           |
| unicode normalisation       | sᴛᴇᴠᴇ123           |
| interpunction difference    | steve123.          |