@EditCategory
Feature: EditCategory
    
    As a staff member, I want to be able to edit categories in the system.

    This feature enables staff members to modify existing category information, allowing for updates and adjustments as needed.

@NavigateToEditCategory
Scenario: 1 Navigate to Edit Category Page
    Given that the staff member is logged in
    And is on the manage category page
    When the staff member clicks on the edit button associated with a category
    Then they should be redirected to the edit category page
    
@EditSuccess
Scenario: 2 Staff Successfully Edits a Category
    Given the staff member is logged in
    And is on the edit category page
    When the staff member updates the category information with category name "New Category Name" and category description "New Category Description"
    Then the category should be successfully updated in the system
    And the staff member should be redirected to the manage category page

@EditFail
Scenario: 3 Staff cannot edit a category
	Given the staff is logged in
    And is on the edit category page
	When the staff member updates the category information with empty category name and category description
	Then the category is not updated
