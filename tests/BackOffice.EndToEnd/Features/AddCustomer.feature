@AddCustomer
Feature: AddCustomer
As a staff member, I want to add a new customer to the system so that I can keep track of their information.

  Scenario: User adds a customer with valid data
    Given a logged in user on the categories page
    When user add a customer with valid data
    Then the customer is added successfully

  Scenario Outline: User adds a customer with invalid data
    Given a logged in user on the categories page
    When user add a customer with full name '<name>', phone '<phone>', email '<email>', street '<street>', city '<city>', province '<province>', password '<password>' and confirm password '<confirm_password>'
    Then customer error message should be displayed

    Examples: 
      | name | phone      | email          | street | city | province | password | confirm_password |
      |      |            |                |        |      |          |          |                  |
      | test |            |                |        |      |          |          |                  |
      | test | 1234567890 |                |        |      |          |          |                  |
      | test | 1234567890 | test           |        |      |          |          |                  |
      | test | 1234567890 | test           | test   |      |          |          |                  |
      | test | 1234567890 | test           | test   | test |          |          |                  |
      | test | 1234567890 | test           | test   | test | test     |          |                  |
      | test | 1234567890 | test           | test   | test | test     | test     |                  |
      | test | 1234567890 | test           | test   | test | test     | test     | test             |
      | test | 1234567890 | test@gmail.com | test   | test | test     | P@ssw0rd | P@ssw1rd         |
