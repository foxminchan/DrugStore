@AddCustomer
Feature: AddCustomer

    As a staff member, I want to add a new customer to the system so that I can keep track of their information.

@NavigateToCustomerPage
Scenario: 1 Navigate to add add customer page
	Given that the staff member is logged in
	And on the customer page
	When the staff member clicks on the "Add Customer" button
    Then they should be redirected to the "Add Customer" page

@AddSuccess
Scenario: 2 Add a customer successfully
	Given that the staff member is logged in
	And on the "Add Customer" page
	When the staff member fills in the customer's information with Full Name: "John Doe", Email: "johndoes@exmaple.com", Phone: "1234567890"
	And with Address information with Street: "1234 Example St", City: "Example City", Province: "Example Province"
	And with Password: "p@ssw0rD" and Confirm Password: "p@ssw0rD"
	Then the staff member should see a success message that the customer has been added to the system
	And the staff member should be redirected to the manage staff page

@AddSuccessWithDefaultPassword
Scenario: 3 Add a customer successfully with default password
	Given that the staff member is logged in
	And on the "Add Customer" page
	When the staff member fills in the customer's information with Full Name: "John Doe", Email: "johndoes@exmaple.com", Phone: "1234567890"
	And with Address information with Street: "1234 Example St", City: "Example City", Province: "Example Province"
	And click on the checkbox "Set default password P@ssw0rd"
	Then the staff member should see a success message that the customer has been added to the system with the default password
	And the staff member should be redirected to the manage staff page

@AddFail
Scenario Outline: 4 Add a customer with invalid information
	Given that the staff member is logged in
	And on the "Add Customer" page
	When the staff member fills in the customer's information with Full Name: "<full_name>", Email: "<email>", Phone: "<phone>"
	And with Address information with Street: "<street>", City: "<city>", Province: "<province>"
	And with Password: "<password>" and Confirm Password: "<confirm_password>"
	Then the staff member should see an error message "<error_message>"

	Examples:
	| full_name  | email                  | phone        | street            | city           | province           | password   | confirm_password | error_message                  |
	| ""         | ""                     | ""           | ""                | ""             | ""                 | ""         | ""               | "Full Name is required"        |
	| "John Doe" | ""                     | ""           | ""                | ""             | ""                 | ""         | ""               | "Email is required"            |
	| "John Doe" | "johndoes@exmaple.com" | ""           | ""                | ""             | ""                 | ""         | ""               | "Phone is required"            |
	| "John Doe" | "johndoes@exmaple.com" | "1234567890" | ""                | ""             | ""                 | ""         | ""               | "Street is required"           |
	| "John Doe" | "johndoes@exmaple.com" | "1234567890" | "1234 Example St" | ""             | ""                 | ""         | ""               | "City is required"             |
	| "John Doe" | "johndoes@exmaple.com" | "1234567890" | "1234 Example St" | "Example City" | ""                 | ""         | ""               | "Province is required"         |
	| "John Doe" | "johndoes@exmaple.com" | "1234567890" | "1234 Example St" | "Example City" | "Example Province" | ""         | ""               | "Password is required"         |
	| "John Doe" | "johndoes@exmaple.com" | "1234567890" | "1234 Example St" | "Example City" | "Example Province" | "p@ssw0rD" | ""               | "Confirm Password is required" |
	| "John Doe" | "johndoes.email.com"   | "1234567890" | "1234 Example St" | "Example City" | "Example Province" | "p@ssw0rD" | "p@ssw0rD"       | "Email is invalid"             |
	| "John Doe" | "johndoes@exmaple.com" | "123"		| "1234 Example St" | "Example City" | "Example Province" | "p@ssw0rD" | "p@ssw0rD"       | "Phone is invalid"             |
	| "John Doe" | "johndoes@exmaple.com" | "1234567890" | "1234 Example St" | "Example City" | "Example Province" | "p" | "p"       | "Password must contain at least one uppercase letter, one lowercase letter, one number and 8-15 characters" |
