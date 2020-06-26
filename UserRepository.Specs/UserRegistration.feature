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


Scenario Outline: Duplicated emails should be disallowed

When Mike Smith attempts to register with the username "mike99" and email "<email>"
Then the registration should fail with "Email already registered"

Examples: uppercase/lowercase aliases should be detected

| comment                                                    | email               |
| emails should be case insensitive                          | Mike@gmail.com      |
| domains should be case insensitive                         | mike@Gmail.com      |
| unicode normalisation tricks should be detected            | mᴵke@gmail.com      |

Examples: Gmail aliases should be detected

   GMail is a very popular system so many users will register
   with gmail emails. Sometimes they use gmail aliases or labels,
   to prevent users mistakenly registering for multiple accounts
   the user repository should recognise common Gmail tricks.

| comment                                                    | email               |
| google ignores dots in an email, so mi.ke is equal to mike | mi.ke@gmail.com     |
| google allows setting labels by adding +label to an email  | mike+test@gmail.com |
| googlemail is a equivalent alias for gmail                 | mike@googlemail.com |