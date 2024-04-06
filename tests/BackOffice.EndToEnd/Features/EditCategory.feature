Feature: EditCategory
    
Staff can edit a category

@EditSuccess
Scenario: Staff can edit a category
	Given the staff is logged in
	When the staff edits a category
	Then the category is updated

@EditFail
Scenario: Staff cannot edit a category
	Given the staff is logged in
	When the staff edits a category
	Then the category is not updated
