@AddCategory
Feature: AddCategory
As a staff member, I want to be able to add categories to the system.

  Scenario: User adds a category with valid data
    Given a logged in user on the categories page
    When user add a category with valid data
    Then the category is added successfully

  Scenario: User adds a category long description
    Given a logged in user on the categories page
    When user add a category with long description
    Then category error message should be displayed

  Scenario: User adds a category with long name
    Given a logged in user on the categories page
    When user add a category with long name
    Then category error message should be displayed

  Scenario: User adds a category with long name and description
    Given a logged in user on the categories page
    When user add a category with long name and description
    Then category error message should be displayed

  Scenario Outline: User adds a category with invalid data
    Given a logged in user on the categories page
    When user add a category with name '<name>' and description '<description>'
    Then category error message should be displayed

    Examples: 
      | name | description |
      |      |             |
      |      | test        |
