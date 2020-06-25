Feature: User registration

Background: 

Given a user repository with the following users:
| username | personal name | email            |
| mike     | Mike Smith    | mike@gmail.com   |
| steve123 | Steve James   | steve@yahoo.com  |


Scenario: Users with unique data should be able to register

When John Michaels attempts to register with the username "john" and email "johnm@gmail.com"

Then the user repository will contain the following users:
| username | personal name | email            |
| mike     | Mike Smith    | mike@gmail.com   |
| steve123 | Steve James   | steve@yahoo.com  |
| john     | John Michaels | johnm@gmail.com  |


Scenario: Personal names do not have to be unique

When Mike Smith attempts to register with the username "john" and email "johnm@gmail.com"
Then the user repository will contain the following users:
| username | personal name | email            |
| mike     | Mike Smith    | mike@gmail.com   |
| steve123 | Steve James   | steve@yahoo.com  |
| john     | Mike Smith    | johnm@gmail.com  |