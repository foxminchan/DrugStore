@EditCustomer
Feature: EditCustomer

    As a staff member, I want to be able to edit customers in the system.

    This feature enables staff members to modify existing customer information, allowing for updates and adjustments as needed.

@NavigateToEditCustomer
Scenario: 1 Navigate to Edit Customer Page
	Given that the staff member is logged in
	And on the customer page
    When the staff member clicks on the edit button associated with a customer
    Then they should be redirected to the edit customer page

@EditSuccess
Scenario: 2 Edit a staff successfully
	Given that the staff member is logged in
	And on the "Edit staff" page
	When the staff member fills in the staff's information with Full Name: "John Doe", Email: "johndoes@exmaple.com", Phone: "1234567890"
	And with Address information with Street: "1234 Example St", City: "Example City", Province: "Example Province"
    Then the customer should be successfully updated in the system
    And the staff member should be redirected to the manage category page

@EditFail
Scenario Outline: 3 Update a staff with invalid information
	Given that the staff member is logged in
	And on the "Edit staff" page
	When the staff member fills in the staff's information with Full Name: "<full_name>", Email: "<email>", Phone: "<phone>"
	And with Address information with Street: "<street>", City: "<city>", Province: "<province>"
	Then the staff member should see an error message "<error_message>"

	Examples:
		| full_name  | email                  | phone        | street            | city           | province        | error_message                  |
		| ""         | ""					 | ""           | ""                | ""             | ""              | "Full Name is required"         |
		| "John Doe" | ""					 | ""           | ""                | ""             | ""              | "Email is required"             |
		| "John Doe" | "johndoes@exmaple.com" | ""           | ""                | ""             | ""              | "Phone is required"             |
		| "John Doe" | "johndoes.exmaple.com" | "1234567890" | ""                | ""             | ""              | "Email is invalid"              |
		| "John Doe" | "johndoes@exmaple.com" | "1234567890" | ""                | ""             | ""              | "Street is required"            |
		| "John Doe" | "johndoes@exmaple.com" | "1234567890" | "1234 Example St" | ""             | ""              | "City is required"              |
		| "John Doe" | "johndoes@exmaple.com" | "1234567890" | "1234 Example St" | "Example City" | ""              | "Province is required"          |
