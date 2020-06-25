Feature: User registration

Scenario: Users with unique data should be able to register

Given a user repository with the following users:
| username | personal name | email            |
| mike     | Mike Smith    | mike@gmail.com   |
| steve123 | Steve James   | steve@yahoo.com  |

When John Michaels attempts to register with the username "john" and email "johnm@gmail.com"

Then the user repository will contain the following users:
| username | personal name | email            |
| mike     | Mike Smith    | mike@gmail.com   |
| steve123 | Steve James   | steve@yahoo.com  |
| john     | John Michaels | johnm@gmail.com  |