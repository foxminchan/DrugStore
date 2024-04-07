@AddCategory
Feature: AddCategory

    As a staff member, I want to be able to add categories to the system.

@NavigateToAddCategory
Scenario: 1 Navigate to add category page
    Given that the staff member is logged in
    And on the manage category page
    When the staff member clicks on the "Add Category" button
    Then they should be redirected to the "Add Category" page

@AddSuccess
Scenario: 2 Successfully Add a Category
    Given that the staff member is logged in
    And on the "Add Category" page
    When the staff member fills in the category name field with 'Test Category' and the category description field with 'This is a test category'
    And clicks on the submit button
    Then the category should be successfully added to the system and the staff member should be redirected to the manage category page

@AddFail
Scenario: 3 Fail to Add a Category with Empty Category Name
    Given that the staff member is logged in
    When the staff member tries to add a category with an empty category name and empty category description
    Then the system should display an error message indicating that the category name cannot be empty
