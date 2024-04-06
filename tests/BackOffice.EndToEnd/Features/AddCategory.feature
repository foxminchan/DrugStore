Feature: AddCategory

Staff can add category to system

@AddSuccess
Scenario: Add a category with valid data
	Given the staff is logged in
	When the staff adds a category
	Then the category is added to the system

@AddFail
Scenario: Add a category with empty category name
	Given the staff is logged in
	When the staff adds a category with an empty category name
	Then the system displays an error message
