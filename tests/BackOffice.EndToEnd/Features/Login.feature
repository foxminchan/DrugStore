@Login
Feature: Login
As a staff member, i want to login to the system so that i can access the system

  Scenario: User login with valid credentials
    Given a logged out user on the login page
    When the user logs in with valid credentials
    Then they log in successfully

  Scenario Outline: User login with invalid credentials
    Given a logged out user on the login page
    When the user logs in with invalid credentials like email '<email>' and password '<password>'
    Then login error message should be displayed

    Examples: 
      | email          | password |
      |                |          |
      | test           | test     |
      |                | test     |
      | test           |          |
      | test@gmail.com | P@ssw0rd |
